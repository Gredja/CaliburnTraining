using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using CaliburnTraining.Services;
using CaliburnTraining.Services.Interfaces;
using CaliburnTraining.ViewModels.Base;
using Core.Utility;
using Model;

namespace CaliburnTraining.ViewModels
{
    public class UsersListViewModel : BaseViewModel
    {
        #region Variables

        UserModel selectedUser;
        ObservableCollection<UserModel> items;
        DataManager dataManager;
        INavigator navigator;
        string photoPath;

        #endregion

        #region Constructor & Destructor

        public UsersListViewModel(DataManager dataManager, INavigator navigator)
        {
            this.dataManager = dataManager;
            this.navigator = navigator;
        }

        public override void OnActivate()
        {
            base.OnActivate();
            Items = new ObservableCollection<UserModel>(dataManager.LoadModel());
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private async void Items_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            dataManager.SaveModel(Items.ToList());
        }

        #endregion

        #region Properties

        public ObservableCollection<UserModel> Items
        {
            get { return items; }
            set
            {
                if (!Equals(value, items))
                {
                    items = value;
                    NotifyOfPropertyChange(() => Items);
                }
            }
        }

        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (!Equals(value, selectedUser))
                {
                    selectedUser = value;
                    NotifyOfPropertyChange(() => SelectedUser);
                }
            }
        }

        public string PhotoPath
        {
            get { return photoPath; }
            set
            {
                photoPath = ImageHelper.FormattingImagePathForUserPhoto(SelectedUser.PhotoPath);
                NotifyOfPropertyChange(() => PhotoPath);
            }
        }


        #endregion

        #region Public Methods

        public void AddUser()
        {
            navigator.NavigateTo(SubjectProvider.Instance.Create<UserDetailsViewModel>("selectedUser", new UserModel()));
        }

        public void EditUser()
        {
            if (SelectedUser != null)
            {
                navigator.NavigateTo(SubjectProvider.Instance.Create<UserDetailsViewModel>("selectedUser", SelectedUser));
            }
        }

        public void SelectUser(object param)
        {
            var user = param as UserModel;
            if (user != null)
            {
                SelectedUser = user;
                PhotoPath = user.PhotoPath;
            }
        }

        public void DeleteUser()
        {
            Items.Remove(SelectedUser);

            SelectedUser = null;

            if (Items.Any())
            {
                SelectedUser = Items.First();
                PhotoPath = SelectedUser.PhotoPath;
            }
        }
        #endregion

    }
}
