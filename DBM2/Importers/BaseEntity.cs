using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBM2.Importers
{
    class BaseEntity
    {
        // Нельзя использовать
        public bool IsBad
        {
            get
            {
                if (IsBadReason != "")
                    return true;
                else
                    return false;
            }
        }

        public string IsBadReason
        {
            get
            {
                  return "";
            }
            set
            {
                _isbadreason = value;
            }
        }
                private string _isbadreason;
        public DataRow SourceRow
        {
            get{ return _sourcerow; }
            set{ _sourcerow = value; }
        }
        private DataRow _sourcerow;

        public Dictionary<string, Object> KeyValDict= new Dictionary<string, Object>();
        public Dictionary<int, string> MapDict = new Dictionary<int, string>();



        // Конструкторы
        public  BaseEntity()
        {

        }

        public BaseEntity(DataRow drow, Dictionary<string,string> _mapdict)
        {
            
        }
        public BaseEntity(DataRow drow, Dictionary<int, string> _mapdict)
        {
            SourceRow = drow;
            MapDict = _mapdict;
        }

    }
}
