using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Path for the resulting HTML file
        string htmlOutputPath = "output.html";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(sourcePath);

            // Iterate through all pages and shapes to compress embedded images
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        try
                        {
                            // Original image bytes
                            byte[] originalBytes = shape.ForeignData.Value;

                            // Load the image using Aspose.Drawing (fully qualified to avoid ambiguity)
                            using (MemoryStream msIn = new MemoryStream(originalBytes))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(msIn))
                            using (MemoryStream msOut = new MemoryStream())
                            {
                                // Save the image as JPEG (default quality compression)
                                img.Save(msOut, Aspose.Drawing.Imaging.ImageFormat.Jpeg);
                                // Replace the foreign data with the compressed JPEG bytes
                                shape.ForeignData.Value = msOut.ToArray();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log image processing errors and continue with other shapes
                            Console.Error.WriteLine($"Failed to compress image in shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }
            }

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false,
                // Optional: set resolution for generated images (e.g., 96 DPI)
                Resolution = 96
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine("Visio diagram has been converted to HTML with compressed images.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}