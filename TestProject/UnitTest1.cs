using NUnit.Framework;
using WpfApp.ViewModel;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Connect_RunConnect_ConnectedToDataBase()
        {   //[MethodUnderTest]_[Scenario]_[ExpectedResult]

            // Arrange
            CPeopleDBContext ccontext = InitPDBContext();

            // Act
            bool bConnected = ccontext.cbConnectionExist;

            // Assert
            Assert.IsTrue(bConnected);
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
}