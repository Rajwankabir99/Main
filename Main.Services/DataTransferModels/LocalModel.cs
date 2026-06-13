namespace DataTransferModel;

public class LocalModel
{
    public LocalModel ( )
    {
        Numbers = new List<int> ( );
    }

    public string PanelTitle
    {
        get; set;
    }

    public int TemplateTypeID
    {
        get; set;
    }

    public int PageID
    {
        get; set;
    }

    public List<int> Numbers
    {
        get; set;
    }
}
