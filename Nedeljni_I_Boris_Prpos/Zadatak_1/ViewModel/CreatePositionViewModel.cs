using System;
using Zadatak_1.Methods;
using Zadatak_1.Model;
using Zadatak_1.View;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class CreatePositionViewModel:ViewModelBase
    {
        CreatePosition createPos;
        Tools tool = new Tools();
        Entity context = new Entity();

        public CreatePositionViewModel(CreatePosition createPosOpen)
        {
            createPos = createPosOpen;
            Position = new tblPosition();
        }


        private tblPosition position;

        public tblPosition Position
        {
            get { return position; }
            set { position = value;
                OnPropertyChanged("Position");
            }
        }


        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SavePositionExecute(), param => CanSavePositionExecute());
                }
                return save;
            }
        }
        /// <summary>
        /// Create new position, name must be unique
        /// </summary>
        private void SavePositionExecute()
        {
            try
            {
                tblPosition newPosition = new tblPosition();
                newPosition.PoisitionName = Position.PoisitionName;
                newPosition.PoisitionDesc = Position.PoisitionDesc;
                if (tool.UniquePosition(newPosition.PoisitionName)==true)
                {
                    context.tblPositions.Add(newPosition);
                    context.SaveChanges();
                    MessageBox.Show("Position is saved.");
                }
                else
                {
                    MessageBox.Show("Position name must be unique");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSavePositionExecute()
        {
            if (String.IsNullOrEmpty(Position.PoisitionName))
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
            createPos.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
    }
}
