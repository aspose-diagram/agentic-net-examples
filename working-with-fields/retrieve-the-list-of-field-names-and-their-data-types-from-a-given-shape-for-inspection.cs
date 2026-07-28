using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Specify the page index and shape ID you want to inspect
                int pageIndex = 0;      // first page
                int shapeId = 1;        // example shape ID

                // Retrieve the page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found on page {pageIndex}.");
                    return;
                }

                // Ensure the shape has custom properties (Props) collection
                if (shape.Props == null || shape.Props.Count == 0)
                {
                    Console.WriteLine("The shape does not contain any custom properties.");
                    return;
                }

                // Iterate through each custom property and output its name and data type
                Console.WriteLine($"Custom properties for shape ID {shapeId}:");
                foreach (Prop prop in shape.Props)
                {
                    // Property name
                    string name = prop.Name;

                    // Property data type (enum value)
                    string typeName = prop.Type.Value.ToString();

                    Console.WriteLine($"- Name: {name}, Type: {typeName}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }