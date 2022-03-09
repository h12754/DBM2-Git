using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBM2
{
    class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DBM2.Model MyModel
        {
            get
            {
                return DBM2.Model.Inst;
            }
        }



        public UserControl DialogContent
        {
            get
            {
                if(_DialogContent==null)
                {
                    _DialogContent = new WaitCtrl() { DataContext = WaitDialogMdl };
                }
                return _DialogContent;
            }
        }
         public UserControl _DialogContent;

        public WaitCtrlModel WaitDialogMdl
        {
            get
            {
                if (_WaitMdl == null)
                {
                    _WaitMdl = new WaitCtrlModel();
                }
                return _WaitMdl;
            }
        }
        private WaitCtrlModel _WaitMdl;


        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    } 
    }
}
