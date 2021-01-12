using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Drawing;
using WpfLibNice;
using WpfApp.ViewModel;
using System.Data;

namespace WpfApp.View
{
    // This Source Code Form is subject to the terms of the MIT License.
    // Copyright(C) Jan Luk. All Rights Reserved 2020.

    ///<summary> Main window class </summary>
    public partial class MainWindow : Window
    {
      #region - Variable & INI -----------------

        TypeServer cTServer;
        CPeopleDBContext ccontext;

        public MainWindow()
        {
            InitializeComponent();
            cTServer = TypeServer.ServerSQL;

            labTypeOfServer.Content = "Microsoft SQL Server";
            if (cTServer == TypeServer.ServerPostgres)
                labTypeOfServer.Content = "Postgres Server";

            labTable.Content = txtTableName.Text;
            jDataLoginIniToTextBox();
            tabControl1.SelectedItem = tbItem3;
        }
      #endregion ---------------

      #region - Operations of context ------------

        ///<summary>Function view: ReadTable </summary>
        public (bool, string) jReadTableFromDataBase()
        {
            bool bEndOk = false;
            string sinfo = "";

            try
            {
                ccontext = DBContextCreate();

                Binding abind = new Binding();
                cDataGrid1.ItemsSource = ccontext.dbPersons.ToList();

                //BindingSource bsource = new BindingSource();
                //bsource.DataSource = ccontext.dbPersons.ToList();
                //dGridPostgres.DataSource = null;
                //dGridPostgres.DataSource = bsource;

                //CTableFormat cTableFormat = new CTableFormat();
                //cTableFormat.FormatColumnGrid(ref dGridPostgres, 1);

                bEndOk = true;
                sinfo = $"A table was read from the database";
            }
            catch (Exception ex)
            {
                sinfo = ex.Message;
            }

            return (bEndOk, sinfo);
        }

        ///<summary>Function view: AddRow </summary>
        public (bool, string) jAddRowToDataBase()
        {
            bool bEndOk = false;
            string sinfo = "";

            try
            {
                ccontext = DBContextCreate();

                string asName = txtAName.Text; // "Mark";
                string asSurname = txtASurname.Text; // "Molano";
                float afAge = Convert.ToSingle(CheckComma(txtAAge.Text)); //24;
                string asCity = txtACity.Text; // "London";
                float afHeight = Convert.ToSingle(CheckComma(txtAHeight.Text)); //164.5f;

                ccontext.AddOnePerson(asName, asSurname, afAge, asCity, afHeight);
                bEndOk = true;
                sinfo = $"Added person ({asName} {asSurname}) to DataBase";
            }
            catch (Exception ex)
            {
                sinfo = ex.Message;
            }

            return (bEndOk, sinfo);
        }

        ///<summary>Function view: DeleteRow </summary>
        public (bool, string) jDeleteRowFromDataBase(int xiRow)
        {
            bool bEndOk = false;
            string sinfo = "";

            try
            {
                ccontext = DBContextCreate();
                ccontext.RemoveAlbum(xiRow);
                bEndOk = true;
                sinfo = $"Deleted Row ({xiRow}) from DataBase";
            }
            catch (Exception ex)
            {
                sinfo = ex.Message;
            }

            return (bEndOk, sinfo);
        }

        ///<summary>Function view: AddExampleRows </summary>
        public (bool, string) jAddExampleRowsToDataBase()
        {
            bool bEndOk = false;
            string sinfo = "";
            int iCount = 0;

            try
            {
                ccontext = DBContextCreate();
                iCount = ccontext.jAddExampleRowsToDataBase();
                sinfo = $"Added {iCount} persons to DataBase";
                bEndOk = true;
            }
            catch (Exception ex)
            {
                sinfo = ex.Message;
            }

            return (bEndOk, sinfo);
        }


      #endregion -------------

      #region - Prepare Connection ---------------

        ///<summary> Creates DBContext </summary>
        private CPeopleDBContext DBContextCreate()
        {
            string tbUser = txtUser.Text;//"sa";
            string tbSchema = txtSchema.Text; //"dbo"
            string tbTableName = txtTableName.Text;//"Sprinters";

            string tbPass = txtPass.Password;//"fgdgds";
            string tbDataBaseName = txtDataBase.Text;//"dbMark";

            string tbComputer = txtComp.Text; //DESKTOP-BLHJAVB
            string tbServer = txtServer.Text; //SQLEXPRESS

            CPeopleMain aPeopleMain = new CPeopleMain();

            return (aPeopleMain.DBContextCreate(cTServer, tbSchema, tbTableName, tbDataBaseName, 
                            tbUser, tbPass, tbComputer, tbServer));

            // Adnotation to the test procedure
            //CPeopleMain aPeopleMain = new CPeopleMain();
            //aPeopleMain.DBContextCreate(TypeServer.ServerSQL, "dbo", "Sprinters", "dbMark",
            //                "sa", "abcde", "PC-XEONE23", "SQLEXPRESS");
        }

