using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // NOTE: The Aspose.Diagram API does not expose an EventComment cell.
            // Therefore, duplicate event comment removal cannot be performed via this property.
            // The code below simply iterates through pages and shapes to illustrate where such logic would be placed
            // if a supported event cell were available.

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an Event section before accessing any event cells
                    if (shape.Event == null)
                        continue;

                    // Placeholder for event comment handling – currently not supported by the API
                    // Example of accessing a supported event cell:
                    // var dblClickCell = shape.Event.EventDblClick;
                    // if (dblClickCell != null) { /* process dbl-click event formula */ }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}