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

namespace Zadatak_1.ViewModel
{
    class TeamAdminViewModel:ViewModelBase
    {
        TeamAdmin teamAdmin;
        Entity context = new Entity();
        Tools tool = new Tools();

        public TeamAdminViewModel(TeamAdmin teamAdmOpen)
        {
            teamAdmin = teamAdmOpen;
            ListEmploye = tool.GetVwEmploye();
            ListManager = tool.GetVwManager();
        }

        private vwEmploye viewEmploye;

        public vwEmploye ViewEmploye
        {
            get { return viewEmploye; }
            set { viewEmploye = value;
                OnPropertyChanged("ViewEmploye");
            }
        }

        private List<vwEmploye> listEmploye;

        public List<vwEmploye> ListEmploye
        {
            get { return listEmploye; }
            set { listEmploye = value;
                OnPropertyChanged("ListEmploye");
            }
        }

        private vwManager viewManager;

        public vwManager ViewManager
        {
            get { return viewManager; }
            set { viewManager = value;
                OnPropertyChanged("ViewManager");
            }
        }

        private List<vwManager> listManager;

        public List<vwManager> ListManager
        {
            get { return listManager; }
            set { listManager = value;
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
            teamAdmin.Close();
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
        private ICommand editMan;
        public ICommand EditMan
        {
            get
            {
                if (editMan == null)
                {
                    editMan = new RelayCommand(param => EditManExecute(), param => CanEditManExecute());
                }
                return editMan;
            }
        }
        private void EditManExecute()
        {
            try
            {
                EditManager editMan = new EditManager(ViewManager);
                editMan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }
        private bool CanEditManExecute()
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
        private ICommand deleteMan;
        public ICommand DeleteMan
        {
            get
            {
                if (deleteMan == null)
                {
                    deleteMan = new RelayCommand(param => DeleteManExecute(), param => CanDeleteManExecute());
                }
                return deleteMan;
            }
        }
        /// <summary>
        /// if tehre are users which refer to manager, they must be deleted first, and than the manager
        /// </summary>
        private void DeleteManExecute()
        {
            try
            {
                int count = 0;
                List<tblEmploye> employeList = context.tblEmployes.ToList();

                foreach (tblEmploye item in employeList)
                {
                    if (item.ManagerID == ViewManager.ManagerID)
                    {
                        MessageBoxButton btnMessageBox1 = MessageBoxButton.YesNo;
                        MessageBoxImage icnMessageBox1 = MessageBoxImage.Warning;

                        MessageBoxResult resultMessageBox1 = MessageBox.Show("Are you sure you want to delete manager?\nYou also have to delete every employe that refers to this manager", "Warning", btnMessageBox1, icnMessageBox1);

                        if (resultMessageBox1 == MessageBoxResult.Yes)
                        {
                            foreach (tblEmploye item1 in employeList)
                            {
                                if (item1.ManagerID == ViewManager.ManagerID)
                                {
                                    context.tblEmployes.Remove(item1);
                                    tblUser viaUser = (from r in context.tblUsers where r.UserId == item1.UserID select r).FirstOrDefault();
                                    context.tblUsers.Remove(viaUser);
                                    count++;
                                    context.SaveChanges();
                                }
                            }
                        }
                        tblManager viaManager = (from r in context.tblManagers where r.UserID == ViewManager.UserId select r).FirstOrDefault();
                        context.tblManagers.Remove(viaManager);
                        tblUser viaUser1 = (from r in context.tblUsers where r.UserId == viaManager.UserID select r).FirstOrDefault();
                        context.tblUsers.Remove(viaUser1);
                        context.SaveChanges();
                        ListManager = tool.GetVwManager();
                        ListEmploye = tool.GetVwEmploye();
                        MessageBox.Show("Manager is deleted.\nNumber of employes deleted: " + count);
                    }
                }
                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete manager?","Warning", btnMessageBox, icnMessageBox);

                    if (resultMessageBox == MessageBoxResult.Yes)
                    {
                        tblManager viaManager = (from r in context.tblManagers where r.UserID == ViewManager.UserId select r).FirstOrDefault();
                        context.tblManagers.Remove(viaManager);
                        tblUser viaUser1 = (from r in context.tblUsers where r.UserId == viaManager.UserID select r).FirstOrDefault();
                        context.tblUsers.Remove(viaUser1);
                        context.SaveChanges();
                        ListManager = tool.GetVwManager();
                        MessageBox.Show("Manager is deleted.");
                    }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }
        private bool CanDeleteManExecute()
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
