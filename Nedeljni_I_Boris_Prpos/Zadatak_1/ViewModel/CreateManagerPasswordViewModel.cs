using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using Zadatak_1.Model;
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
            Count = 3;
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
            set { password = value;
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
               
                    if (Key != Password)
                    {
                        Count--;
                        MessageBox.Show("Wrong password, you have " + Count + " more attempts.");
                    }
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
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Password) || Count<=0)
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
