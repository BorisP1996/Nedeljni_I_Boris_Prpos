using System;
using Zadatak_1.View;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using System.Windows;
using System.Windows.Input;

namespace Zadatak_1.ViewModel
{
    class CreateManagerPasswordViewModel : ViewModelBase
    {
        CreateManagerPassword cmp;
        Tools tool = new Tools();

        public CreateManagerPasswordViewModel(CreateManagerPassword cmpOpen)
        {
            cmp = cmpOpen;
            //set count, user can guess the password 3 times
            Count = 3;
            //get key from file, ih user gueses it in 3 tries=>can continue
            Key = tool.ReadKey();
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
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
            cmp.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                //if input is wrong,decrement count (initialy it is 3, see in ctor)
                if (Key != Password)
                {
                    Count--;
                    MessageBox.Show("Wrong password, you have " + Count + " more attempts.");
                }
                //if key is correct, go on to create manager
                else
                {
                    cmp.Close();
                    CreateManager cm = new CreateManager();
                    cm.ShowDialog();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //cant click if count is decremented to zero
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Password) || Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}
