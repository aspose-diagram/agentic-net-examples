using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape's height is greater than 200 points
                    if (shape.XForm.Height.Value > 200)
                    {
                        // Apply a 5-degree rotation around the X axis
                        shape.ThreeDFormat.RotationXAngle.Value = 5.0;
                    }
                }
            }

            // Configure PDF save options (default options are sufficient for this task)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the modified diagram as a PDF
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}