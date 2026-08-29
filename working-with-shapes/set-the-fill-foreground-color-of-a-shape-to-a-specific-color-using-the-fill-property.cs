using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page
                Page page = diagram.Pages[0];

                // Example: modify shape with ID 1 (adjust as needed)
                int shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Set the fill foreground color to a solid red using a hex string
                shape.Fill.FillForegnd.Value = "#FF0000";

                // Optionally set a solid fill pattern (1 = solid)
                shape.Fill.FillPattern.Value = 1;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape ID {shapeId} fill color set to red and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }