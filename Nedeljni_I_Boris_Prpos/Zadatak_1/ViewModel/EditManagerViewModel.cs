using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;
using System.ComponentModel;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;
using Zadatak_1.View;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using System.Windows;
using System.Windows.Input;

namespace Zadatak_1.ViewModel
{
    class EditManagerViewModel : ViewModelBase
    {
        EditManager editMan;
        Tools tools = new Tools();
        Entity context = new Entity();

        public EditManagerViewModel(EditManager editManOpen,vwManager forwardedManager)
        {
            editMan = editManOpen;
            Manager = forwardedManager;
            GenderList = tools.GetGenders();
            MarriedList = tools.GetMarried();
            Married = new tblMarried();
            Gender = new tblGender();
        }

        private vwManager manager;

        public vwManager Manager
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
            editMan.Close();
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
                    tblUser user = (from r in context.tblUsers where r.UserId == Manager.UserId select r).FirstOrDefault();
                    user.Firstname = Manager.Firstname;
                    user.Lastname = Manager.Lastname;
                    user.JMBG = Manager.JMBG;
                    user.Username = Manager.Username;
                    user.Pasword = Manager.Pasword;
                    user.Place = Manager.Place;
                    user.GenderID = Gender.GenderID;
                    user.MariageID = Married.MarriedID;
                    
                    user.UserId = Manager.UserId;
                    tblManager newManager = (from r in context.tblManagers where r.ManagerID == Manager.ManagerID select r).FirstOrDefault();
                    newManager.HelpPass = Manager.HelpPass + "WPF";
                    newManager.OfficeNumber = Manager.OfficeNumber;
                    newManager.Mail = Manager.Mail;
                    newManager.ProjectsDone = Manager.ProjectsDone;
                    
                    newManager.ManagerID = Manager.ManagerID;
                    if (tools.ValiationJMBG(user.JMBG) == true /*&& tools.CheckCredentials(user.Username, user.Pasword, user.JMBG) == true && tools.CheckMailPas(newManager.Mail, newManager.HelpPass)*/)
                    {
                       
                        context.SaveChanges();
                        MessageBox.Show("Manager is edited");
                        //Update = true;
                        editMan.Close();
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
            if (String.IsNullOrEmpty(Manager.Firstname) || String.IsNullOrEmpty(Manager.Lastname) || String.IsNullOrEmpty(Manager.JMBG) || String.IsNullOrEmpty(Manager.Username) || String.IsNullOrEmpty(Manager.Pasword) || String.IsNullOrEmpty(Manager.Place) || String.IsNullOrEmpty(Gender.Gender) || String.IsNullOrEmpty(Married.MarriedStatus) || String.IsNullOrEmpty(Manager.HelpPass) || String.IsNullOrEmpty(Manager.Mail) || String.IsNullOrEmpty(Manager.OfficeNumber.ToString()) || String.IsNullOrEmpty(Manager.ProjectsDone.ToString()) || Manager.HelpPass.Length < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
