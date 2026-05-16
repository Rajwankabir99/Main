namespace BusinessModel;

public class ManageAdminPostDataModel : BaseDataModel
{
    public ManageAdminPostDataModel ( ) => ListAdminPost = new List<AdminPostDataModel> ( );

    public List<AdminPostDataModel> ListAdminPost { get; set; }
}
