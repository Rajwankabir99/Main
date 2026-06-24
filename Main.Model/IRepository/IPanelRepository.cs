
namespace Main.IRepository;

public interface IPanelRepository
{
    Task<bool> DeletePanelAsync ( int panelId );
}