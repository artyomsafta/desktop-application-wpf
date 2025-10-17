using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly WpfAppDbContext _context;

    public DbInitializer(WpfAppDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.Migrate();
        var newSeed = new DbSeeder();
        newSeed.Seed(_context);
    }
}
