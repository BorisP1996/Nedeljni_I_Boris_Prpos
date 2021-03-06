﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for AssignDuty.xaml
    /// </summary>
    public partial class AssignDuty : Window
    {
        public AssignDuty(vwManager viewManager)
        {
            InitializeComponent();
            this.DataContext = new AssignDutyViewModel(this, viewManager);
        }
    }
}
