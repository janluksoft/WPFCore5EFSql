using System;

namespace WpfApp.ViewModel
{
    ///<summary>The class creates a ConnectionString for the indicated server</summary>
    public class CConnString
    {
        public CConnString()
        {
        }

      #region - ConnectionString ----------------------

        public string jConnectionStringS
        (TypeServer xTServer, string xtbDataBaseName, string xtbUser, string xtbPass, string xComputer="", string xServer="")
        {
            string asCString = "";
            if (xTServer == TypeServer.ServerPostgres)
            {
                asCString = jConnectionStringToPostgres(xtbDataBaseName, xtbUser, xtbPass);
            }
            else if (xTServer == TypeServer.ServerSQL)
            {
                asCString = jConnectionStringToSQLServer
                    (xComputer, xServer, xtbDataBaseName, xtbUser, xtbPass);
            }

            return (asCString);
        }

        public string jConnectionStringToPostgres
            (string xtbDataBaseName, string xtbUser, string xtbPass)
        {   
            string tbHost = "localhost";
            string tbPort = "5432";
            string tbUser = xtbUser;//"Mark";
            string tbPass = xtbPass;//"abcde";
            string tbDataBaseName = xtbDataBaseName;//"dbMark";

            string ast = $"server={tbHost};port={tbPort};User Id={tbUser};" +
                $"Password={tbPass};Database={tbDataBaseName};";

            string connstring = String.Format(ast);

            return (connstring);
        }

        public string jConnectionStringToSQLServer
            (string xComputer, string xServer, string xtbDataBaseName, string xtbUser, string xtbPass)
        {
            string tbUser = xtbUser;//"sa";
            string tbPass = xtbPass;//"abcde";
            string tbDataBaseName = xtbDataBaseName;//"dbMark";

            string tbComputer = xComputer; //DESKTOP-BLYUDRB
            string tbServer = xServer; //SQLEXPRESS

            string sConnSql = $"Data Source={tbComputer}\\{tbServer};Initial Catalog={tbDataBaseName};" +
                $"User ID={tbUser};Password={tbPass};";

            string connstring = String.Format(sConnSql);
            return (connstring);
        }

      #endregion -------------------------
    }

}
