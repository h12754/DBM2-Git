using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DBM2
{
    class CBItemColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          //  var TargetCb = values[0];
            //    ComboBoxItem TargetCb=  values[0] as ComboBoxItem;
            string TargetItemName = values[0] as string;


            if (ImportVM.Inst.lReqDescr.Contains(TargetItemName))
            {
                return Brushes.DarkOrange;
            }
            else
            {
                return Brushes.White;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
