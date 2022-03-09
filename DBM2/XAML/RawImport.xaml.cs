using System;
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
using ExlDBReader;
using DBM2.Importers;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace DBM2
{
    /// <summary>
    /// Логика взаимодействия для RawImport.xaml
    /// </summary>
    public partial class RawImport : Window
    {
        public DBM2.Model MyModel
        {
            get
            {
                return DBM2.Model.Inst;
            }
        }

        public RawImport()
        {
            InitializeComponent();
            DataContext = ImportVM.Inst;
           //ImportVM.Inst.PropertyChanged += ViewModel_DG_Changed;
         
        }

    }
}
