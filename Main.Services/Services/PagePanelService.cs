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
          LocalModel model, List<PanelPostDataModel> listUserSelectedPosts,BaseDataModel baseDataModel
    )
    {

        PagePanel panelEntity = new PagePanel();

        panelEntity.PanelTemplate = ( EnumPanelTemplate ) model.TemplateTypeID;

        panelEntity.PanelTitle = model.PanelTitle;

        panelEntity.CreateBaseData( baseDataModel )

        listUserSelectedPosts.ForEach ( obj => {

            PanelPost panelPost = new PanelPost ( )
            {
                ImageFileContent = obj.ImageFileContent,
                Price = obj.Price,
                PostTitle = obj.PostTitle,
                PostDescription = obj.PostDescription
            };

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

    private static PagePanel SetCreateDateTime ( BaseDataModel baseDataModel, PagePanel panelEntity )
    {
        panelEntity.CreatedDate = baseDataModel.CreatedDate;
        panelEntity.ModifiedDate = baseDataModel.ModifiedDate;
        panelEntity.CreatedBy = baseDataModel.CreatedBy;
        panelEntity.ModifiedBy = baseDataModel.ModifiedBy;
        panelEntity.HostCompanyName = baseDataModel.HostCompanyName;
        panelEntity.HostCountry = baseDataModel.HostCountry;

        return panelEntity;
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
                                                             
