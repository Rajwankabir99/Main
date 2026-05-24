using DataTransferModel;
using Domain.Model;
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Main.Services;

public class PagePanelService: IPagePanelService
{

    public readonly IProductImageRepository _productImageRepository;

    public readonly IAdminPostImageRepository _adminPostsImageRepository;

    public readonly IPageRepository _pageRepository;

    public PagePanelService ( 
        IProductImageRepository productImageRepository,
        IAdminPostImageRepository adminPostsImageRepository,
        IPageRepository pageRepository )
    {
        _productImageRepository = productImageRepository;
        _adminPostsImageRepository = adminPostsImageRepository;
        _pageRepository = pageRepository;
    }

    public async Task<bool> CreateNewPanels 
    ( 
          LocalModel model, List<PanelPostDataModel> listUserSelectedPosts
    )
    {

        PagePanel panelEntity = new PagePanel();

        panelEntity.PanelTemplate = ( EnumPanelTemplate ) model.TemplateTypeID;

        panelEntity.PanelTitle = model.PanelTitle;

        panelEntity.CreateBaseData ( baseDataModel );

        listUserSelectedPosts.ForEach ( objPost => {

            PanelPost panelPost = new PanelPost ( )
            {
                ImageFileContent = objPost.ImageFileContent,
                Price = objPost.Price,
                PostTitle = objPost.PostTitle,
                PostDescription = objPost.PostDescription
            };

            panelPost.CreateBaseData ( objPost.BaseDataModel );

            panelEntity.CreatePanelPost ( panelPost );

        } );

        var objPageEntity = await _context
                            .Pages
                            .FirstOrDefaultAsync
                            (m => m.PageID ==  model.PageID);

        PageContent objPageCotentEntity = objPageEntity != null

                    ? objPageEntity.GetNewOrExistingPageContent
                                    (model.PageID, baseDataModel)
                    : new PageContent();

        objPageCotentEntity.Page = null;

        objPageCotentEntity.CreatePagePanel ( panelEntity );


        if ( objPageEntity != null )
        {
            objPageEntity.SavePageContent ( objPageCotentEntity );

            _context.Pages.Update ( objPageEntity );
        }

        int result = await _context.SaveChangesAsync ( );

        return result > 0;


        return await _pageRepository.CreateNewContent (
                                            model,
                                            enumCompany,
                                            listUserSelectedPosts,
                                            baseDataModel );
    }

    

    public async Task<List<PanelPostDataModel>> GetSelectProducts ( 
                                                EnumCompanyName company )
    {

        return await _productImageRepository.GetSelectProducts ( company );
    }

    public async Task<PageDataModel> GetPanelList ( int pageID )
    {
        return await _pageRepository.GetSinglePage ( pageID );
    }
}
                                                             
