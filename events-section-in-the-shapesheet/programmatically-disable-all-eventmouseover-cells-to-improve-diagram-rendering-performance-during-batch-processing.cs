using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // The EventMouseOver cell is not exposed in Aspose.Diagram.
                    // As a fallback, clear any supported event cells to improve performance.
                    if (shape.Event != null)
                    {
                        // Disable double-click event
                        shape.Event.EventDblClick.Ufe.F = "";
                        // Disable drop event
                        shape.Event.EventDrop.Ufe.F = "";
                        // Disable multi-drop event
                        shape.Event.EventMultiDrop.Ufe.F = "";
                        // Disable XFMod event
                        shape.Event.EventXFMod.Ufe.F = "";
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}