using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine the next available page ID (maximum existing ID + 1).
            int maxPageId = 0; // Changed to int to match Page.ID type.
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new blank page and assign a unique ID and name.
            Page newPage = new Page();
            newPage.ID = maxPageId + 1; // Explicitly using int.
            newPage.Name = "NewPage";

            // Add the new page to the diagram.
            diagram.Pages.Add(newPage);

            // Define oval (ellipse) position and size.
            // PinX and PinY specify the center of the shape.
            // Width and Height specify the size of the ellipse.
            double pinX = 2.0;   // X coordinate of the center (in inches)
            double pinY = 2.0;   // Y coordinate of the center (in inches)
            double width = 3.0;  // Horizontal diameter (in inches)
            double height = 1.5; // Vertical diameter (in inches)

            // Draw the oval on the newly added page using the non‑obsolete API.
            // DrawOwal expects the bounding rectangle corners (x1,y1) top‑left and (x2,y2) bottom‑right.
            double x1 = pinX - width / 2.0;
            double y1 = pinY - height / 2.0;
            double x2 = pinX + width / 2.0;
            double y2 = pinY + height / 2.0;
            newPage.DrawOwal(x1, y1, x2, y2);

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
