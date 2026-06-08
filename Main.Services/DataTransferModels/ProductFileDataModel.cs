using Main.Common.Model;

namespace DataTransferModel;

public class ProductFileDataModel : DataModel
{
    public ProductFileDataModel() {
        BaseDataModel = new BaseDataModel ( );
    }

    public ProductFileDataModel ( BaseDataModel baseDataModel )
    {
        BaseDataModel = new BaseDataModel ( );
        BaseDataModel = baseDataModel;
    }

    public int ProductImageFileID { get; set; }

    public byte[] ImageFileContent { get; set; }

    public int ProductID { get; set; }

    public ProductDataModel Product { get; set; }
}
