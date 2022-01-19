using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    public static void AllStopImageDownload()
    {
        var imageDownloaders = GameObject.FindObjectsOfType<ImageDownloader>();
        for(int i = 0; i < imageDownloaders.Length; i++)
        {
            imageDownloaders[i].enabled = false;
            Destroy(imageDownloaders[i]);
        }
    }

    public void DownLoadImage(string url)
    {
        StartCoroutine(GetTexture(url));
    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);            
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(uwr);
            if (false == Data.rawBookImgDic.ContainsKey(url))
            {
                Data.rawBookImgDic.Add(url, texture);
            }
        }

        Destroy(this);
    }
}


