using System;
using System.Data;
using System.Data.Common;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Oracle connection details – replace with actual credentials and data source
        string connectionString = "User Id=myUser;Password=myPassword;Data Source=MyOracleDB";

        // Provider name for Oracle Managed Data Access
        string providerName = "Oracle.ManagedDataAccess.Client";

        // SQL query to retrieve shape data (ID, Name, X, Y coordinates)
        string query = "SELECT ID, NAME, POSX, POSY FROM SHAPE_DATA";

        // Attempt to create a diagram and populate it with data from the database
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to work with
            Page page = diagram.Pages[0];

            // Obtain a provider‑agnostic factory for the Oracle provider
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            // Open Oracle connection and read data
            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Retrieve values from the data reader
                            double posX = Convert.ToDouble(reader["POSX"]);
                            double posY = Convert.ToDouble(reader["POSY"]);
                            string shapeName = reader["NAME"].ToString();

                            // Add a rectangle shape at the specified position (isCalculate = false)
                            long shapeId = page.AddShape(posX, posY, "Rectangle", false);

                            // Retrieve the shape object to modify its properties
                            Shape shape = page.Shapes.GetShape(shapeId);

                            // Clear any existing text and set the shape's text to the name from the database
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(shapeName));

                            // Optional: set a fill color for visual distinction
                            shape.Fill.FillForegnd.Value = "#FFCC00"; // Light orange
                        }
                    }
                }
            }

            // Save the diagram to a VSDX file
            diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}