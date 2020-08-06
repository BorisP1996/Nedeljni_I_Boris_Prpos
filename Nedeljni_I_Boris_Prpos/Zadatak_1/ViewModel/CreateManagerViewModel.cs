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
    class CreateManagerViewModel : ViewModelBase
    {
        CreateManager cm;
        Tools tools = new Tools();

        public CreateManagerViewModel(CreateManager cmOpen)
        {
            cm = cmOpen;
            GenderList = tools.GetGenders();
            MarriedList = tools.GetMarried();
            User = new tblUser();
            Gender = new tblGender();
            Married = new tblMarried();
            Manager = new tblManager();
        }

        #region Properties
        private tblUser user;

        public tblUser User
        {
            get { return user; }
            set { user = value;
                OnPropertyChanged("User");
            }
        }

        private tblManager manager;

        public tblManager Manager
        {
            get { return manager; }
            set { manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<tblGender> genderList;
        public List<tblGender> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }
        private tblGender gender;

        public tblGender Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<tblMarried> marriedList;
        public List<tblMarried> MarriedList
        {
            get
            {
                return marriedList;
            }
            set
            {
                marriedList = value;
                OnPropertyChanged("MarriedList");
            }
        }
        private tblMarried married;

        public tblMarried Married
        {
            get { return married; }
            set
            {
                married = value;
                OnPropertyChanged("Married");
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
            cm.Close();
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
                using (Entity context = new Entity())
                {
                    tblUser user = new tblUser();
                    user.Firstname = User.Firstname;
                    user.Lastname = User.Lastname;
                    user.JMBG = User.JMBG;
                    user.Username = User.Username;
                    user.Pasword = User.Pasword;
                    user.Place = User.Place;
                    user.GenderID = Gender.GenderID;
                    user.MariageID = Married.MarriedID;
                    tblManager newManager = new tblManager();
                    newManager.HelpPass = Manager.HelpPass+"WPF";
                    newManager.OfficeNumber = Manager.OfficeNumber;
                    newManager.Mail = Manager.Mail;
                    newManager.ProjectsDone = Manager.ProjectsDone;
                    if (tools.ValiationJMBG(User.JMBG) == true && tools.CheckCredentials(user.Username, user.Pasword, user.JMBG) == true && tools.CheckMailPas(newManager.Mail,newManager.HelpPass))
                    {
                        context.tblUsers.Add(user);                   
                        newManager.UserID = user.UserId;
                        context.tblManagers.Add(newManager);
                        context.SaveChanges();
                        MessageBox.Show("Manager is saved");
                        //Update = true;
                        cm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error input\nCheck following:\nUsername must be unique\nPaswword must be unique\nReserve pasword must be unique\nE-mail must be unique\nJMBG must be valid");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(User.Firstname) || String.IsNullOrEmpty(User.Lastname) || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Place) || String.IsNullOrEmpty(Gender.Gender) || String.IsNullOrEmpty(Married.MarriedStatus) || String.IsNullOrEmpty(Manager.HelpPass) || String.IsNullOrEmpty(Manager.Mail) || String.IsNullOrEmpty(Manager.OfficeNumber.ToString()) || String.IsNullOrEmpty(Manager.ProjectsDone.ToString()) || Manager.HelpPass.Length<5)
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
