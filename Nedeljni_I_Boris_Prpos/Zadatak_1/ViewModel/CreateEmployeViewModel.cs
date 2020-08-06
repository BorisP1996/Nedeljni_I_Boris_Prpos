using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class CreateEmployeViewModel : ViewModelBase
    {
        CreateEmploye ce;
        Tools tools = new Tools();

        public CreateEmployeViewModel(CreateEmploye ceOpen)
        {
            ce = ceOpen;
            GenderList = tools.GetGenders();
            MarriedList = tools.GetMarried();
            SectorList = tools.GetSector();
            EducationList = tools.GetEducation();
            PositionList = tools.GetPosition();
            User = new tblUser();
            Gender = new tblGender();
            Married = new tblMarried();
            Employe = new tblEmploye();
            Sector = new tblSector();
            Education = new tblEducation();
            Position = new tblPosition();
        }

        #region Properties
        private tblUser user;

        public tblUser User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private tblEmploye employe;

        public tblEmploye Employe
        {
            get { return employe; }
            set
            {
                employe = value;
                OnPropertyChanged("Employe");
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
        private tblEducation education;

        public tblEducation Education
        {
            get { return education; }
            set { education = value;
                OnPropertyChanged("Education");
            }
        }

        private List<tblEducation> educatioList;

        public List<tblEducation> EducationList
        {
            get { return educatioList; }
            set { educatioList = value;
                OnPropertyChanged("EudcationList");
            }
        }

        private tblSector sector;

        public tblSector Sector
        {
            get { return sector; }
            set { sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private List<tblSector> sectorList;

        public List<tblSector> SectorList
        {
            get { return sectorList; }
            set { sectorList = value;
                OnPropertyChanged("SectorList");
            }
        }

        private tblPosition position;

        public tblPosition Position
        {
            get { return position; }
            set { position = value;
                OnPropertyChanged("Position");
            }
        }

        private List<tblPosition> positionList;

        public List<tblPosition> PositionList
        {
            get { return positionList; }
            set { positionList = value;
                OnPropertyChanged("PositionList");
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
            ce.Close();
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
                    if (tools.GetRandomMenager() == 0)
                    {
                        MessageBox.Show("Not possible to create employe because there is not any manager created.");
                    }
                    else
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

                        if (tools.ValiationJMBG(User.JMBG) == true && tools.CheckCredentials(user.Username, user.Pasword, user.JMBG) == true && tools.GetRandomMenager() != 0)
                        {
                            context.tblUsers.Add(user);
                            tblEmploye newEmploye = new tblEmploye();
                            newEmploye.UserID = user.UserId;
                            newEmploye.ExperienceYear = Employe.ExperienceYear;
                            if (Position.PoisitionName != null)
                            {
                                newEmploye.PositionID = Position.PositionID;
                            }
                            newEmploye.SectorID = Sector.SectorID;
                            newEmploye.EducationID = Education.EducationID;
                            newEmploye.ManagerID = tools.GetRandomMenager();
                            context.tblEmployes.Add(newEmploye);
                            context.SaveChanges();
                            MessageBox.Show("Employe is saved");
                            //Update = true;
                            ce.Close();
                        }

                        else
                        {
                            MessageBox.Show("Error input\nCheck following:\nUsername must be unique\nPaswword must be unique\nJMBG must be valid");
                        }
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
            if ( String.IsNullOrEmpty(User.Firstname) || String.IsNullOrEmpty(User.Lastname) || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Place) || String.IsNullOrEmpty(Gender.Gender) || String.IsNullOrEmpty(Married.MarriedStatus) || String.IsNullOrEmpty(Sector.SectorName) || String.IsNullOrEmpty(Education.Education) || String.IsNullOrEmpty(Employe.ExperienceYear.ToString()))
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
