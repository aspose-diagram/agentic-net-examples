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
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page in the document
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (if any)
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                targetShape = s;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Assign a custom script to the EventXFMod cell (shape data changed event).
            // The formula uses CALLTHIS to invoke a macro or script named "RecalcDependentValues".
            targetShape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"RecalcDependentValues\")";

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Event cell updated and diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}