using Domain.Model;
using Main.Common.Enums;

namespace IRepository;

public interface IProductImageRepository
{
    Task<List<PanelPost>> GetSelectProducts ( EnumCompanyName company );
}
