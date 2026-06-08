using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Retrieve tooltip data from an external SQL database
                // Expected table schema: ShapeId (bigint) , Tooltip (nvarchar)
                string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;";
                var tooltipMap = new Dictionary<long, string>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT ShapeId, Tooltip FROM ShapeTooltips", connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long shapeId = reader.GetInt64(0);
                            string tip = reader.GetString(1);
                            tooltipMap[shapeId] = tip;
                        }
                    }
                }

                // Iterate through all pages and shapes, assigning tooltips via hyperlink description
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (tooltipMap.TryGetValue(shape.ID, out string tooltip))
                        {
                            // Ensure there is at least one hyperlink; create if none exist
                            Hyperlink link;
                            if (shape.Hyperlinks.Count > 0)
                            {
                                link = shape.Hyperlinks[0];
                            }
                            else
                            {
                                link = new Hyperlink();
                                shape.Hyperlinks.Add(link);
                            }

                            // Set the tooltip text (description) for the hyperlink
                            link.Description.Value = tooltip;
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }