using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    /// <summary>The class that merges two classes</summary>
    public class CPeopleMain
    {
        public CPeopleMain()
        {
        }

        public CPeopleDBContext DBContextCreate(TypeServer xTServer, string xSchema, string xTableName,
            string xtbDataBaseName, string xtbUser, string xtbPass, string xComputer = "", string xServer = "")
        {
            CConnString cCStr = new CConnString();
            string asConnString = cCStr.jConnectionStringS(xTServer, xtbDataBaseName, 
                xtbUser, xtbPass, xComputer, xServer);

            return (new CPeopleDBContext(asConnString, xSchema, xTableName));
        }

    }
}
