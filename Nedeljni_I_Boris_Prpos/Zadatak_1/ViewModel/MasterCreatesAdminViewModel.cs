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
    class MasterCreatesAdminViewModel : ViewModelBase
    {
        MasterCreatesAdmin mcd;
        Tools tool = new Tools();

        public MasterCreatesAdminViewModel(MasterCreatesAdmin mcdOpen)
        {
            mcd = mcdOpen;
            AdminList = tool.GetAdminsList();
            GenderList = tool.GetGenders();
            MarriedList = tool.GetMarried();
            User = new tblUser();
            Gender = new tblGender();
            Married = new tblMarried();
            AdminType = new tblAdminType();
            Admin = new tblAdmin();
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

        private tblAdmin admin;

        public tblAdmin Admin
        {
            get { return admin; }
            set { admin = value;
                OnPropertyChanged("Admin");
            }
        }

        private List<tblAdminType> adminList;
        public List<tblAdminType> AdminList
        {
            get
            {
                return adminList;
            }
            set
            {
                adminList = value;
                OnPropertyChanged("AdminTypeList");
            }
        }
        private tblAdminType adminType;

        public tblAdminType AdminType
        {
            get { return adminType; }
            set { adminType = value;
                OnPropertyChanged("AdminType");
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
            set { gender = value;
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
            set { married = value;
                OnPropertyChanged("Married");
            }
        }

        private bool update;

        public bool Update
        {
            get { return update; }
            set { update = value;
                OnPropertyChanged("Update");
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
            mcd.Close();
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
                if (save==null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }
        /// <summary>
        /// Method creates admin and validates unique paramtres
        /// </summary>
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
                    if (tool.ValiationJMBG(User.JMBG)==true && tool.CheckCredentials(user.Username,user.Pasword,user.JMBG)==true)
                    {
                        context.tblUsers.Add(user);
                        tblAdmin admin = new tblAdmin();
                        admin.UserID = user.UserId;
                        admin.AdminTypeID = AdminType.AdminTypeID;
                        admin.AcountExpire = DateTime.Now.AddDays(7);
                        context.tblAdmins.Add(admin);
                        context.SaveChanges();
                        MessageBox.Show("Admin is saved");
                        Update = true;
                    }
                    else
                    {
                        MessageBox.Show("Error input\nCheck following:\nUsername must be unique\nPaswword must be unique\nJMBG must be valid");
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
            if (String.IsNullOrEmpty(User.Firstname) || String.IsNullOrEmpty(User.Lastname) || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Place) || String.IsNullOrEmpty(Gender.Gender) || String.IsNullOrEmpty(Married.MarriedStatus) || String.IsNullOrEmpty(AdminType.AdminDesc))
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
