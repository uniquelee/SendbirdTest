using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class MainUI : MonoBehaviour
{
    // 한번 요청할때마다 10번씩 요청
    const int buffer_Page = 10;

    enum ESearchType
    {
        OR,
        NOT
    }

    #region UI
    public SendbirdRequest sendbirdRequest;

    public InputField inputfield_0;

    public Button button_Find;

    public BookScrollView bookScrollView;

    public DetailPopup detailPopup;
    #endregion

    private bool needDownLoad = false;
    private bool isDownloading = false;
    private int downloadPage;
    private int needDownloadPage;
    private string keyWord0;
    private string keyWord1;
    private ESearchType requestSearchType;

    private void Awake()
    {
        this.button_Find.onClick.AddListener(OnButton_Find);

        // 리퀘스터에 콜백 등록
        this.sendbirdRequest.SetCallback(Callback_SearchBook, Callback_DetailBook);
        this.bookScrollView.SetCallback(Callback_ScrollView);
    }

    private void Start()
    {
    }

    // 찾은 목록 지우기
    private void InitData()
    {
        this.keyWord0 = null;
        this.keyWord1 = null;
        this.sendbirdRequest.Stop();
        this.needDownLoad = true;
        this.isDownloading = false;
        this.downloadPage = 1;
        this.needDownloadPage = this.downloadPage + buffer_Page;
        this.requestSearchType = ESearchType.OR;
        Data.Clear();
    }

    private void StartSearchBook()
    {
        InitData();

        if (true == string.IsNullOrEmpty(inputfield_0.text))
        {
            // 팝업으로 검색어 입력 요구
            return;
        }

        // http query 에... or 연산은 되는데 not 연산이 안되는듯...
        // not 은 클라에서 자체적으로 구현
        if(true == inputfield_0.text.Contains("|"))
        {
            this.requestSearchType = ESearchType.OR;

            var strings = inputfield_0.text.Split('|');
            if(strings.Length > 0)
            {
                this.keyWord0 = strings[0];
            }

            if (strings.Length > 1)
            {
                this.keyWord1 = strings[1];
            }
        }
        else if(true == inputfield_0.text.Contains("-"))
        {
            this.requestSearchType = ESearchType.NOT;

            var strings = inputfield_0.text.Split('-');
            if (strings.Length > 0)
            {
                this.keyWord0 = strings[0];
            }

            if (strings.Length > 1)
            {
                this.keyWord1 = strings[1];
            }
        }
        else
        {
            this.keyWord0 = inputfield_0.text;
            this.keyWord1 = null;
        }

        this.bookScrollView.Init();
        SearchBook();
    }


    private void SearchBook()
    {
        this.isDownloading = true;
        this.needDownloadPage = this.downloadPage + buffer_Page;
        this.sendbirdRequest.StartSearch(this.keyWord0, this.keyWord1, this.requestSearchType == ESearchType.OR, this.downloadPage);
    }

    #region CallBack
    private void OnButton_Find()
    {
        StartSearchBook();
    }

    private void Callback_DetailBook(DetailBook data)
    {
        this.detailPopup.SetBook(data);
    }

    private void Callback_SearchBook(SearchBookData data)
    {
        if(null == data || null == data.books)
        {
            // 실패 혹은 마지막 페이지
            this.needDownLoad = false;
        }
        else
        {
            // 다운받은 페이지
            if(this.downloadPage != data.page)
            {
                this.needDownLoad = false;
            }

            if(this.requestSearchType == ESearchType.OR)
            {
                Data.AddData(data, null);
            }
            else
            {
                Data.AddData(data, this.keyWord1);
            }

            this.bookScrollView.Show();
        }

        if (true == this.needDownLoad && this.downloadPage < this.needDownloadPage)
        {
            this.sendbirdRequest.StartSearch(this.keyWord0, this.keyWord1, this.requestSearchType == ESearchType.OR, ++this.downloadPage);
        }
        else
        {
            this.isDownloading = false;
        }
    }

    private void Callback_ScrollView()
    {
        if(false == this.isDownloading)
        {
            SearchBook();
        }
    }

    public void OnButton_BookSelect(SimpleBook book)
    {
        this.detailPopup.OpenPopup();
        this.sendbirdRequest.GetDetailBook(book.isbn13);
    }
    #endregion
}


