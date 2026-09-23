using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prefix to prepend to event formulas
        const string prefix = "Prefix_";

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Event section exists and the target event cell is available
                    // (using EventDblClick as a representative event cell, since EventData1 is not exposed)
                    if (shape.Event != null && shape.Event.EventDblClick != null)
                    {
                        // Retrieve the current formula; handle possible null
                        string currentFormula = shape.Event.EventDblClick.Ufe.F ?? string.Empty;

                        // Prepend the defined prefix to the formula
                        shape.Event.EventDblClick.Ufe.F = prefix + currentFormula;
                    }
                }
            }

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}