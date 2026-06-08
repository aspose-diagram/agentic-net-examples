using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve external data (could be from a database, API, etc.)
            List<Record> records = GetExternalData();

            // Filter the data using LINQ to keep only active records
            var filteredRecords = records.Where(r => r.IsActive).ToList();

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Iterate through shapes and bind filtered data
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                // Assume each shape's Data1 holds an identifier matching Record.Id
                string shapeKey = shape.Data1;

                // Find a matching record from the filtered list
                Record match = filteredRecords.FirstOrDefault(r => r.Id.ToString() == shapeKey);
                if (match != null)
                {
                    // Update the shape's text with information from the matched record
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Name: {match.Name}, Value: {match.Value}"));
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

    // Mock method to simulate retrieval of external data
    static List<Record> GetExternalData()
    {
        return new List<Record>
        {
            new Record { Id = 1, Name = "Alpha", Value = 100, IsActive = true },
            new Record { Id = 2, Name = "Beta", Value = 200, IsActive = false },
            new Record { Id = 3, Name = "Gamma", Value = 300, IsActive = true }
        };
    }
}

// Simple DTO representing external data items
class Record
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public bool IsActive { get; set; }
}