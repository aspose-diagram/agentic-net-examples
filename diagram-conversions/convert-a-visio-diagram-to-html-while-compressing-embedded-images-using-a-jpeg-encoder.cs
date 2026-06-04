using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputPath = "output.html";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Compress embedded images (foreign shapes) to JPEG
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] originalData = shape.ForeignData.Value;
                        using (var inputStream = new MemoryStream(originalData))
                        using (var image = Aspose.Drawing.Image.FromStream(inputStream))
                        using (var outputStream = new MemoryStream())
                        {
                            // Re-encode the image as JPEG
                            image.Save(outputStream, ImageFormat.Jpeg);
                            shape.ForeignData.Value = outputStream.ToArray();
                        }
                    }
                }
            }

            // Set HTML save options (adjust additional options as needed)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false,
                IsExportComments = false
            };

            // Save the diagram as HTML
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}