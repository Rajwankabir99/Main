namespace BusinessModel;

public class AdminImageFileDataModel : BaseDataModel
{
    public AdminImageFileDataModel()
    {
    }

    public int AdminImageFileID { get; set; }
   
    public byte[] ImageFileContent { get; set; }
   
    public int AdminPostID { get; set; }
}
