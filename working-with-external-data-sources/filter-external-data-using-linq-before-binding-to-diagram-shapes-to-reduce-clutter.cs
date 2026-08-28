using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve external data (could be from a database, file, etc.)
            List<DataItem> externalData = GetExternalData();

            // Filter data using LINQ to keep only the items we need
            IEnumerable<DataItem> filteredData = externalData
                .Where(item => item.Category == "Important")
                .OrderBy(item => item.Id);

            // Bind the filtered data to diagram shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Assume the shape's universal name (NameU) matches the data item's Id
                    DataItem match = filteredData
                        .FirstOrDefault(d => d.Id.ToString() == shape.NameU);

                    if (match != null)
                    {
                        // Replace the shape's text with the filtered value
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(match.Value));
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

    // Mock method to simulate retrieving external data
    private static List<DataItem> GetExternalData()
    {
        return new List<DataItem>
        {
            new DataItem { Id = 1, Value = "Alpha", Category = "Important" },
            new DataItem { Id = 2, Value = "Beta", Category = "Ignore" },
            new DataItem { Id = 3, Value = "Gamma", Category = "Important" },
            new DataItem { Id = 4, Value = "Delta", Category = "Other" }
        };
    }

    // Simple DTO representing external data
    private class DataItem
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}