using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, background image file, output PDF file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <backgroundImagePath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string imagePath = args[1];
        string outputPdfPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over all pages and add the background image shape.
                foreach (Page page in diagram.Pages)
                {
                    // Add the image shape to the page. Coordinates (0,0) and size (8.5 x 11) are used as an example.
                    // The AddShape method returns the shape ID (long).
                    long shapeId;
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        shapeId = page.AddShape(0.0, 0.0, 8.5, 11.0, imgStream);
                    }

                    // Retrieve the shape object using the returned ID.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Ensure the shape is sent to the back so it appears behind other content.
                    shape.SendToBack();

                    // Set a solid fill pattern for the shape as a safety measure.
                    shape.Fill.FillPattern.Value = 1;
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the save format as required.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export the updated diagram to PDF.
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}