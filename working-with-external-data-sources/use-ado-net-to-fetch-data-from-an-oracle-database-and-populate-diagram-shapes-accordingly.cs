using System;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // ------------------------------------------------------------
                // Simulated data retrieval.
                // In a real scenario you would use ADO.NET with an Oracle provider, e.g.:
                // ------------------------------------------------------------
                // string connString = "User Id=myUser;Password=myPwd;Data Source=MyOracleDB";
                // using (var conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                // {
                //     conn.Open();
                //     using (var cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("SELECT Id, Name, Value FROM MyTable", conn))
                //     using (var adapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(cmd))
                //     {
                //         DataTable dt = new DataTable();
                //         adapter.Fill(dt);
                //         // Process dt...
                //     }
                // }
                // ------------------------------------------------------------
                // Since external Oracle drivers are not available, we create an in‑memory DataTable.

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Value", typeof(double));

                // Sample rows
                dataTable.Rows.Add(1, "Alpha", 12.5);
                dataTable.Rows.Add(2, "Beta", 7.3);
                dataTable.Rows.Add(3, "Gamma", 15.0);
                dataTable.Rows.Add(4, "Delta", 3.8);

                // ------------------------------------------------------------
                // Create a new empty Visio diagram.
                // ------------------------------------------------------------
                Diagram diagram = new Diagram();

                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first page.
                Page page = diagram.Pages[0];

                // Starting position for shapes.
                double startX = 2.0; // inches
                double startY = 2.0; // inches
                double verticalSpacing = 1.5; // inches between shapes
                double shapeWidth = 1.5; // inches
                double shapeHeight = 0.8; // inches

                // Iterate over the data rows and create a rectangle shape for each.
                foreach (DataRow row in dataTable.Rows)
                {
                    string shapeName = row["Name"].ToString();
                    double numericValue = Convert.ToDouble(row["Value"]);

                    // Add a rectangle shape. The fourth parameter isCalculate must be false.
                    long shapeId = page.AddShape(startX, startY, shapeWidth, shapeHeight, "Rectangle", false);

                    // Retrieve the shape object.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Set the shape's text to the Name field.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(shapeName));

                    // Position the shape (PinX/Y represent the center of the shape).
                    shape.XForm.PinX.Value = startX + shapeWidth / 2.0;
                    shape.XForm.PinY.Value = startY + shapeHeight / 2.0;

                    // Apply a fill color based on the numeric value.
                    // Example rule: value >= 10 => red, otherwise green.
                    if (numericValue >= 10.0)
                    {
                        shape.Fill.FillForegnd.Value = "#FF0000"; // Red
                    }
                    else
                    {
                        shape.Fill.FillForegnd.Value = "#00FF00"; // Green
                    }

                    // Optionally store the Id in the shape's custom property for later reference.
                    // Ensure the Props collection exists.
                    if (shape.Props != null)
                    {
                        // Create a new custom property.
                        Prop prop = new Prop();
                        prop.Name = "RecordId";
                        prop.Label.Value = "Record Id";
                        prop.Value.Val = row["Id"].ToString();
                        shape.Props.Add(prop);
                    }

                    // Move to the next vertical position.
                    startY += shapeHeight + verticalSpacing;
                }

                // ------------------------------------------------------------
                // Save the diagram to a VSDX file.
                // ------------------------------------------------------------
                string outputPath = "DataDrivenDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }