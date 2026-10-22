using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (adjust as needed for other pages).
            Page page = diagram.Pages[0];

            // Iterate over all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Set the EventDrop cell (closest substitute for EventMouseEnter) to call a custom macro.
                // The formula uses the CALLTHIS function to invoke a macro named "MyMacro".
                shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"MyMacro\")";
            }

            // Save the modified diagram in a macro‑enabled format.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}