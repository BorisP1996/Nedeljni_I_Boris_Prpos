using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class LocalAdminViewModel : ViewModelBase
    {
        LocalAdmin localAdmin;
        Entity context = new Entity();
        Tools tool = new Tools();

        public LocalAdminViewModel(LocalAdmin localAdminOpen)
        {
            localAdmin = localAdminOpen;
            ListEmploye = tool.GetVwEmploye();
            ListManager = tool.GetVwManager();
        }

        private vwEmploye viewEmploye;

        public vwEmploye ViewEmploye
        {
            get { return viewEmploye; }
            set
            {
                viewEmploye = value;
                OnPropertyChanged("ViewEmploye");
            }
        }

        private List<vwEmploye> listEmploye;

        public List<vwEmploye> ListEmploye
        {
            get { return listEmploye; }
            set
            {
                listEmploye = value;
                OnPropertyChanged("ListEmploye");
            }
        }

        private vwManager viewManager;

        public vwManager ViewManager
        {
            get { return viewManager; }
            set
            {
                viewManager = value;
                OnPropertyChanged("ViewManager");
            }
        }

        private List<vwManager> listManager;

        public List<vwManager> ListManager
        {
            get { return listManager; }
            set
            {
                listManager = value;
                OnPropertyChanged("ListManager");
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
            localAdmin.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        private ICommand delEmp;
        public ICommand DelEmp
        {
            get
            {
                if (delEmp == null)
                {
                    delEmp = new RelayCommand(param => DelEmpExecute(), param => CanDelEmpExecute());
                }
                return delEmp;
            }
        }
        /// <summary>
        /// Click on this button deletes employe
        /// </summary>
        private void DelEmpExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete employe?", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    tblEmploye viaEmploye = (from r in context.tblEmployes where r.UserID == ViewEmploye.UserId select r).FirstOrDefault();
                    context.tblEmployes.Remove(viaEmploye);
                    tblUser viaUser = (from r in context.tblUsers where r.UserId == viaEmploye.UserID select r).FirstOrDefault();
                    context.tblUsers.Remove(viaUser);
                    context.SaveChanges();
                    MessageBox.Show("Employe is deleted");
                    context.SaveChanges();
                    ListEmploye = tool.GetVwEmploye();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private bool CanDelEmpExecute()
        {
            if (ViewEmploye == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand editEmp;
        public ICommand EditEmp
        {
            get
            {
                if (editEmp == null)
                {
                    editEmp = new RelayCommand(param => EditEmpExecute(), param => CanEditEmpExecute());
                }
                return editEmp;
            }
        }
        private void EditEmpExecute()
        {
            try
            {
                EditEmploye editEmploye = new EditEmploye(ViewEmploye);
                editEmploye.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private bool CanEditEmpExecute()
        {
            if (ViewEmploye == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Command that open window to assign duty level
        /// </summary>
        private ICommand assign;
        public ICommand Assign
        {
            get
            {
                if (assign == null)
                {
                    assign = new RelayCommand(param => AssignExecute(), param => CanAssignExecute());
                }
                return assign;
            }
        }
        private void AssignExecute()
        {
            try
            {
                AssignDuty assignDuty = new AssignDuty(viewManager);
                assignDuty.ShowDialog();
                if((assignDuty.DataContext as AssignDutyViewModel).Update == true)
                {
                    ListManager = tool.GetVwManager();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private bool CanAssignExecute()
        {
            if (ViewManager == null)
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