        /// <summary> Initialization of login data on the selected server </summary>
        private void jDataLoginIniToTextBox()
        {
            string[,] stbLog = new string[,] {
                {"dbo", "Sprinters", "dbMark", "sa", "abcde", "PC-XEONE23", "SQLEXPRESS" }, //SQL Server
                {"public", "Sprinters", "dbMark", "Mark", "abcde", "", "" } //Postgres Server
            };

            int n = 1;
            if (cTServer == TypeServer.ServerSQL)
                n = 0;

            txtSchema.Text = stbLog[n, 0]; //"dbo"
            txtTableName.Text = stbLog[n, 1];//"Sprinters";

            txtDataBase.Text = stbLog[n, 2];//"dbMark";
            txtUser.Text = stbLog[n, 3];//"sa";
            txtPass.Password = stbLog[n, 4];//"fdfff";

            txtComp.Text = stbLog[n, 5]; //PC
            txtServer.Text = stbLog[n, 6]; //SQLEXPRESS
        }

      #endregion ----------------

      #region - Technical ------------------------
        private string CheckComma(string xstxt)
        {   //Have to be numeric comma: ','(comma), not '.'(dot)
            //because Convert.ToSingle(string) waiting for ','(comma) 
            return (xstxt.Replace('.', ','));
        }

      #endregion ----------

      #region - VIEW Operations ------------------

        ///<summary> Reads table from DataBase and shows Message </summary>
        private void jReadTableFromDataBaseWithMessage(bool xbAllMessage = true)
        {
            var (bEndOk, sinfo) = jReadTableFromDataBase();
            if (xbAllMessage || !bEndOk)
                ShowMessage(bEndOk, sinfo);
        }

        ///<summary> Shows universal Message</summary>
        private void ShowMessage(bool xbEndOk, string xsinfo)
        {
            if (xbEndOk)
                MessageBox.Show(xsinfo, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(xsinfo, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        ///<summary> Shows Message: DeleteRow </summary>
        private int ShowMessageDeleteRow()
        {
            if (!(cDataGrid1 is null))
                if (cDataGrid1.Items.Count > 0)
                {
                    var cellInfo = cDataGrid1.SelectedCells[0];
                    int aId = Convert.ToInt32((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                    cellInfo = cDataGrid1.SelectedCells[2];
                    string asurname = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

                    if (MessageBox.Show($"You want delete a row Id={aId}!" +
                        $" {asurname}\r\nAre you sure?", "Confirm delete row",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        return (aId);
                }

            MessageBox.Show("Delete canceled", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            return (-1);
        }

        ///<summary> Shows Message: AddExamples </summary>
        private bool ShowMessageAddExamples()
        {
            if (MessageBox.Show($"You want add example rows" +
                $"\r\nAre you sure?", "Confirm add rows",
                MessageBoxButton.YesNo, MessageBoxImage.Question ) == MessageBoxResult.Yes)
                return (true);
            else
            {
                MessageBox.Show("Add rows canceled", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return (false);
            }
        }

      #endregion ------


      #region - Buttons --------------------------

        ///<summary> Checks the connection to the DataBase </summary>
        private void buttCheckConn_Click(object sender, RoutedEventArgs e)
        {
            ccontext = DBContextCreate();
            string asConnString = ccontext.GetConnString();
            bool bExist = ccontext.cbConnectionExist;
            if (bExist)
            {
                labConnectionInfo.Content = "Connection Good";
                labConnectionInfo.Foreground = new SolidColorBrush(Colors.Green); // Color.Green;
            }
            else
            {
                labConnectionInfo.Content = "Connection Error";
                labConnectionInfo.Foreground = new SolidColorBrush(Colors.Red);// Color.Red;
            }

        }

        ///<summary> Reads table from DataBase </summary>
        private void buttReadTb_Click(object sender, RoutedEventArgs e)
        {
            jReadTableFromDataBaseWithMessage();
            //jReadTableFromDataBase();
        }

        ///<summary> Adds one row </summary>
        private void butAddPerson_Click(object sender, RoutedEventArgs e)
        {
            var (bEndOk, sinfo) = jAddRowToDataBase();
            if (bEndOk)
                jReadTableFromDataBaseWithMessage(false);
            ShowMessage(bEndOk, sinfo);
        }

        ///<summary> DELETE BUTTON </summary>
        private void buttDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int iRow = ShowMessageDeleteRow();
            if (iRow > -1)
            {
                var (bEndOk, sinfo) = jDeleteRowFromDataBase(iRow);
                if (bEndOk)
                    jReadTableFromDataBaseWithMessage(false);
                ShowMessage(bEndOk, sinfo);
            }
        }

        ///<summary> ADDS EXAMPLE ROWS </summary>
        private void buttAddExamples_Click(object sender, RoutedEventArgs e)
        {
            bool bOk = ShowMessageAddExamples();
            if (!bOk) return;

            var (bEndOk, sinfo) = jAddExampleRowsToDataBase();
            if (bEndOk)
                jReadTableFromDataBaseWithMessage(false);
            ShowMessage(bEndOk, sinfo);
        }

      #endregion ---------------------------

    }
}
