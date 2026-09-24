using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                targetShape = shp;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Set the text direction to right‑to‑left.
            // In Aspose.Diagram the TextDirection enum provides Horizontal and Vertical.
            // Using Vertical here as the closest representation for right‑to‑left text flow.
            targetShape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Text direction updated and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
