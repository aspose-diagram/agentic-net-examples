using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify; using shape ID 1 as an example
                Shape shape = page.Shapes.GetShape(1);

                // Configure the EventXFMod cell (triggered on shape resize) to maintain aspect ratio.
                // The formula sets Width = Height * 1.5, preserving a 1.5 aspect ratio.
                shape.Event.EventXFMod.Ufe.F = "SETF(Width, Height * 1.5)";

                // Save the modified diagram with a valid SaveFileFormat enum value
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}