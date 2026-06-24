using Main.Common;
namespace DataTransferModel;

public class DataModel
{
    public DataModel ( )
    {
    }

    public BaseDataModel BaseDataModel
    {
        get;
        set;
    }

    public void SetBaseDataModel ( BaseDataModel baseDataModel )
    {
        BaseDataModel = new BaseDataModel ( );

        BaseDataModel = baseDataModel;
    }
}
