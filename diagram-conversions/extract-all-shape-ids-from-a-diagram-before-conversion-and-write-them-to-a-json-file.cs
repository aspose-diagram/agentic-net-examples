using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Collect all shape IDs from every page
            List<long> shapeIds = new List<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }
            }

            // Serialize the IDs to a formatted JSON string
            string json = JsonSerializer.Serialize(shapeIds, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to a file
            File.WriteAllText("shapeIds.json", json);

            // Example of using the provided Save rule (optional, shows proper save usage)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
