using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Set the EventXFMod cell (Visio's After Update event) to the current timestamp
                    // Using the NOW() formula ensures the timestamp updates when the shape is modified
                    shape.Event.EventXFMod.Ufe.F = "NOW()";
                }
            }

            // Save the modified diagram to the specified output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}