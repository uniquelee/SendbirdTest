using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BookListItem : MonoBehaviour
{
    const string noneMsg = "--------";  

    #region UI
    public Text text_Title;
    public Text text_SubTitle;
    public Text text_Price;
    public Text text_Url;
    public Text text_Isbn13;
    public RawImage image_BookImg;
    public Button button_BookSelect;
    public GameObject root;

    public MainUI mainUI;
    #endregion

    private SimpleBook book;
    private int index;
    public int Index
    {
        get { return this.index; }
        set
        {
            this.index = value;
            this.transform.SetPositionY(BookScrollView.listStartY - (this.index * BookScrollView.listGapY));
        }
    }

    private void Awake()
    {
        this.button_BookSelect.onClick.AddListener(OnButtonClick);
    }

    public void MoveIndex(bool moveDown)
    {
        if(true == moveDown)
        {
            if(Data.serchBookList.Count > this.Index + BookScrollView.numOfListItem)
            {
                this.Index += BookScrollView.numOfListItem;
            }
        }
        else
        {
            if(0 <= this.Index - BookScrollView.numOfListItem)
            {
                this.Index -= BookScrollView.numOfListItem;
            }
        }
    }

    private void SetBook(SimpleBook book)
    {
        if(this.book == book)
        {
            return;
        }

        this.book = book; 
        if (null == this.book)
        {
            this.root.SetActive(false);
        }
        else
        {
            this.root.SetActive(true);
            this.text_Title.text = WrapString(this.book.title);
            this.text_SubTitle.text = WrapString(this.book.subtitle);
            this.text_Price.text = WrapString(this.book.price);
            this.text_Url.text = WrapString(this.book.url);
            this.text_Isbn13.text = WrapString(this.book.isbn13.ToString());

            if (false == string.IsNullOrEmpty(this.book.image) && false == Data.rawBookImgDic.ContainsKey(this.book.image))
            {
                ImageDownloader imageDownLoader = this.gameObject.AddComponent<ImageDownloader>();
                // 이미지 다운로드 요청 (스스로 사라짐)
                imageDownLoader.DownLoadImage(this.book.image);
            }
        }

        this.image_BookImg.texture = null;
    }

    private string WrapString(string msg)
    {
        if(true == string.IsNullOrEmpty(msg))
        {
            return noneMsg;
        }

        return msg;
    }

    private void LateUpdate()
    {
        if(null == Data.serchBookList)
        {
            SetBook(null);
        }
        else
        {
            if(this.index < Data.serchBookList.Count)
            {
                if(Data.serchBookList[this.index] != this.book)
                {
                    SetBook(Data.serchBookList[this.index]);
                }
            }
            else
            {
                SetBook(null);
            }
        }

        // 이미지 세팅
        if(null != this.book && null == this.image_BookImg.texture && false == string.IsNullOrEmpty(this.book.image))
        {
            if (true == Data.rawBookImgDic.ContainsKey(this.book.image))
            {
                this.image_BookImg.texture = Data.rawBookImgDic[this.book.image];
            }
        }
    }

    #region callback
    private void OnButtonClick()
    {
        this.mainUI.OnButton_BookSelect(this.book);
    }
    #endregion
}


