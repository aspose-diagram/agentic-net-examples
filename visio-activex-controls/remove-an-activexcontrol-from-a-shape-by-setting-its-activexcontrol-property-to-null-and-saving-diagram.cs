using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

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

        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // Locate the first shape that contains an ActiveX control
            Shape shapeWithControl = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.ActiveXControl != null)
                {
                    shapeWithControl = shape;
                    break;
                }
            }

            // If no such shape exists, report and exit
            if (shapeWithControl == null)
            {
                Console.Error.WriteLine("No shape with an ActiveX control was found in the diagram.");
                return;
            }

            // Remove the shape containing the ActiveX control from the page.
            // Direct assignment to ActiveXControl is not allowed (read‑only), so the shape itself is removed.
            page.Shapes.Remove(shapeWithControl);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}