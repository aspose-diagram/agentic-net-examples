using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page; create one if necessary
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Parameters for the new shape
                double pinX = 2.0;      // X coordinate (in inches)
                double pinY = 2.0;      // Y coordinate (in inches)
                double width = 1.5;     // Width (in inches)
                double height = 1.0;    // Height (in inches)
                string masterName = "Rectangle"; // Built‑in master name

                // Add the shape; AddShape returns the automatically assigned shape ID (long)
                long newShapeId = page.AddShape(pinX, pinY, width, height, masterName, false);

                // Retrieve the newly added shape using its ID
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Output the assigned ID and basic information
                Console.WriteLine($"New shape added with ID: {newShapeId}");
                Console.WriteLine($"Shape NameU: {newShape.NameU}");
                Console.WriteLine($"Master Name: {newShape.Master?.Name}");

                // Example further operation: set the shape's text
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt("Hello Aspose"));

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}