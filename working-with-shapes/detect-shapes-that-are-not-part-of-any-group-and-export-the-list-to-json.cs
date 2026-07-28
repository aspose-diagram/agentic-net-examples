using System;
using Aspose.Diagram;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // List to hold information about shapes that are not in any group
            var ungroupedShapes = new List<object>();

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Shape.IsInGroup() returns true if the shape belongs to a group
                    if (!shape.IsInGroup())
                    {
                        // Collect desired properties (you can add more if needed)
                        ungroupedShapes.Add(new
                        {
                            PageId = page.ID,
                            ShapeId = shape.ID,
                            Name = shape.Name,
                            Type = shape.Type
                        });
                    }
                }
            }

            // Serialize the list to formatted JSON
            string json = JsonSerializer.Serialize(ungroupedShapes, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON output to a file
            File.WriteAllText("ungrouped_shapes.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
