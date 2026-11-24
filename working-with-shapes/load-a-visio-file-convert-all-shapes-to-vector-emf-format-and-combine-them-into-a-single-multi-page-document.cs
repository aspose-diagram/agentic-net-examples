using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output folder for EMF files, output PDF file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputEmfFolder> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string emfFolder = args[1];
        string outputPdfPath = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the EMF output folder exists.
        if (!Directory.Exists(emfFolder))
        {
            try
            {
                Directory.CreateDirectory(emfFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create EMF output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the source Visio diagram inside a using block.
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Collect all shapes from all pages.
                List<Shape> allShapes = new List<Shape>();
                foreach (Page page in sourceDiagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        allShapes.Add(shape);
                    }
                }

                // Export each shape to an individual EMF file.
                int shapeIndex = 1;
                foreach (Shape shape in allShapes)
                {
                    string emfPath = Path.Combine(emfFolder, $"Shape_{shapeIndex}.emf");
                    // Use ImageSaveOptions with EMF format.
                    ImageSaveOptions emfOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                    // Export the shape as an EMF image.
                    shape.ToImage(emfPath, emfOptions);
                    shapeIndex++;
                }
            }

            // After exporting all shapes, combine the EMF files into a multi‑page PDF.
            // Use fully qualified Aspose.Pdf types to avoid namespace conflicts.
            var pdfDocument = new Aspose.Pdf.Document();

            // Get all EMF files sorted by name to maintain order.
            string[] emfFiles = Directory.GetFiles(emfFolder, "*.emf");
            Array.Sort(emfFiles);

            foreach (string emfFile in emfFiles)
            {
                // Add a new page for each EMF image.
                Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                // Create an image object and load the EMF stream.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                using (FileStream emfStream = new FileStream(emfFile, FileMode.Open, FileAccess.Read))
                {
                    pdfImage.ImageStream = emfStream;
                }

                // Fit the image to the page dimensions.
                pdfImage.FixWidth = pdfPage.PageInfo.Width;
                pdfImage.FixHeight = pdfPage.PageInfo.Height;

                // Add the image to the page.
                pdfPage.Paragraphs.Add(pdfImage);
            }

            // Save the combined PDF document.
            pdfDocument.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}