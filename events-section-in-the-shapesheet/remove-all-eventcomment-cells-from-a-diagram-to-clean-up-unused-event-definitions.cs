using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_cleaned.vsdx";

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Clear each supported event cell if it exists.
                    // Setting the formula to an empty string removes the event definition.
                    try { shape.Event.EventXFMod.Ufe.F = ""; } catch { }
                    try { shape.Event.EventDblClick.Ufe.F = ""; } catch { }
                    try { shape.Event.EventDrop.Ufe.F = ""; } catch { }
                    try { shape.Event.EventMultiDrop.Ufe.F = ""; } catch { }
                    try { shape.Event.TheText.Ufe.F = ""; } catch { }
                    try { shape.Event.TheData.Ufe.F = ""; } catch { }
                }
            }

            // Save the cleaned diagram to the output file using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}