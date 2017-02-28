using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using CaliburnTraining.Services.Interfaces;
using CaliburnTraining.ViewModels;
using CaliburnTraining.ViewModels.Base;
using Action = System.Action;


namespace CaliburnTraining.Services
{
    public class Navigator : PropertyChangedBase, INavigator
    {
        BaseViewModel mainViewModel;
        public event Action changeViewModel;

        readonly Stack<BaseViewModel> screenStack = new Stack<BaseViewModel>();

        public BaseViewModel MainViewModel
        {
            get { return mainViewModel; }
            set
            {
                mainViewModel = value;
                NotifyOfPropertyChange(() => MainViewModel);
            }
        }

        public void NavigateTo(BaseViewModel viewModel)
        {
            screenStack.Push(viewModel);
            MainViewModel = viewModel;
            viewModel.OnActivate();
            changeViewModel?.Invoke();
        }

        public void NavigateBack()
        {
            if (screenStack.Count > 1)
            {
                var viewModel = screenStack.Pop();
                MainViewModel = screenStack.Peek();
                viewModel.OnActivate();
                changeViewModel?.Invoke();
            }
        }
    }
}
