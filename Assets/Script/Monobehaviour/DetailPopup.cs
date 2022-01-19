using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class DetailPopup : MonoBehaviour
{
    const string noneMsg = "--------";  

    #region UI
    public Text text_Info;
    public RawImage image_BookImg;
    public RectTransform transform_Content;
    public Button button_X;
    #endregion

    private DetailBook book;   

    private void Awake()
    {
        this.button_X.onClick.AddListener(OnButton_X);
    } 
    
    public void OpenPopup()
    {
        InitUI();
        this.gameObject.SetActive(true);
    }

    private void InitUI()
    {
        this.transform_Content.SetPositionY(0);
        this.transform_Content.SetSizeY(500);
        this.text_Info.text = string.Empty;
        this.image_BookImg.gameObject.SetActive(false);
    }

    public void SetBook(DetailBook book)
    {
        this.book = book;
        if (null == this.book)
        {
            // 정보 가져오기 실패
            InitUI();
        }
        else
        {
            this.text_Info.text = GetInfoString(book);

            var sizeY = this.text_Info.preferredHeight;
            this.transform_Content.SetSizeY(sizeY);

            if (false == string.IsNullOrEmpty(this.book.image) && false == Data.rawBookImgDic.ContainsKey(this.book.image))
            {
                ImageDownloader imageDownLoader = this.gameObject.AddComponent<ImageDownloader>();
                // 이미지 다운로드 요청 (스스로 사라짐)
                imageDownLoader.DownLoadImage(this.book.image);
            }
        }

        this.image_BookImg.texture = null;
    }

    private string GetInfoString(DetailBook book)
    {
        return string.Format(" Title : {0}\n\n Subtitle : {1}\n\n Authors : {2}\n\n Publisher : {3}\n\n Language : {4}\n\n Isbn10 : {5}\n\n Isbn13 : {6}\n\n Pages : {7}\n\n Year : {8}\n\n Rating : {9}\n\n Price : {10}\n\n URL : {11}\n\n Desc : {12}",
            book.title, book.subtitle, book.authors, book.publisher, book.language, book.isbn10, book.isbn13, book.pages, book.year, book.rating, book.price, book.url, book.desc);
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
        // 이미지 세팅
        if(null != this.book && null == this.image_BookImg.texture && false == string.IsNullOrEmpty(this.book.image))
        {
            if (true == Data.rawBookImgDic.ContainsKey(this.book.image))
            {
                this.image_BookImg.gameObject.SetActive(true);
                this.image_BookImg.texture = Data.rawBookImgDic[this.book.image];
            }
        }
    }

    #region callback
    private void OnButton_X()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}


