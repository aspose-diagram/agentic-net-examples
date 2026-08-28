using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class DiagramDataPopulation
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("TemplateDiagram.vsdx");

            // -------------------------------------------------
            // 1. Create and configure a DataConnection for SQL Server
            // -------------------------------------------------
            DataConnection sqlConnection = new DataConnection();
            // Unique ID for the connection (Visio expects a positive integer)
            sqlConnection.ID = 1;
            // Connection string to the SQL Server database
            sqlConnection.ConnectionString = "Data Source=SERVER_NAME;Initial Catalog=DatabaseName;Integrated Security=True;";
            // SQL query to retrieve data
            sqlConnection.Command = "SELECT EmployeeID, EmployeeName, Department FROM Employees";
            // Add the connection to the diagram's collection
            diagram.DataConnections.Add(sqlConnection);

            // -------------------------------------------------
            // 2. Create a DataRecordSet that uses the above connection
            // -------------------------------------------------
            DataRecordSet recordSet = new DataRecordSet();
            // Assign a unique ID and a friendly name
            recordSet.ID = 1;
            recordSet.Name = "EmployeeData";
            // Link the record set to the previously created connection
            recordSet.ConnectionID = sqlConnection.ID;
            // Set the same command (optional if already set in the connection)
            recordSet.Command = sqlConnection.Command;
            // Add the record set to the diagram (DataRecordSets collection)
            diagram.DataRecordSets.Add(recordSet);

            // -------------------------------------------------
            // 3. Refresh the DataRecordSet to pull data from SQL Server
            // -------------------------------------------------
            // Use the SQL connection type defined in Aspose.Diagram.Manipulation
            recordSet.Refresh(DataConnectionType.SQL);

            // -------------------------------------------------
            // 4. (Optional) Map rows to shapes if needed
            // -------------------------------------------------
            // Example: map the first row to a shape with ID 5 on page 0
            // recordSet.RowMaps.Add(new RowMap { RowID = 1, ShapeID = 5, PageID = 0 });

            // -------------------------------------------------
            // 5. Save the updated diagram
            // -------------------------------------------------
            diagram.Save("DiagramWithData.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
