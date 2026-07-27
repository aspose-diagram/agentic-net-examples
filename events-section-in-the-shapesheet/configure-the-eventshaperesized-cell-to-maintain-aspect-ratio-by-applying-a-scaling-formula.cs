using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example ID = 1)
                // Adjust the ID as needed for your specific diagram
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a scaling formula to maintain aspect ratio when the shape is resized.
                // The EventXFMod cell is used here as a placeholder for a resize-related event.
                // The formula sets the Height to be 75% of the Width (adjust the factor as required).
                shape.Event.EventXFMod.Ufe.F = "GUARD(Width * 0.75)";

                // Save the modified diagram to the output path
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Event cell configured successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}