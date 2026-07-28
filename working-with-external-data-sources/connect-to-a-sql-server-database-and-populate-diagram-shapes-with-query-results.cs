using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Create a DataConnection for SQL Server
            // -------------------------------------------------
            DataConnection sqlConnection = new DataConnection();
            sqlConnection.ID = 1; // unique within the document
            sqlConnection.ConnectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;";
            // The Command property of DataConnection is optional when using DataRecordSet
            diagram.DataConnections.Add(sqlConnection);

            // -------------------------------------------------
            // Create a DataRecordSet that uses the above connection
            // -------------------------------------------------
            DataRecordSet recordSet = new DataRecordSet();
            recordSet.ID = 1; // unique within the document
            recordSet.Name = "EmployeeData";
            recordSet.ConnectionID = sqlConnection.ID;
            recordSet.Command = "SELECT EmployeeID, Name, Title FROM Employees";
            diagram.DataRecordSets.Add(recordSet);

            // -------------------------------------------------
            // Refresh the DataRecordSet to pull data from SQL Server
            // -------------------------------------------------
            // This executes the query and updates any linked shapes automatically
            recordSet.Refresh(DataConnectionType.SQL);

            // -------------------------------------------------
            // Save the updated diagram (replace with your desired output path)
            // -------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
