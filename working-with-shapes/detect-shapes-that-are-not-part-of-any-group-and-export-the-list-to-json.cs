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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // List to hold information about shapes that are not in any group
            var ungroupedShapes = new List<object>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shape.IsInGroup() returns true if the shape belongs to a group
                    if (!shape.IsInGroup())
                    {
                        ungroupedShapes.Add(new
                        {
                            PageId = page.ID,
                            PageName = page.Name,
                            ShapeId = shape.ID,
                            ShapeName = shape.Name,
                            ShapeType = shape.Type
                        });
                    }
                }
            }

            // Convert the list to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ungroupedShapes, jsonOptions);

            // Write JSON to a file
            File.WriteAllText("ungrouped_shapes.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
