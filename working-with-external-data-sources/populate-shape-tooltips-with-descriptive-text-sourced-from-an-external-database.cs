using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Database connection string – adjust to your environment
                string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve tooltip data from the database
                Dictionary<long, string> tooltipMap = LoadTooltipsFromDatabase(connectionString);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if we have a tooltip for this shape (using its ID)
                        if (tooltipMap.TryGetValue(shape.ID, out string tooltip))
                        {
                            // Ensure the Hyperlinks collection exists
                            if (shape.Hyperlinks == null)
                                continue; // Should not happen, but safety check

                            // Create a new hyperlink with empty address and set the description as tooltip
                            Hyperlink link = new Hyperlink();
                            link.Address.Value = "";               // No navigation address
                            link.Description.Value = tooltip;      // Tooltip text

                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Loads shape tooltip information from a database table.
        /// Expected table schema: ShapeTooltips(ShapeId BIGINT PRIMARY KEY, Tooltip NVARCHAR(MAX))
        /// </summary>
        static Dictionary<long, string> LoadTooltipsFromDatabase(string connString)
        {
            var map = new Dictionary<long, string>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT ShapeId, Tooltip FROM ShapeTooltips";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long shapeId = reader.GetInt64(0);
                        string tooltip = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        map[shapeId] = tooltip;
                    }
                }
            }

            return map;
        }
    }