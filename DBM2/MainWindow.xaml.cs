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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using LightPropertyViewer;
using Logging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DBM2.XAML;

namespace DBM2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RawImport rwimp = null;

        public  DBM2.Model MyModel
        {
            get
            {
                return DBM2.Model.Inst;
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MyModel;
           Logger.Instance.Setup(LogBox);
            this.Closing += this.CloseRawReader;
      
        }

       

        public void OpenRawReader(object sender, RoutedEventArgs e)
        {
            if(rwimp==null)
            {
                rwimp = new RawImport() { DataContext=ImportVM.Inst};
               // rwimp.PropertyChanged += reevent;
            }
           
                rwimp.Visibility = Visibility.Visible;
        }

        public  void CloseRawReader(object sender, CancelEventArgs e)
        {
            if (rwimp != null)
            {
                rwimp.Close();
                rwimp = null;
            }
        }




        //public void OpenTst(object sender, RoutedEventArgs e)
        //{

        //    var rwimp = new Window1();
        //        // rwimp.PropertyChanged += reevent;
       

        //    rwimp.Visibility = Visibility.Visible;
        //}






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
