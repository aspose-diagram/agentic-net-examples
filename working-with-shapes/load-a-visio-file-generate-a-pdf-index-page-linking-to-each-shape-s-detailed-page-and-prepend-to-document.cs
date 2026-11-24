using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for shape images
        string tempDir = Path.Combine(Path.GetTempPath(), "VisioShapeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Lists to hold image paths and display names for each shape
                List<string> shapeImagePaths = new List<string>();
                List<string> shapeNames = new List<string>();

                // Iterate through all pages and shapes to generate PNG images
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine a display name for the shape
                        string displayName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : $"Shape_{shape.ID}";

                        // Generate a PNG image for the shape
                        string imgPath = Path.Combine(tempDir, $"shape_{shape.ID}.png");
                        shape.ToImage(imgPath, new ImageSaveOptions(SaveFileFormat.Png));

                        shapeImagePaths.Add(imgPath);
                        shapeNames.Add(displayName);
                    }
                }

                // Create a new PDF document using fully qualified Aspose.Pdf types
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

                // Add an index page at the beginning
                Aspose.Pdf.Page indexPage = pdfDoc.Pages.Add();

                // Add title text to the index page
                Aspose.Pdf.Text.TextFragment title = new Aspose.Pdf.Text.TextFragment("Visio Shapes Index");
                title.TextState.FontSize = 20;
                title.Position = new Aspose.Pdf.Text.Position(0, 800);
                indexPage.Paragraphs.Add(title);

                // Layout parameters for index entries
                float startY = 750;
                float lineHeight = 20;
                float linkX = 50;
                float linkWidth = 200;
                float linkHeight = 15;

                // Add a page for each shape image and a corresponding link on the index page
                for (int i = 0; i < shapeImagePaths.Count; i++)
                {
                    // Add a new page for the shape image
                    Aspose.Pdf.Page shapePage = pdfDoc.Pages.Add();

                    // Load the image and add it to the page
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.ImageStream = new FileStream(shapeImagePaths[i], FileMode.Open, FileAccess.Read);
                    // Fit the image to the page dimensions
                    pdfImage.FixWidth = shapePage.PageInfo.Width;
                    pdfImage.FixHeight = shapePage.PageInfo.Height;
                    shapePage.Paragraphs.Add(pdfImage);

                    // Calculate the rectangle for the link annotation on the index page
                    float linkY = startY - i * lineHeight;
                    Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(linkX, linkY, linkX + linkWidth, linkY + linkHeight);

                    // Create a link annotation that points to the newly added shape page
                    Aspose.Pdf.Annotations.LinkAnnotation link = new Aspose.Pdf.Annotations.LinkAnnotation(indexPage, linkRect);
                    link.Action = new Aspose.Pdf.Annotations.GoToAction(shapePage);
                    indexPage.Annotations.Add(link);

                    // Add visible text for the link on the index page
                    Aspose.Pdf.Text.TextFragment linkText = new Aspose.Pdf.Text.TextFragment(shapeNames[i]);
                    linkText.Position = new Aspose.Pdf.Text.Position(linkX, linkY);
                    linkText.TextState.FontSize = 12;
                    indexPage.Paragraphs.Add(linkText);
                }

                // Save the combined PDF document
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF with index generated successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Clean up temporary images
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}