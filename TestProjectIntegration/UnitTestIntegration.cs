using System;
using System.Transactions;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WpfApp.ViewModel;

using System.Linq;
using System.Data;


namespace TestProjectIntegration
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
    }

    public class UserIntegrationTests
    {
        [Test, CTransactionIsolated]

        public void Add_AddValidUser_ShouldAddUserToDatabase()
        {   // [MethodUnderTest]_[Scenario]_[ExpectedResult]

            // Arrange
            string sname = "Pietro";
            string ssurname = "Mennea";
            CPeopleDBContext ccontext = InitPDBContext();

            // Act
            ccontext.AddOnePerson(sname, ssurname, 48, "Rome", 186);

            // Assert
            var usrList = ccontext.dbPersons.ToList()
                .Where(n => (n.surname == ssurname))
                .ToList();
            int n = usrList.Count;

            Assert.That(n, Is.EqualTo(1));
        }


        private CPeopleDBContext InitPDBContext()
        {
            // Adnotation to the test procedure
            CPeopleMain aPeopleMain = new CPeopleMain();
            return (
                aPeopleMain.DBContextCreate(TypeServer.ServerSQL,
                "dbo", "Sprinters", "dbMark", "sa", "abcde", "PC-XEONE23", "SQLEXPRESS")
            );
        }
    }

    public class CTransactionIsolated : Attribute, ITestAction
    {
        private TransactionScope transactionScp;

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        public void AfterTest(ITest test)
        {
            transactionScp.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            transactionScp = new TransactionScope();
        }
    }

}