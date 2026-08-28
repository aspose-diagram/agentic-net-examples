using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for save options (if needed)

class Program
{
    static void Main(string[] args)
    {
        // Input and output Visio file paths
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // The EventShapeDeleted cell does not exist in Aspose.Diagram.
                    // Use a supported event cell (e.g., EventDrop) as a placeholder
                    // to demonstrate setting an event formula that calls a macro.
                    shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"LogDeletion\")";
                }
            }

            // Save the modified diagram to the output path using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}