using DataTransferModel;
using Main.Common.Enums;

namespace Application.Service;

public interface IQueryPageService
{
    Task<List<PageDataModel>> GetAllPages(EnumCompanyName company);
}

