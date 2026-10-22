using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <watermarkImagePath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string watermarkImagePath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image file not found: {watermarkImagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the center position for the watermark shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the image as a shape covering the whole page.
                    long shapeId;
                    using (FileStream imageStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imageStream);
                    }

                    // Retrieve the newly added shape and send it to the back so it appears behind existing content.
                    Shape watermarkShape = page.Shapes.GetShape(shapeId);
                    watermarkShape.SendToBack();
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark added and diagram saved to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}