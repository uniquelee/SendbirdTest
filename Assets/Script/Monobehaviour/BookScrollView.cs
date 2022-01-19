using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BookScrollView : MonoBehaviour
{
    public const int numOfListItem = 10;
    public const int listStartY = -60;
    public const int listGapY = 110;
    private const float callbackOffsetY = 0.2f;

    #region UI
    public BookListItem dummyListItem;
    public RectTransform transform_Content;

    public Transform clip_Up;
    public Transform clip_Down;

    public ScrollRect scrollRect;
    #endregion

    private List<BookListItem> listItems = new List<BookListItem>(numOfListItem);
    private System.Action callback_BookScrollView;

    private void Awake()
    {
        // numOfListItem 만큼 리스트 아이템 생성       
        for (int i = 0; i < numOfListItem; i++)
        {
            BookListItem newListItem = null;
            if (0 == i)
            {
                newListItem = this.dummyListItem;
            }
            else
            {
                newListItem = Instantiate<BookListItem>(this.dummyListItem);
                newListItem.transform.Copy(this.dummyListItem.transform);
            }
            
            this.listItems.Add(newListItem);           
        }

        this.scrollRect.onValueChanged.AddListener(OnScrollRectValueChanged);

        Init();
    }

    public void SetCallback(System.Action callback)
    {
        this.callback_BookScrollView = callback;
    }

    public void Init()
    {
        for (int i = 0; i < this.listItems.Count; i++)
        {
            this.listItems[i].Index = i;
        }

        this.transform_Content.SetPositionY(0);
        this.transform_Content.SetSizeY(500);
    }

    public void Show()
    {
        if(0 == Data.serchBookList.Count)
        {
            Init();
            return;
        }

        var contentSizeY = Data.serchBookList.Count * listGapY;
        this.transform_Content.SetSizeY(contentSizeY);
        Update();
    }

    private void Update()
    {
        for (int i = 0; i < this.listItems.Count; i++)
        {
            var posY = this.listItems[i].transform.position.y;
            if(posY > clip_Up.position.y)
            {
                this.listItems[i].MoveIndex(true);
            }
            else if(posY < clip_Down.position.y)
            {
                this.listItems[i].MoveIndex(false);
            }
        }
    }

    #region callback
    void OnScrollRectValueChanged(Vector2 pos)
    {
        if(callbackOffsetY >= pos.y)
        {
            if(null != callback_BookScrollView)
            {
                callback_BookScrollView();
            }
        }
    }
    #endregion
}


