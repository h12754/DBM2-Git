using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace DBM2.Importers
//{
//    class Mappings
//    {
//        const string PricesImporter = "Цены на комплектующие";
//        const string DelImporter = "На удаление/аннулирование";


//        public static Dictionary<string,Dictionary<string,string>> Maps
//        {
//            get
//            {
//                if(_Maps is null)
//                {
//                    _Maps = new Dictionary<string, Dictionary<string, string>>();
//                    // импорт цен

//                    var  PriceMap = new Dictionary<string, string>();
//                    _Maps.Add(PricesImporter, PriceMap);
//                    PriceMap.Add("ArticleNumber", "Заказной код");
//                    PriceMap.Add("CompName", "Наименование");
//                    PriceMap.Add("", "Базовая цена");
//                    PriceMap.Add("", "Скидка, %");
//                    PriceMap.Add("", "Товарная группа");
//                    PriceMap.Add("", "Срок поставки, дней");
//                    PriceMap.Add("", "Класс 1");
//                    PriceMap.Add("", "Класс 2");
//                    PriceMap.Add("", "Примечание");
//                    // импорт на удаление/аннулирование

//                    var DelMap = new Dictionary<string, string>();
//                    _Maps.Add(DelImporter, DelMap);
//                    DelMap.Add("", "Производитель");
//                    DelMap.Add("", "Заказной код");
                    
//                }


//                return _Maps;
//            }
//        }
//        private static Dictionary<string, Dictionary<string, string>> _Maps;



//        public static List<string> MapsNames
//        {
//             get
//            {
//                List<string> lst = Maps.Keys.ToList();
//                lst.Sort();
//                return lst;
//            }
//        }
     




//    }
//}
