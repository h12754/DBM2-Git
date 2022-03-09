using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Windows;
using ExlDBReader;
using Microsoft.Win32;
using System.IO;
using System.Runtime.CompilerServices;
using DBM2.Importers;

namespace DBM2
{
    public class Model : INotifyPropertyChanged
    {
        //string tmpconnstr = "Initial Catalog=tkpconf;Data Source=ones;User ID=tkpcwrite;Password=Wy7hv#kv;";

     

        // Моделька Instance
        public static Model Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new Model();
                }
                return _inst;
            }
        }
        private static Model _inst;
        // Производители
        public List<string> vendorNames
        {
            get
            {

              if(_vendorNames==null)
                {
                    _vendorNames = new List<string>();
                    _vendorNames.Add("");
                    _vendorNames.AddRange(EF_TKP.EFConn.VendorsList);
                }
              
       
                return _vendorNames;
            }
        }
        private List<string> _vendorNames;


        public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
