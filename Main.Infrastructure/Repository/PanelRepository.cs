
using Domain.Model;

using Main.IRepository;

using Main.Infrastructure;

namespace Main.Repository;

public class PanelRepository: IPanelRepository
{
    private readonly ApplicationDbContext _context;

    public PanelRepository ( )
    {
    }


    public PanelRepository ( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<bool> DeletePanelAsync ( int panelId )
    {
        Panel? panel = await _context.Panels.FindAsync ( panelId );

        if ( panel != null )
        {
            _context.Panels.Remove ( panel );
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        return false;
    }
}

