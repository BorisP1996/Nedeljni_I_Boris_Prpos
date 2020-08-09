using System;
using System.Collections.Generic;
using System.Linq;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class EditEmployeViewModel : ViewModelBase
    {
        EditEmploye editEmp;
        Entity context = new Entity();
        Tools tools = new Tools();

        public EditEmployeViewModel(EditEmploye editEmpOpen, vwEmploye forwardedEmploye)
        {
            editEmp = editEmpOpen;
            //catch employe to edit
            User = forwardedEmploye;
            GenderList = tools.GetGenders();
            MarriedList = tools.GetMarried();
            SectorList = tools.GetSector();
            EducationList = tools.GetEducation();
            PositionList = tools.GetPosition();
            Gender = new tblGender();
            Married = new tblMarried();
            Sector = new tblSector();
            Education = new tblEducation();
            Position = new tblPosition();
        }
        #region Properties
        private vwEmploye user;

        public vwEmploye User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
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
            set
            {
                education = value;
                OnPropertyChanged("Education");
            }
        }

        private List<tblEducation> educatioList;

        public List<tblEducation> EducationList
        {
            get { return educatioList; }
            set
            {
                educatioList = value;
                OnPropertyChanged("EudcationList");
            }
        }

        private tblSector sector;

        public tblSector Sector
        {
            get { return sector; }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private List<tblSector> sectorList;

        public List<tblSector> SectorList
        {
            get { return sectorList; }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
            }
        }

        private tblPosition position;

        public tblPosition Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        private List<tblPosition> positionList;

        public List<tblPosition> PositionList
        {
            get { return positionList; }
            set
            {
                positionList = value;
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
            editEmp.Close();
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
        /// <summary>
        /// Basic edit method
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    tblUser user = (from r in context.tblUsers where r.UserId == User.UserId select r).FirstOrDefault();
                    user.Firstname = User.Firstname;
                    user.Lastname = User.Lastname;
                    user.JMBG = User.JMBG;
                    user.Username = User.Username;
                    user.Pasword = User.Pasword;
                    user.Place = User.Place;
                    user.GenderID = Gender.GenderID;
                    user.MariageID = Married.MarriedID;
                    user.UserId = User.UserId;
                    context.SaveChanges();
                    if (tools.ValiationJMBG(User.JMBG) == true )
                    {

                        tblEmploye newEmploye = (from r in context.tblEmployes where r.EmployeID == User.EmployeID select r).FirstOrDefault();
                        newEmploye.UserID = User.UserId;
                        newEmploye.ExperienceYear = User.ExperienceYear;
                        if (Position.PoisitionName != null)
                        {
                            newEmploye.PositionID = Position.PositionID;
                        }
                        newEmploye.SectorID = Sector.SectorID;
                        newEmploye.EducationID = Education.EducationID;
                        context.SaveChanges();
                        MessageBox.Show("Employe is updated");
                        //Update = true;                          
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
            if (String.IsNullOrEmpty(User.Firstname) || String.IsNullOrEmpty(User.Lastname) || String.IsNullOrEmpty(User.JMBG) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Place) || String.IsNullOrEmpty(Gender.Gender) || String.IsNullOrEmpty(Married.MarriedStatus) || String.IsNullOrEmpty(Sector.SectorName) || String.IsNullOrEmpty(Education.Education) || String.IsNullOrEmpty(User.ExperienceYear.ToString()))
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
