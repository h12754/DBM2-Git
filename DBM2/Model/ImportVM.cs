using DBM2.Importers;
using ExlDBReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DBM2
{
    class ImportVM: BaseVM
    {
         #region Моделька Instance
        public static ImportVM Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ImportVM();
                }
                return _inst;
            }
        }
        private static ImportVM _inst;
        #endregion
        #region ForBinding
        public string Caption
        {
            get
            {
                if(FilePath == "")
                {
                    return "Импорт исходного файла Excel";
                }
                else
                {
                    return "Открыт: " + FileName;
                }
            }
        }
        // Путь к файлу
        public string FilePath
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(FilePath));
                OnPropertyChanged(nameof(FileName));
                OnPropertyChanged(nameof(Msg_txt));
            }
        }
        private string _path = "";
        public string FileName
        {
            get
            {
                if (FilePath != "")
                {
                    return System.IO.Path.GetFileName(FilePath);
                }
                return "";
            }
        }
        // Пропуск строк
        public string SkipRows
        {
            get
            {
                return (_SkipRows).ToString();
            }
            set
            {
                int val = 0;
               if( int.TryParse(value.ToString(),  out val))
                    {
                    if(val<1)
                    {
                        _SkipRows = 1;
                    }
                    else
                    {
                        _SkipRows = val;
                    }
                    
                }
               else
                {
                _SkipRows = 1;
                }
                OnPropertyChanged(nameof(ImportSourceAsDW));
            }
        }
        public int _SkipRows=2;
      
        // читалка raw
        public ExlOleReader Reader;
        // центральная свалка строк
        public DataTable ImportSource
        {
            get
            {
                //if (Model.Inst.CurrentImporter == null)
                //{
                return _ImportSource;
                //}
                //else
                //{
                //    return Model.Inst.CurrentImporter.DTSource;
                //}
            }
            set
            {
                _ImportSource = value;
               
                OnPropertyChanged(nameof(ImportSource));
                OnPropertyChanged(nameof(ImportSourceAsDW));
            }

        }
        private DataTable _ImportSource;
        // конвертация в DataView для совместимости с DG
        public DataView ImportSourceAsDW
        {
            get
            {
                 if (_ImportSource != null)
                    {
                        _ImportSourceAsDW = _ImportSource.AsDataView();
                    }
                  
                return _ImportSourceAsDW;
            }
        }
        private DataView _ImportSourceAsDW;

        // Листы в Экселе
        public List<string> ExlListNames
        {
            get
            {
                return _ExlListNames;
            }
            set
            {
                _ExlListNames = value;
                OnPropertyChanged(nameof(ExlListNames));
            }
        }
        List<string> _ExlListNames;

        // Выбранный лист 
        public string SelectedListName
        {
            get
            {
                return _SelectedListName;
            }
            set
            {
                _SelectedListName = value;
                GetTableByName();
                OnPropertyChanged(nameof(Msg_txt));
            }
        }
        private string _SelectedListName;
        // Иконка на кнопку
        public BitmapImage Msg_img
        {
            get
            {
                return wpfutils.GetBitmapImageFromUri(Msg_img_type);
            }
        }
        private string Msg_img_type = "info1.png";
        // Текст на кнопку
        public string Msg_txt
        {
            get
            {
                string msg = "";
                ImportBtnEnabled = false;
                if (FilePath=="")
                {
                    Msg_img_type = "info2.png";
                    msg = "Выберите файл для импорта";
                                 }
                else if (SelectedListName == null)
                {
                    Msg_img_type = "info2.png";
                    msg = "Нужно выбрать лист импорта";
                }
                else if (CurrentImporter == null)
                {
                    Msg_img_type = "warn1.png";
                    msg = "Импортер не выбран";
                }
                else if (UsedAtts.Count == 0 || ReqAttsUsed == false)
                {
                    Msg_img_type = "warn1.png";
                    msg = "Не назначены обязательные столбцы для импорта";
                }
              
                else
                {
                    Msg_img_type = "ok2.png";
                    msg = "Импортировать таблицу";
                    ImportBtnEnabled = true;
                }

                OnPropertyChanged(nameof(Msg_img));
                OnPropertyChanged(nameof(ImportBtnEnabled));
                return msg;
            }
        }

        // Все обязательные аттрибуты используются
        public bool ReqAttsUsed
        {
            get
            {
                List<string> bindedAtts = dAttsBind.Values.ToList();

                foreach(string i in ReqAttLst)
                {
                    if(!bindedAtts.Contains(i))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        // Импорт разрешен
        public bool ImportBtnEnabled { get; set; }

        // Выбор атрибутов включен
        public bool AttCBEnabled
        {
            get
            {
            if(CurrentImporter!=null)
                {
                    return true;
                }
                return false;
            }
        }
        // Список импортеров

        private Dictionary<string, BaseImporter> importers_d
        {
            get
            {
                if (_importers_d == null)
                {
                    _importers_d = new Dictionary<string, BaseImporter>();
                    IEnumerable<Type> subclasses = System.Reflection.Assembly.GetAssembly(typeof(BaseImporter)).GetTypes().Where(t => t.IsSubclassOf(typeof(BaseImporter)));
                    foreach (Type t in subclasses)
                    {
                        BaseImporter inst = (BaseImporter)Activator.CreateInstance(t);
                        _importers_d.Add(inst.HumanDescription, inst);
                    }
                }
                return _importers_d;
            }
        }
        private Dictionary<string, BaseImporter> _importers_d = null;
      // Список для binding
        public List<string> importersNames
        {
            get
            {
                if(_importersNames==null)
                {
                 _importersNames = new List<string>();
                _importersNames.Add("");
                _importersNames.AddRange(importers_d.Keys.ToList());
                }
                return _importersNames;
            }
        }
        public List<string> _importersNames;

        // Текущий импортер - установка
        public string SetImporter
        {
            set
            {
                if (value != "" && importers_d.Keys.Contains(value))
                {
                    CurrentImporter = importers_d[value];
                }
                else
                {
                    CurrentImporter = null;
                }


            }
        }
        // Текущий импортер
        public BaseImporter CurrentImporter
        {
            get
            {
                return _CurrentImporter;
            }
            set
            {
                _CurrentImporter = value;
                OnPropertyChanged(nameof(Msg_txt));
                OnPropertyChanged(nameof(AttCBEnabled));
            }
        }
        private BaseImporter _CurrentImporter = null;

        public string CurrImporterName
        {
            get
            {
                if (CurrentImporter != null)
                {
                    return "Импортер: " + CurrentImporter.HumanDescription;
                }
                return "";
            }
            set
            {
                if (value != "" && importers_d.Keys.Contains(value))
                {
                    CurrentImporter = importers_d[value];
                }
                else
                {
                    CurrentImporter = null;
                }

                GenerateBasicAttLst();
                OnPropertyChanged(nameof(CurrentImporter));
                OnPropertyChanged(nameof(BasicAttLst));

            }
        }
      // производители (переадресация из осн модели)
        public List<string> vendorNames
        {
            get
            {
                return Model.Inst.vendorNames;
            }
        }

        // Словарь привязок 
        public Dictionary<string, string> dAttsBind { get; set; } = new();
     

        // использованные аттрибуты

        public List<string> UsedAtts
        {
            get
            {
                List<string> lst = dAttsBind.Values.ToList();
                lst.RemoveAll(NullContains);
                return lst;

            }
        }
        private static bool NullContains(String s)
        {

            return s == null | s=="";
        }

        public List<string> ReqAttLst
        {
            get
            {
                if(CurrentImporter!=null)
                {
                    return CurrentImporter.lReqAtt;
                }
                return new List<string>();
            }
        }
        private List<string> _ReqAttLst = new();

        public List<string> BasicAttLst
        {
            get
            {
               return _BasicAttLst;
            }
        }
        private List<string> _BasicAttLst=new();

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
        private string _SelectedVendor=null;


        #endregion

        #region Функции
        //// Человечные аттрибуты
        //public string GetAttsDescr(string a)
        //{
        //    if (a != null | a != "")
        //    {
        //        string d = EF_TKP.EFConn.AttDescrD[a];
        //        if (d != null | d != "")
        //        {
        //            return d;
        //        }
        //        return a;

        //    }
        //    else
        //    {
        //        return a;
        //    }
        //}
        // ФайлПикер)
        public void filePicker()
        {
            //  string _path="";
            OpenFileDialog fl = new OpenFileDialog();

            fl.Title = "Открыть файл";
            fl.Filter = "Таблица Excel| *.xls;*.xlsx;*.xlsm;*.cvs | Все файлы | *.* ";
            if (Properties.Settings.Default.LastFilePath != "")
            {
                if (System.IO.Directory.Exists(Properties.Settings.Default.LastFilePath))
                {
                    fl.InitialDirectory = Properties.Settings.Default.LastFilePath;
                }
                else
                {
                    fl.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                }
            }
            else
            {
                fl.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();

            }
            //  fl.ShowDialog();

            if (fl.ShowDialog() == true)
            {
               FilePath = fl.FileName;
                //  bool j=  File.Exists(fl.FileName);
                string dirpath = System.IO.Path.GetDirectoryName(FilePath);
                Properties.Settings.Default.LastFilePath = dirpath;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(FilePath));
                OnPropertyChanged(nameof(FileName));
                OnPropertyChanged(nameof(Caption));
            }
         //   return Model.Inst.FilePath;

        }

        public void Fill()
        {
            WaitDialogMdl.CnvVis = Visibility.Visible;
            WaitDialogMdl.Headline = "Читаем Excel...";
          Task task = Task.Run(() =>
           {
               Reader = new ExlOleReader(FilePath);
               ExlListNames = Reader.ExlListNames;
               WaitDialogMdl.CnvVis = Visibility.Hidden;
           });
          
            //  Reader = new ExlOleReader(FilePath);
            // ImportSource = Reader.dtTablesList;
           

        }
        
        // Запрос другой таблицы
        public void GetTableByName(string _name)
        {
            if (Reader != null)
            {
                WaitDialogMdl.CnvVis = Visibility.Visible;
                WaitDialogMdl.Headline = "Читаем таблицу...";
                Task task = Task.Run(() => 
                { 
                Reader.GetTableByName(_name);
                ImportSource = Reader.ReadedTable;
                    // Заново делаем словарь соответствия 
                dAttsBind = new();

                foreach(DataColumn dc in ImportSource.Columns)
                    {
                        dAttsBind.Add(dc.ColumnName, null);
                    }
                WaitDialogMdl.CnvVis = Visibility.Hidden;
                   // SyncNumColumns();
                    //SyncWidthColumns();
                }
                );
                            
            }
        }
        // Запрос другой таблицы
        public void GetTableByName()
        {
            if (Reader != null)
            {
                if(SelectedListName=="" | SelectedListName == null)
                {
                    return;
                }

                string tableRealName = Reader.ExlTableNamesDictionary[SelectedListName];
               GetTableByName(tableRealName);
                //ImportSource = Reader.ReadedTable;
                
            }
        }

        //
        public void GenerateBasicAttLst()
        {
            List<string> lst = new();
            if (CurrentImporter != null)
            {
                //  lst.Add("");
                BasicAttLst.Clear();

                foreach (string i in CurrentImporter.dAttMapping.Keys.ToList())
                {
                    BasicAttLst.Add(GetAttsDescr(i));

                }
                foreach (string i in CurrentImporter.lReqAtt)
                {
                    ReqAttLst.Add(GetAttsDescr(i));
                }
                               
                BasicAttLst.Add(NDItem);
                BasicAttLst.Sort();
            }
           
        }

        public string NDItem
        {
            get
            {
                return "...";
            }
        }

        #endregion

        #region Команды

        // Выбрать файл
        private RelayCommand _Open_xlsx;
        public RelayCommand Open_xlsx
        {
            get
            {
                return _Open_xlsx ??
                    (_Open_xlsx = new RelayCommand(obj =>
                    {
                        filePicker();
                        Fill();
                    }));
            }
        }
        // Открыть по пути
        private RelayCommand _Open_xlsx_byPath;
        public RelayCommand Open_xlsx_byPath
        {
            get
            {
                return _Open_xlsx_byPath ??
                    (_Open_xlsx_byPath = new RelayCommand(obj =>
                    {
                        Fill();
                    }));
            }
        }
     
        private RelayCommand _SetColName;
        public RelayCommand SetColName
        {
            get
            {
                return _SetColName ??
                    (_SetColName = new RelayCommand(obj =>
                    {
                        ComboBox cb = obj as ComboBox;
                        string str = "";

                        if(cb.SelectedItem!=null)
                        {
                            str = cb.SelectedItem.ToString();
                        }


                        //ищем DataGridColumn
                        DependencyObject c1 =VisualTreeHelper.GetParent(cb);
                        while (c1.GetType()!=typeof(DataGridColumnHeader))
                        {
                            c1 = VisualTreeHelper.GetParent(c1);
                        }
                        DataGridColumnHeader c5 = (DataGridColumnHeader)c1;

                        DataGridColumn d5 =  c5.Column;
                       
                        if(d5!=null && cb.SelectedItem != null)
                        {
                            Dictionary<string, string> dA = new();
                           dAttsBind[ d5.Header.ToString()] = str;
                           OnPropertyChanged(nameof(BasicAttLst));
                            OnPropertyChanged(nameof(Msg_txt));
                         //   cb.SelectedItem = str;

                        }
                                  }));
            }
        }

        private RelayCommand _doImport;
        public RelayCommand doImport
        {
            get
            {
                return _doImport ??
                    (_doImport = new RelayCommand(obj =>
                    {
                        //if (CurrentImporter != null && )
                        //{




                        //}
                                              
                    }));
            }
        }




        #endregion




    }
}
