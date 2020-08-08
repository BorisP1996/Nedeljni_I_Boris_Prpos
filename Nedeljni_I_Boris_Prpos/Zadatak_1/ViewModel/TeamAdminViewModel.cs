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
                MessageBox.Show("Del emp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
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
                MessageBox.Show("edit emp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
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
                MessageBox.Show("edit man");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
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
        private void DeleteManExecute()
        {
            try
            {
                MessageBox.Show("delete man");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
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
