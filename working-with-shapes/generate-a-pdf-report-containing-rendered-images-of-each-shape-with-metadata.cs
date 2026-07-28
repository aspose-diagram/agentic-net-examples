using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file (replace with actual path)
        string visioPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Output PDF report file
        string pdfReportPath = "ShapeReport.pdf";

        // Load the Visio diagram inside a try/catch to capture loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Temporary folder to store shape images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ShapeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold image paths and metadata for each shape
        List<(string ImagePath, string Metadata)> shapeData = new List<(string, string)>();

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Prepare image file path
                string imageFile = Path.Combine(tempFolder, $"shape_{shape.ID}.png");

                // Export shape to PNG image inside a try/catch
                try
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    shape.ToImage(imageFile, imgOptions);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error exporting shape ID {shape.ID} to image: {ex.Message}");
                    continue;
                }

                // Build metadata string
                string masterName = shape.Master != null ? shape.Master.Name : "N/A";
                string metadata = $"Shape ID: {shape.ID}\nName: {shape.Name}\nMaster: {masterName}";

                // Store for PDF generation
                shapeData.Add((imageFile, metadata));
            }
        }

        // Create PDF document (fully qualified to avoid namespace clash)
        Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

        // Add a page per shape with its image and metadata
        foreach (var item in shapeData)
        {
            // Add a new page
            Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

            // Add the shape image
            try
            {
                using (FileStream imgStream = new FileStream(item.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imgStream
                    };
                    pdfPage.Paragraphs.Add(pdfImage);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding image to PDF for shape file {item.ImagePath}: {ex.Message}");
                continue;
            }

            // Add metadata text below the image
            Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(item.Metadata);
            tf.TextState.FontSize = 10;
            pdfPage.Paragraphs.Add(tf);
        }

        // Save the PDF report inside a try/catch
        try
        {
            pdfDoc.Save(pdfReportPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving PDF report: {ex.Message}");
        }

        // Optional: clean up temporary images
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not stop the program
        }
    }
}