using BusinessModel;
using IRepository;
using IService;
using Main.Common.Enums;

namespace Main.Service;

public class PageDataService: IPageDataService
{

    public readonly IPageRepository _pageRepository;


    public PageDataService(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }


    public async Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company ) => 
                await _pageRepository
                        .GetAllPages ( company );
                   
}

