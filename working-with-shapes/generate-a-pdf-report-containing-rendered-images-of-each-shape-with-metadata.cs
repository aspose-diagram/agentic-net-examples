using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF report path
                string outputPdfPath = "ShapeReport.pdf";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(inputPath);

                // Create a new diagram that will hold the report
                Diagram reportDiagram = new Diagram();

                // Use the first (default) page of the report diagram
                Page reportPage = reportDiagram.Pages[0];

                // Layout parameters
                double startX = 1.0;               // left margin
                double currentY = 1.0;             // start from top
                double imageWidth = 2.0;           // width of each shape image
                double imageHeight = 2.0;          // height of each shape image
                double textWidth = 4.0;            // width of the metadata text box
                double textHeight = 0.5;           // height of the metadata text box
                double verticalSpacing = 0.3;      // space between entries

                // Temporary folder for shape images
                string tempFolder = Path.Combine(Path.GetTempPath(), "ShapeImages");
                Directory.CreateDirectory(tempFolder);

                // Iterate through all pages and shapes in the source diagram
                foreach (Page srcPage in sourceDiagram.Pages)
                {
                    foreach (Shape srcShape in srcPage.Shapes)
                    {
                        // Skip deleted shapes
                        if (srcShape.Del == BOOL.True)
                            continue;

                        // Generate a temporary PNG file for the shape
                        string imagePath = Path.Combine(tempFolder, Guid.NewGuid().ToString() + ".png");
                        var imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        srcShape.ToImage(imagePath, imgOptions);

                        // Insert the image into the report page
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            // AddShape returns the shape ID (long)
                            long imageShapeId = reportPage.AddShape(startX, currentY, imageWidth, imageHeight, imgStream);
                            // Optionally, you could retrieve the shape object if further adjustments are needed:
                            // Shape imageShape = reportPage.Shapes.GetShape(imageShapeId);
                        }

                        // Prepare metadata text
                        string metadata = $"ID: {srcShape.ID}, Name: {srcShape.Name}, Type: {srcShape.Type}";

                        // Add a text box next to the image
                        double textPosX = startX + imageWidth + 0.2;
                        reportPage.AddText(textPosX, currentY, textWidth, textHeight, metadata);

                        // Move to the next vertical position
                        currentY += Math.Max(imageHeight, textHeight) + verticalSpacing;
                    }
                }

                // Clean up temporary images
                try
                {
                    Directory.Delete(tempFolder, true);
                }
                catch
                {
                    // If deletion fails, ignore – the OS will clean up temp files later.
                }

                // Configure PDF save options
                var pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Save the report diagram as a PDF
                reportDiagram.Save(outputPdfPath, pdfOptions);

                Console.WriteLine($"PDF report generated at: {Path.GetFullPath(outputPdfPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }