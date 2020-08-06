using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class CreateEmployeViewModel : ViewModelBase
    {
        CreateEmploye ce;

        public CreateEmployeViewModel(CreateEmploye ceOpen)
        {
            ce = ceOpen;
        }
    }
}
