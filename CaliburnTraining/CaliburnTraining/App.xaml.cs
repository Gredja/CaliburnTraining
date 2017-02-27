using System.Windows;
using CaliburnTraining.ViewModels;
using CaliburnTraining.Views;

namespace CaliburnTraining
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Window window;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShowWindow(new MainWindowView(SubjectProvider.Instance.Create<MainWindowViewModel>()));
        }

        void ShowWindow(Window win)
        {
            window = win;
            window.Show();
        }
    }
}
