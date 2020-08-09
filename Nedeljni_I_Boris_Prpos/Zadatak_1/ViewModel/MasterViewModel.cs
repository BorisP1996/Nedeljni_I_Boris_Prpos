using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MasterViewModel : ViewModelBase
    {
        Master master;
        Entity context = new Entity();
        Tools tool = new Tools();

        public MasterViewModel(Master masterOpen)
        {
            master = masterOpen;
            AdminList = tool.GetAdmins();
        }
        #region Properties
        private vwAdmin admin;
        public vwAdmin Admin
        {
            get { return admin; }
            set { admin = value;
                OnPropertyChanged("Admin");
            }
        }
        private List<vwAdmin> adminList;

        public List<vwAdmin> AdminList
        {
            get { return adminList; }
            set { adminList = value;
                OnPropertyChanged("AdminList");
            }
        }

        #endregion
        #region Command
        private ICommand createAdmin;
        public ICommand CreateAdmin
        {
            get
            {
                if (createAdmin==null)
                {
                    createAdmin = new RelayCommand(param => CreateAdminExecute(), param => CanCreateAdminExecute());
                }
                return createAdmin;
            }
        }

        private void CreateAdminExecute()
        {
            try
            {
                MasterCreatesAdmin mca = new MasterCreatesAdmin();
                mca.ShowDialog();
                if ((mca.DataContext as MasterCreatesAdminViewModel ).Update==true)
                {
                    AdminList = tool.GetAdmins();
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateAdminExecute()
        {
            return true;
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            master.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion

    }
}
