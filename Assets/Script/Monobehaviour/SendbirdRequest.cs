using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;


public class SendbirdRequest : MonoBehaviour
{
    public delegate void SearchBookCallback(SearchBookData data);
    public delegate void DetailBookCallback(DetailBook data);

    const string url = "https://api.itbook.store/1.0/search/{0}/{1}"; //https://api.itbook.store/1.0/search/{query}/{page}
    const string orForm = "{0}|{1}";

    const string detailBookUrl = "https://api.itbook.store/1.0/books/{0}"; //https://api.itbook.store/1.0/books/{isbn13}

    private SearchBookCallback searchBookCallback;
    private DetailBookCallback detailBookCallback;

    public void SetCallback(SearchBookCallback searchBookCallback, DetailBookCallback detailBookCallback)
    {
        this.searchBookCallback = searchBookCallback;
        this.detailBookCallback = detailBookCallback;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void StartSearch(string keyword1, string keyword2, bool isOr, int page)
    {
        string caculUrl = null;

        if (true == string.IsNullOrEmpty(keyword2) || false == isOr)
        {
            caculUrl = string.Format(url, keyword1, page);
        }
        else
        {
            string query = string.Format(orForm, keyword1, keyword2);
            caculUrl = string.Format(url, query, page);
        }

        StartCoroutine(GetRequest(caculUrl));
    }

    IEnumerator GetRequest(string caculUrl)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(caculUrl))
        {
            yield return request.SendWebRequest();
            if(true == request.isNetworkError)
            {
                if(null != this.searchBookCallback)
                {
                    this.searchBookCallback(null);
                }
            }
            else
            {
                var data = request.downloadHandler.data;
                string str = Encoding.Default.GetString(data);  
                var dataObject = JsonUtility.FromJson<SearchBookData>(str);
                if (null != this.searchBookCallback)
                {
                    this.searchBookCallback(dataObject);
                }
            }
        }
    }

    public void GetDetailBook(string isbn13)
    {
        var sendUrl = string.Format(detailBookUrl, isbn13);     
        StartCoroutine(GetDetailBookRequest(sendUrl));
    }

    IEnumerator GetDetailBookRequest(string sendUrl)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(sendUrl))
        {
            yield return request.SendWebRequest();
            if (true == request.isNetworkError)
            {
                if (null != this.detailBookCallback)
                {
                    this.detailBookCallback(null);
                }
            }
            else
            {
                var data = request.downloadHandler.data;
                string str = Encoding.Default.GetString(data);               
                var dataObject = JsonUtility.FromJson<DetailBook>(str);
                if (null != this.detailBookCallback)
                {
                    this.detailBookCallback(dataObject);
                }
            }
        }
    }
}


