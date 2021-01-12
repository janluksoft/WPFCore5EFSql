# WPF Core: Entity Framework with SqlServer and MVVM

The application demonstrates the use of Entity Framework to perform CRUD operations on a Microsoft SQL Server database.  
Application uses the MVVM pattern.
Application uses my "nice" WPF windows (see: https://github.com/janluksoft/WPFCoreNiceWin).

The structure of the table is defined by the POCO (CPerson) class. The class (PeopleDBContext) creates a context (dbPersons) that represents a table (Sprinters) in the form of an object. Operations on this object are automatically transferred to the table (Sprinters) in the database.

![](/Jpg/WPF_Window_nice_EF_MVVM.png)

## Details

- Environment: VS2019
- Target: .NET5 (.NET Core)
- Window: WPF
- Pattern: MVVM
- Tests: unit and integration

## Using the application

- On the (Login) tab, enter the login information from SQL Server
- Check the connection with the button 2 (Check Connection)
- For valid data, a message will be shown: (Connection Good)

![](/Jpg/Entity-Framework_e_SQL-Server_Login-parameters.png)

- In the (Proposal) tab, press the (4) button. The application should automatically create a table (Sprinters) on SQL Server
![](Jpg/Entity-Framework_f_SQL-Server_Use_examples.png)

- In the (DataBase) tab you can: (5) read the table from the SQL server, (6) add rows, (7) delete rows. 
![](/Jpg/Entity-Framework_g_SQL-Server_Created-table.png)

## Tests in application

The application also includes two test projects: 1) unit test (TestProject) 2) integration test (TestProjectIntegration). 
They both perform an action on the database using the Entity Framework. The integration test additionally uses transaction 
classes for the database to reverse test operations.

Below are the test results:

![](/Jpg/Test_unit.png)

![](/Jpg/Test_integration.png)
