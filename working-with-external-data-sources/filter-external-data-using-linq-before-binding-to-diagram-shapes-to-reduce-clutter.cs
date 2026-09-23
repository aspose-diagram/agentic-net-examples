using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (template.vsdx should exist in the executable directory)
                string diagramPath = "template.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Simulate external data source using an in‑memory DataTable
                DataTable externalData = new DataTable();
                externalData.Columns.Add("ID", typeof(int));
                externalData.Columns.Add("Name", typeof(string));
                externalData.Columns.Add("Value", typeof(int));

                // Populate sample rows
                externalData.Rows.Add(1, "Alpha", 30);
                externalData.Rows.Add(2, "Beta", 75);
                externalData.Rows.Add(3, "Gamma", 55);
                externalData.Rows.Add(4, "Delta", 20);
                externalData.Rows.Add(5, "Epsilon", 90);

                // Filter rows where Value > 50 using LINQ
                IEnumerable<DataRow> filteredRows = externalData.Rows
                    .Cast<DataRow>()
                    .Where(row => Convert.ToInt32(row["Value"]) > 50);

                // Positioning variables for newly added shapes
                double startX = 1.0;   // PinX
                double startY = 1.0;   // PinY
                double shapeWidth = 2.0;
                double shapeHeight = 1.0;
                double verticalSpacing = 1.5;

                // Add a rectangle shape for each filtered data row and bind the Name to the shape's text
                foreach (DataRow row in filteredRows)
                {
                    // Add a rectangle master shape to the page
                    long shapeId = page.AddShape(startX, startY, shapeWidth, shapeHeight, "Rectangle", false);

                    // Retrieve the created shape instance
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and set the shape's text to the Name field
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(row["Name"].ToString()));

                    // Move to the next vertical position for the following shape
                    startY += verticalSpacing;
                }

                // Save the modified diagram to a new file
                diagram.Save("filtered_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }