using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace DBM2
{
    class RowNumberToColor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Object item = values[0];
            DataGrid grid = values[1] as DataGrid;

            int index = grid.Items.IndexOf(item);
            if (index < 0)
            {
                return null;
            }
            if(int.Parse(ImportVM.Inst.SkipRows)>index+1)
            {
                return Brushes.LightGray;
            }
            return Brushes.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
