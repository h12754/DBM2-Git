using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBM2.Importers
{
    class NewItemsImporter:BaseImporter
    {

        // Человеческое имя
        public override string HumanDescription
        {
            get
            {
                return "Импорт новых";
            }
        }


    }
}
