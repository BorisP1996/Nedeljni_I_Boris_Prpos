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
    class SystemAdminViewModel : ViewModelBase
    {
        Entity context = new Entity();
        SystemAdmin sysAdmin;
        Tools tool = new Tools();

        public SystemAdminViewModel(SystemAdmin sysAdminOpen)
        {
            sysAdmin = sysAdminOpen;
            Position = new tblPosition();
            Sector = new tblSector();
            SectorList = tool.GetSector();
        }

        private tblPosition position;

        public tblPosition Position
        {
            get { return position; }
            set { position = value;
                OnPropertyChanged("Position");
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

        #region Command
        private ICommand createPosition;
        public ICommand CreatePosition
        {
            get
            {
                if (createPosition == null)
                {
                    createPosition = new RelayCommand(param => CreatePositionExecute(), param => CanCreatePositionExecute());
                }
                return createPosition;
            }
        }
        /// <summary>
        /// Openn window to create position
        /// </summary>
        private void CreatePositionExecute()
        {
            try
            {
                CreatePosition createPos = new CreatePosition();
                createPos.ShowDialog();
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreatePositionExecute()
        {
            return true;
        }
        private ICommand createSector;
        public ICommand CreateSector
        {
            get
            {
                if (createSector == null)
                {
                    createSector = new RelayCommand(param => CreateSectorExecute(), param => CanCreateSectorExecute());
                }
                return createSector;
            }
        }
        /// <summary>
        /// Open window to create sector
        /// </summary>
        private void CreateSectorExecute()
        {
            try
            {
                CreateSector createSector = new CreateSector();
                createSector.ShowDialog();
                if ((createSector.DataContext as CreateSectorViewModel).Update == true)
                {
                    SectorList = tool.GetSector();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateSectorExecute()
        {
            return true;
        }
        private ICommand deleteSector;
        public ICommand DeleteSector
        {
            get
            {
                if (deleteSector == null)
                {
                    deleteSector = new RelayCommand(param => DeleteSectorExecute(), param => CanDeleteSectorExecute());
                }
                return deleteSector;
            }
        }
        /// <summary>
        /// Button inserted into table, deletes sector
        /// </summary>
        private void DeleteSectorExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete sector?", "Warning", btnMessageBox,icnMessageBox);

                if (resultMessageBox==MessageBoxResult.Yes)
                {
                    tblSector viaSector = (from r in context.tblSectors where r.SectorID == Sector.SectorID select r).FirstOrDefault();

                    List<tblEmploye> employeList = context.tblEmployes.ToList();

                    foreach (tblEmploye item in employeList)
                    {
                        //if employe has deleted sector, he gets default sector instead
                        if (item.SectorID==Sector.SectorID)
                        {
                            tblSector defaultSector = (from r in context.tblSectors where r.SectorName == "Default" select r).FirstOrDefault();
                            item.SectorID = defaultSector.SectorID;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    context.tblSectors.Remove(viaSector);
                    context.SaveChanges();
                    SectorList = tool.GetSector();
                    //this is poenta
                    MessageBox.Show("Sector is deleted\nAll employes that where in this sector are now assigned in default sector.");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteSectorExecute()
        {
            if (Sector!=null)
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
            sysAdmin.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion

    }
}
