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
    class CreatePositionViewModel:ViewModelBase
    {
        CreatePosition createPos;

        public CreatePositionViewModel(CreatePosition createPosOpen)
        {
            createPos = createPosOpen;
        }
    }
}
