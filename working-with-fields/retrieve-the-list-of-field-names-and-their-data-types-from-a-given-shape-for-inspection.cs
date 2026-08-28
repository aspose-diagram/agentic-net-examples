using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Determine which shape to inspect
                Shape shape = null;

                if (args.Length > 0 && int.TryParse(args[0], out int shapeId))
                {
                    // Try to retrieve shape by the provided ID
                    shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                        return;
                    }
                }
                else
                {
                    // Fallback: use the first shape on the page
                    foreach (Shape s in page.Shapes)
                    {
                        shape = s;
                        break;
                    }

                    if (shape == null)
                    {
                        Console.WriteLine($"No shapes found on page '{page.Name}'.");
                        return;
                    }
                }

                Console.WriteLine($"Inspecting shape ID: {shape.ID}, Name: {shape.Name}");

                // Iterate custom properties (Props) – these act as fields with names and data types
                if (shape.Props != null && shape.Props.Count > 0)
                {
                    Console.WriteLine("Custom Properties (Name : Data Type):");
                    foreach (Prop prop in shape.Props)
                    {
                        // prop.Type.Value is an enum indicating the data type
                        string typeName = prop.Type.Value.ToString();
                        Console.WriteLine($"{prop.Name} : {typeName}");
                    }
                }
                else
                {
                    Console.WriteLine("The shape does not contain any custom properties.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }