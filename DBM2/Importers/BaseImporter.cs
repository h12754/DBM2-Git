using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_TKP;


namespace DBM2.Importers
{
    public class BaseImporter
    {
        public BaseImporter()
        {
            create_lists();
        }

        // Человеческое имя
        public virtual string HumanDescription
        {
            get
            {
                return "Базовый импортер";
            }
        }
        // строка ND
        public static string NDItem
        {
            get
            {
                return "...";
            }
        }

        // Словарь привязок к аттрибутам аттр-столбец
        public virtual Dictionary<string, string> dAttMapping
        {
            get
            {
                if(dCol_toDescrBind.Count>0)
                {
                    foreach(KeyValuePair<string, string> i in dCol_toDescrBind)
                    {
                        if(_dAttMapping.Keys.Contains(dDescrAtt[i.Value]))
                        {
                            _dAttMapping[dDescrAtt[i.Value]] = i.Value;
                        }
                    }
                }

               return _dAttMapping;
            }

        }
        private Dictionary<string, string> _dAttMapping = new Dictionary<string, string>();
       // словарь названий атрибутов данного импортера
        public  Dictionary<string, string> dDescrAtt
        {
            get
            {
                if(_dDescrAtt==null)
                {
                    _dDescrAtt = new();
                    foreach (string i in _dAttMapping.Keys.ToList())
                    {
                       _dDescrAtt.Add(EFConn.GetAttsDescr(i), i);
                    }
                }
                return _dDescrAtt;
            }
        }
        private Dictionary<string, string> _dDescrAtt; // = new Dictionary<string, string>();
        // лист с названиями атрибутов для биндинга. Заполняется void'ом
        public List<string> lDescr=new();
        // лист с названиями обязательных атрибутов для биндинга
        public List<string> lReqDescr
        {
            get
            {
                if(_lReqDescr.Count!= _lReqAtt.Count)
                {
                 _lReqDescr.Clear();
                 foreach (string i in lReqAtt)
                   {
                    _lReqDescr.Add(dDescrAtt[i]); // На момент запроса словарь уже есть. Ну или тут и заполнится.
                   }
                }
                return _lReqDescr;
            }
        }
        private List<string> _lReqDescr = new();
        // лист обязательных аттрибутов
        public virtual List<string> lReqAtt
        {
            get
            {
               return _lReqAtt;
            }
        }
        private List<string> _lReqAtt = new();

        // Словарь привязок столбцов к названиям
        public Dictionary<string, string> dCol_toDescrBind { get; set; } = new();
       
        // привязанные наименования аттрибутов

        public List<string> lUsedAtts
        {
            get
            {
                List<string> lst = dCol_toDescrBind.Values.ToList();
                lst.RemoveAll(NullContains);
                return lst;
            }
        }
        private static bool NullContains(String s)
        {
            return s == null | s == "";
        }

        public string SelectedVendor
        {
            get
            {
                return _SelectedVendor;
            }
            set
            {
                _SelectedVendor = value;
            }
        }

        private string _SelectedVendor = null;

        //  производитель выбран аттрибутом
        public virtual bool MultVendors
        {
            get
            {
                if (dAttMapping.Keys.Contains(VendorAtt))
                {
                    if (dAttMapping[VendorAtt] != "")
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        // аттрибут производителя
        public string VendorAtt = "Supplier";

        public void create_lists()
        {
            lDescr.AddRange(dDescrAtt.Keys.ToList());
            lDescr.Add(NDItem);
            lDescr.Sort();



            }

        }




    }
}
