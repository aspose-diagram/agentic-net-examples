using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after removal
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the page index (0‑based) and shape ID to work with
                int pageIndex = 0;      // first page
                long shapeId = 1;       // example shape ID

                // Name of the custom property (field) to remove
                string targetFieldName = "MyCustomProp";

                // Retrieve the target page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the target shape
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found on page {pageIndex}.");
                }

                // Collect matching custom properties (Props) by name
                List<Prop> toRemove = new List<Prop>();
                foreach (Prop prop in shape.Props)
                {
                    if (prop.Name == targetFieldName)
                    {
                        toRemove.Add(prop);
                    }
                }

                // Remove the identified properties from the shape
                foreach (Prop prop in toRemove)
                {
                    shape.Props.Remove(prop);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }