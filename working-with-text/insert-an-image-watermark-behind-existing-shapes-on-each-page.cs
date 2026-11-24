using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <watermarkImagePath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string watermarkImagePath = args[1];
        string outputVisioPath = args[2];

        // Guard: check input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: check watermark image file exists
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image file not found: {watermarkImagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the watermark shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the watermark image as a shape covering the full page
                using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape returns the shape ID (long)
                    long watermarkShapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                    // Retrieve the shape object using the ID
                    Shape watermarkShape = page.Shapes.GetShape(watermarkShapeId);

                    // Send the watermark shape to the back so it appears behind existing content
                    watermarkShape.SendToBack();
                }
            }

            // Save the modified diagram to the specified output path (VSDX format)
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark added and diagram saved to: {outputVisioPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}