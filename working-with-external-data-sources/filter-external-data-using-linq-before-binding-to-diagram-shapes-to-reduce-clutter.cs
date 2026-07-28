using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramDataBindingExample
{
    // Simple data model representing external data to be bound to diagram shapes
    public class DataItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Paths for input Visio file and output Visio file
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Retrieve the first page (avoid using ActivePage)
                    Page page = diagram.Pages[0];

                    // ------------------------------------------------------------
                    // STEP 1: Obtain external data (could be from DB, CSV, etc.)
                    // Here we mock a data source for demonstration purposes.
                    // ------------------------------------------------------------
                    List<DataItem> externalData = GetMockData();

                    // ------------------------------------------------------------
                    // STEP 2: Filter the data using LINQ to keep only relevant items.
                    // This reduces clutter by avoiding creation of shapes for unwanted data.
                    // ------------------------------------------------------------
                    var filteredData = externalData
                        .Where(item => item.Value > 10 && item.Category == "Important")
                        .ToList();

                    // ------------------------------------------------------------
                    // STEP 3: Bind each filtered data item to a new shape on the diagram.
                    // ------------------------------------------------------------
                    double startX = 2.0;   // starting X coordinate (in inches)
                    double startY = 2.0;   // starting Y coordinate (in inches)
                    double offsetX = 2.5;  // horizontal spacing between shapes

                    for (int i = 0; i < filteredData.Count; i++)
                    {
                        DataItem item = filteredData[i];

                        // Calculate position for the new shape
                        double pinX = startX + i * offsetX;
                        double pinY = startY;

                        // Add a rectangle shape using the master name "Rectangle"
                        // AddShape returns the shape ID (long)
                        long shapeId = diagram.AddShape(pinX, pinY, "Rectangle", 0);

                        // Retrieve the concrete Shape object using the returned ID
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Clear any existing text and set new text based on the data item
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt($"{item.Name}: {item.Value}"));

                        // Optional: set a fill color to visually differentiate the shape
                        shape.Fill.FillForegnd.Value = "#FFCC00"; // light orange
                    }

                    // ------------------------------------------------------------
                    // STEP 4: Save the modified diagram to a new file.
                    // ------------------------------------------------------------
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Mock method to simulate retrieval of external data.
        private static List<DataItem> GetMockData()
        {
            return new List<DataItem>
            {
                new DataItem { Id = 1, Name = "Alpha",   Value = 5,  Category = "Minor" },
                new DataItem { Id = 2, Name = "Beta",    Value = 12, Category = "Important" },
                new DataItem { Id = 3, Name = "Gamma",   Value = 8,  Category = "Minor" },
                new DataItem { Id = 4, Name = "Delta",   Value = 20, Category = "Important" },
                new DataItem { Id = 5, Name = "Epsilon", Value = 15, Category = "Important" }
            };
        }
    }
}