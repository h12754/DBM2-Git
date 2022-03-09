using System;
using System.Collections.Generic;
using System.Data;
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

namespace DBM2.XAML
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataTable dt = new();


        public Window1()
        {
            InitializeComponent();
           
            for (int i = 0; i< 20; i++)
            {
                dt.Columns.Add(new DataColumn("Col" + i));
            }

 BindDG1.ItemsSource = dt.AsDataView();
            BindDG2.ItemsSource = dt.AsDataView();



        }
    }
}
