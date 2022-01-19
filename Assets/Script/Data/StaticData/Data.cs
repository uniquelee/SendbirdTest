using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    // 메모리 관리를 위해서 서치 할때 마다 다운 받은 이미지를 지울지 결정
    const bool needRemoveBookTextureAtSearch = true;

    private static Dictionary<string, SimpleBook> serchBookDic = new Dictionary<string, SimpleBook>();   
    public static List<SimpleBook> serchBookList = new List<SimpleBook>();

    public static Dictionary<string, Texture2D> rawBookImgDic = new Dictionary<string, Texture2D>();

    public static void Clear()
    {
        serchBookDic.Clear();
        serchBookList.Clear();

        if(true == needRemoveBookTextureAtSearch)
        {
            ImageDownloader.AllStopImageDownload();

            foreach (var imageData in rawBookImgDic)
            {
                GameObject.DestroyImmediate(imageData.Value);
            }
            rawBookImgDic.Clear();
        }
    }

    public static void AddData(SearchBookData data, string excludeWard)
    {
        // excludeWard 는 대소문자 가리지 않고 제외
        if (null != excludeWard)
        {
            excludeWard = excludeWard.ToLower();
        }

        for (int i = 0; i < data.books.Length; i++)
        {
            if(null != excludeWard)
            {
                if(true == data.books[i].title.ToLower().Contains(excludeWard.ToLower()))
                {
                    continue;
                }
            }

            if (false == serchBookDic.ContainsKey(data.books[i].isbn13))
            {
                serchBookDic.Add(data.books[i].isbn13, data.books[i]);
                serchBookList.Add(data.books[i]);
            }
        }
    }
}