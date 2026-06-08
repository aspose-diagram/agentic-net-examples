using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the target page index and shape ID (adjust as needed)
                int pageIndex = 0;               // first page
                long shapeId = 1;                // ID of the shape to modify

                // Retrieve the page and shape
                Page page = diagram.Pages[pageIndex];
                Shape shape = page.Shapes.GetShape(shapeId);

                // Name of the custom property (Prop) to remove
                string targetPropName = "MyCustomProperty";

                // Find and remove the property with the specified name
                Prop propToRemove = null;
                foreach (Prop prop in shape.Props)
                {
                    if (prop.Name == targetPropName)
                    {
                        propToRemove = prop;
                        break;
                    }
                }

                if (propToRemove != null)
                {
                    shape.Props.Remove(propToRemove);
                    Console.WriteLine($"Property '{targetPropName}' removed from shape ID {shapeId}.");
                }
                else
                {
                    Console.WriteLine($"Property '{targetPropName}' not found on shape ID {shapeId}.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }