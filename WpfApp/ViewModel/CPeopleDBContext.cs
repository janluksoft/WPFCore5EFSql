using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WpfApp.Model;

namespace WpfApp.ViewModel
{
    public enum TypeServer :byte
    {
        ServerPostgres,
        ServerSQL
    }

    /// <summary> 
    /// The class creates a context (dbPersons) that represents a table (Sprinters) in the form of an object.<br/>
    /// The class also includes methods to handle the object
    /// </summary>
    public class CPeopleDBContext : DbContext
    {
      #region - Variables ------------------------

        //(Database context)
        public DbSet<CPerson> dbPersons { get; set; } //Represents the table in the DataBase

        private string sSchema;
        private string sTableName;

        public string csConnString { get; set; } //Internal string
        public bool cbConnectionExist { get; set; }

      #endregion -----------

      #region - Constructor operations -----------

        public CPeopleDBContext(string xConnString, string xsSchema, string xsTableName)
            : base(nameOrConnectionString: xConnString )
        {
            sSchema = xsSchema;
            sTableName = xsTableName;
            csConnString = xConnString;
            cbConnectionExist = Database.Exists();
        }

        ///<summary> Set TableName and schema properties </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(sSchema);
            modelBuilder.Entity<CPerson>().ToTable(sTableName, sSchema);
            base.OnModelCreating(modelBuilder);
        }

      #endregion -----------

      #region - Methods-Operations ---------------

        ///<summary> Adds a new row to the DataBase table </summary>
        public void AddOnePerson(string xsName, string xsSurname, 
                                 float xfAge, string xsCity, float xfHeight)
        {
            //temporary POCO class
            CPerson aLud = new CPerson 
            {
                //Id = 1, //adds automatic
                name = xsName,
                surname = xsSurname,
                age = xfAge,
                city = xsCity,
                height = xfHeight
            };

            AddAlbum(aLud); //Adds aLud to dbPersons
            SaveChanges();  //Save changes
        }

        ///<summary> Adds a new row to the DataBase table from argument xLud </summary>
        public void AddAlbum(CPerson xLud)
        {
            dbPersons.Add(xLud);
        }

        ///<summary> Removes a row, specified in the argument, from the DataBase table </summary>
        public void RemoveAlbum(int xId)
        {
            var person1 = new CPerson() { Id = xId };
            dbPersons.Remove(dbPersons.Find(xId));
            SaveChanges();  //Save changes
        }

        ///<summary> Adds a new rows to the DataBase table from examples table </summary>
        /// <returns>(int) Count of added rows or error -1</returns>
        public int jAddExampleRowsToDataBase()
        {
            int iCount = -1;

            try
            {
                CPersonExample aPerEx = new CPersonExample();
                List<CPerson> lPersons = aPerEx.ListExampleRows();

                foreach (var cperson in lPersons)
                {
                    this.AddAlbum(cperson);
                    this.SaveChanges();
                }
                iCount = lPersons.Count;
            }
            catch (Exception ex)
            {
            }

            return(iCount);
        }

      #endregion -----------

      #region - Connect ------------------

        ///<summary> Returns ConnectionString </summary>
        public string GetConnString()
        {
            return (csConnString);
        }

        ///<summary> Returns if there is a connection to the database </summary>
        public bool GetConnectionExist()
        {
            return (cbConnectionExist);
        }

      #endregion -----------

    }
}
