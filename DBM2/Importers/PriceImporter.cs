using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBM2.Importers
{
    class PriceImporter:BaseImporter
    {
        //PriceImporter()
        // : base()
        //{

        //}

      public  PriceImporter()
            : base()
        {

            dAttMapping.Add("ArticleNumber", "");
            dAttMapping.Add("BasePrice", "");
            dAttMapping.Add("Discount", "");
            dAttMapping.Add("DiscGroup", "");
            dAttMapping.Add("DeliveryTimeInDays", "");
            dAttMapping.Add(this.VendorAtt, "");

            lReqAtt.Add("ArticleNumber");
           

            //     MappingDict.Add("BasePrice", "");
        }


        // Человеческое имя
        public override string HumanDescription
        {
            get
            {
                return "Обновление цен (действ.)";
            }
        }


    }
}
