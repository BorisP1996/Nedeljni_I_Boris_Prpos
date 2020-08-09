using System;
using System.Collections.Generic;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class CreateSectorViewModel:ViewModelBase
    {
        CreateSector createSec;
        Entity context = new Entity();
        Tools tools = new Tools();

        public CreateSectorViewModel(CreateSector createSecOpen)
        {
            createSec = createSecOpen;
            Sector = new tblSector();
        }

        private bool update;

        public bool Update
        {
            get { return update; }
            set { update = value; }
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

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveSectorExecute(), param => CanSaveSectorExecute());
                }
                return save;
            }
        }
        /// <summary>
        /// Sector list has capacity of 15 slots,after that it is not possible to create new sector...also name must be unique
        /// </summary>
        private void SaveSectorExecute()
        {
            try
            {
                List<tblSector> sectorList = new List<tblSector>();
                sectorList = tools.GetSector();

                if (sectorList.Count < 15 && tools.UniqueSector(Sector.SectorName)==true)
                {
                    tblSector newSector = new tblSector();
                    newSector.SectorName = Sector.SectorName;
                    newSector.SectorDesc = Sector.SectorDesc;
                    context.tblSectors.Add(newSector);
                    context.SaveChanges();
                    MessageBox.Show("Sector is saved");
                    Update = true;
                }
                else if (tools.UniqueSector(Sector.SectorName) == false)
                {
                    MessageBox.Show("Sector name already exists.");
                }
                else 
                {
                    MessageBox.Show("You have reached maximum sector capacity (15).\nIn order to create new sector, please delete one first. ");
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveSectorExecute()
        {
            if (String.IsNullOrEmpty(Sector.SectorName))
            {
                return false;
            }
            else
            {
                return true;
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
            createSec.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

    }
}
