using System;
using System.IO;
using System.Linq;
using CaliburnTraining.Services;
using CaliburnTraining.Services.Interfaces;
using CaliburnTraining.ViewModels.Base;
using Microsoft.Win32;
using Model;
using Core.Utility;

namespace CaliburnTraining.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        #region Variables

        Navigator navigator;
        DataManager dataManager;
        int selectedIndex = -1;
        UserModel selectedUser;
        UsersListViewModel usersListViewModel;
        OpenFileDialog ofd;

        #endregion

        #region Constructor & Destructor

        public UserDetailsViewModel(DataManager dataManager, INavigator navigator, UserModel selectedUser)
        {
            this.navigator = (Navigator)navigator;
            this.dataManager = dataManager;
            usersListViewModel = SubjectProvider.Instance.Create<UsersListViewModel>();

            PreporationForEdit(selectedUser);

        }

        #endregion

        #region Properties
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

        #endregion

        #region Public Methods

        public void BrowseFile()
        {
            ofd = new OpenFileDialog { Filter = "BMP | *.bmp | JPG | *.jpg; *.jpeg | PNG | *.png ", Title = "Photos" };

            if (ofd.ShowDialog() == true)
            {
                SelectedUser.PhotoPath = ofd.SafeFileName;
            }
        }

        public void Cancel()
        {
            navigator.NavigateBack();
        }

        public void Save()
        {
            SelectedUser.AddAllValidationErrors();
            if (SelectedUser.IsValid)
            {
                if (ofd != null)
                {
                    var extension = Path.GetExtension(ofd.SafeFileName);
                    Guid guid = Guid.NewGuid();
                    var fileName = $"{guid}{extension}";

                    File.Copy(ofd.FileName, PathHelper.MakeAbsPath($"/Media/{fileName}"));
                    SelectedUser.PhotoPath = fileName;
                }

                if (selectedIndex < 0)
                {
                    usersListViewModel.Items.Add(SelectedUser);
                }
                else
                {
                    usersListViewModel.Items.RemoveAt(selectedIndex);
                    usersListViewModel.Items.Insert(selectedIndex, SelectedUser);
                }

                navigator.NavigateBack();
            }
        }

        #endregion

        #region Private Methods

        void PreporationForEdit(UserModel selectedUser)
        {
            selectedIndex = usersListViewModel.Items.ToList().FindIndex(x => x == selectedUser);
            SelectedUser = (UserModel)selectedUser.Clone();
        }

        #endregion

    }
}
