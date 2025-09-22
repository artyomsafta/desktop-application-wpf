using QuestPDF.Infrastructure;
using System.Windows;
using Task8_WPF.BAL.Services.DbInitializer;
using Task8_WPF.DAL;

namespace Task8_WPF.UI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var context = new WpfAppDbContext())
        {
            IDbInitializer initializer = new DbInitializer(context);
            initializer.Initialize();
        }

        var mainWindow = new MainWindow();
        mainWindow.Show();
        QuestPDF.Settings.License = LicenseType.Community;
    }
}
