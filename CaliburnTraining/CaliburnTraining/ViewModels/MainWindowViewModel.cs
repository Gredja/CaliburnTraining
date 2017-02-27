using Caliburn.Micro;
using CaliburnTraining.Services;
using CaliburnTraining.Services.Interfaces;
using CaliburnTraining.ViewModels.Base;

namespace CaliburnTraining.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        Screen initViewModel;
        Navigator navigator;

        public Screen InitViewModel
        {
            get { return initViewModel; }
            set
            {
                initViewModel = value;
                NotifyOfPropertyChange(() => InitViewModel);
            }
        }

        public MainWindowViewModel(INavigator navigator)
        {
            this.navigator = (Navigator)navigator;
            this.navigator.changeViewModel += () =>
            {
                InitViewModel = this.navigator.MainViewModel;
            };
            navigator.NavigateTo(SubjectProvider.Instance.Create<UsersListViewModel>());
        }
    }
}


