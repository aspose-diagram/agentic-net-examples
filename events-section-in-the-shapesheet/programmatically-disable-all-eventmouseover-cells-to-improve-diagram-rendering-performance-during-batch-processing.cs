using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (replace with actual paths or pass via command‑line arguments)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to clear supported event cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Event section exists before accessing its cells
                    if (shape.Event != null)
                    {
                        // Clear the formula of each known event cell to disable the event
                        shape.Event.EventDblClick.Ufe.F = "";
                        shape.Event.EventDrop.Ufe.F = "";
                        shape.Event.EventMultiDrop.Ufe.F = "";
                        shape.Event.EventXFMod.Ufe.F = "";
                        // TheText and TheData are also part of the Event section; clear them as well
                        shape.Event.TheText.Ufe.F = "";
                        shape.Event.TheData.Ufe.F = "";
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}