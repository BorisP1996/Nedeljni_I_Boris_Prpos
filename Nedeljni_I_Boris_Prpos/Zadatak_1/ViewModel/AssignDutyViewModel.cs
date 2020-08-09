using System;
using System.Linq;
using Zadatak_1.Model;
using Zadatak_1.View;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class AssignDutyViewModel : ViewModelBase
    {
        AssignDuty assign;
        Entity context = new Entity();

        public AssignDutyViewModel(AssignDuty assignOpen,vwManager forwardedView)
        {
            assign = assignOpen;
            //catch manager that will have duty assigned
            VwManager = forwardedView;
        }
        private bool update;

        public bool Update
        {
            get { return update; }
            set { update = value; }
        }

        private vwManager vwManager;

        public vwManager VwManager
        {
            get { return vwManager; }
            set { vwManager = value; }
        }

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value;
                OnPropertyChanged("Level");
            }
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
        /// Give duty level to manager
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                tblManager viaManager = (from r in context.tblManagers where r.ManagerID == VwManager.ManagerID select r).FirstOrDefault();
                viaManager.DutyLevel = Level;
                if (Level==1 || Level ==2 || Level==3)
                {
                    context.SaveChanges();
                    MessageBox.Show("Duty level is assigned to manager");
                    Update = true;
                }
                else
                {
                    MessageBox.Show("Duty level can be 1,2 or 3");
                }
               
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (Level == 1 || Level ==2 || Level ==3 )
            {
                return true;
            }
            else
            {
                return false;
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
            assign.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

    }
}
