using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace DBM2
{
    class WaitCtrlModel : BaseVM
    {


        public System.Windows.Visibility CnvVis
        {
            get
            {
                return _CnvVis;
            }
            set
            {
                _CnvVis = value;
                 Value = 0;
                 OnPropertyChanged(nameof(CnvVis));
            }
        }

        private System.Windows.Visibility _CnvVis = System.Windows.Visibility.Hidden;



        private string _Headline;
        private double _Maximum;
        private double _Value;
        private string defHeadline = "Идет загрузка.";
        public Action Act { get; set; }

        public WaitCtrlModel()
           {
            Headline = defHeadline;
           }

       public WaitCtrlModel(string _h)
         {
            if(_h=="" | _h==null)
            {
                Headline = defHeadline;
            }
            Headline = _h;
         }


        public WaitCtrlModel(string _h, Action _a)
        {
            if (_h == "" | _h == null)
            {
                Headline = defHeadline;
            }
            Headline = _h;
            Act = _a;
        }


        public string Headline
        {
            get
            {
                return _Headline; // + Constants.vbCrLf + defHeadline;
            }
            set
            {
                _Headline = value;
                OnPropertyChanged(nameof(Headline));
            }
        }


   

   public void OnceGrowValue()
        {
            Value += 1;
        }



        public bool  IsIndeterminate
        {
            get
            {
                return Maximum == 0;
            }
               
        }


        public double Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                OnPropertyChanged(nameof(Maximum));
                OnPropertyChanged(nameof(IsIndeterminate));
            }
        }

        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = Value;
                OnPropertyChanged(nameof(Value));
            }
        }

    }
}
