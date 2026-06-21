
using Main.Common.Model;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.WebAppCore;

public class MenuObjectModel
{
    public MenuObjectModel ( )
    {
    }

    public string ClientName
    {
        get; set;
    }

    public long? CategoryID
    {
        get; set;
    }

    public long? SubCategoryID
    {
        get; set;
    }

    public string SearchKey
    {
        get; set;
    }

    public string SimpleSearchKey
    {
        get; set;
    }

    public string SearchTag
    {
        get; set;
    }

    public string CategoryText
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_Category
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_SubCategory
    {
        get; set;
    }

    public List<TenantVariableModel> ListCategory
    {
        get; set;
    }

    public List<TenantVariableModel> ListSubCategory
    {
        get; set;
    }
}
