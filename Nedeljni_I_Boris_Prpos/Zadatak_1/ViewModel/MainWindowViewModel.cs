using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;
using Zadatak_1.Methods;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        Methods.Delegate d = new Methods.Delegate();
        LogIn logged = new LogIn();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            //random 8 letter string into file, will be used for login
            d.GenerateKey();
            Count = 0;
        }

        #region Properties
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands
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
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        private void LoginExecute()
        {
            try
            {
                //Master logged in
                if (Username=="WPFMaster" && Password=="WPFAccess")
                {
                    Master master = new Master();
                    master.ShowDialog();
                }
                else if (logged.Admin(Username,Password)==1)
                {
                    MessageBox.Show("Welcome team admin");
                }
                else if (logged.Admin(Username,Password)==2)
                {
                    MessageBox.Show("Welcome system admin");
                }
                else if (logged.Admin(Username,Password)==3)
                {
                    MessageBox.Show("Welcome team admin");
                }
                else
                {
                    MessageBox.Show("Invalid parametres");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand createMan;
        public ICommand CreateMan
        {
            get
            {
                if (createMan == null)
                {
                    createMan = new RelayCommand(param => CreateManExecute(), param => CanCreateManExecute());
                }
                return createMan;
            }
        }
        private void CreateManExecute()
        {
            try
            {
                CreateManagerPassword cmp = new CreateManagerPassword();
                cmp.ShowDialog();
                Count = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private bool CanCreateManExecute()
        {
            if (Count>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand createEmp;
        public ICommand CreateEmp
        {
            get
            {
                if (createEmp == null)
                {
                    createEmp = new RelayCommand(param => CreateEmpExecute(), param => CanCreateEmpExecute());
                }
                return createEmp;
            }
        }
        private void CreateEmpExecute()
        {
            try
            {
                CreateEmploye ce = new CreateEmploye();
                ce.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private bool CanCreateEmpExecute()
        {
            return true;
        }
        #endregion
    }
}
