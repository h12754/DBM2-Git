using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DBM2
{
    class ColNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //get the grid and the item
            string TargetColName = values[0] as string;
            DataGridColumnHeader TargetGrid1 = values[1] as DataGridColumnHeader;
            //DataGrid TargetGrid = values[1] as DataGrid;
            DataGrid SrcGrid = values[2] as DataGrid;

            //foreach(DataGridColumn i in grid.Columns)
            //{
            if (TargetGrid1 != null && SrcGrid != null && TargetColName != null)
            {


                return SrcGrid.Columns[TargetGrid1.TabIndex].ActualWidth;
            }
            else
            {
                return null;
            }

            //}

            //double g = 150;
            //return g;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
