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

            // Collect all shape IDs from all pages
            List<long> shapeIds = new List<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }
            }

            // Serialize the list of IDs to JSON
            string json = JsonSerializer.Serialize(shapeIds, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to a file
            File.WriteAllText("shapeIds.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
