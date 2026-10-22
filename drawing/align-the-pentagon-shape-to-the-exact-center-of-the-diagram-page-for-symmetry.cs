using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_centered.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if multiple pages are needed)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (width and height in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Iterate through all shapes on the page to find the pentagon
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape uses a master and that the master name is "Pentagon"
                if (shape.Master != null && shape.Master.Name == "Pentagon")
                {
                    // Align the shape's center (PinX, PinY) to the page center
                    shape.XForm.PinX.Value = pageWidth / 2.0;
                    shape.XForm.PinY.Value = pageHeight / 2.0;
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}