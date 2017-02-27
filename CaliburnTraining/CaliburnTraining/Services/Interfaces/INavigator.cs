using Caliburn.Micro;
using CaliburnTraining.ViewModels.Base;

namespace CaliburnTraining.Services.Interfaces
{
    public interface INavigator
    {
        void NavigateTo(BaseViewModel viewModel);

        void NavigateBack();
    }
}
