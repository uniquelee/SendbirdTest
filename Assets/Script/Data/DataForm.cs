[System.Serializable]
public class SearchBookData
{
    public int error;
    public int total;
    public int page;
    public SimpleBook[] books;
}

[System.Serializable]
public class SimpleBook
{
    public string title;
    public string subtitle;
    public string isbn13;
    public string price;
    public string image;
    public string url;
}

[System.Serializable]
public class DetailBook
{
    public int error;
    public string title;
    public string subtitle;
    public string authors;
    public string publisher;
    public string language;
    public string isbn10;
    public string isbn13;
    public string pages;
    public string year;
    public string rating;
    public string desc;
    public string price;
    public string image;
    public string url;
}