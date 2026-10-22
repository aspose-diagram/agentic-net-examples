using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // StringBuilder to compose the final HTML document.
                StringBuilder htmlBuilder = new StringBuilder();

                // Write basic HTML header.
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("    <title>Diagram Export</title>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("    <h1>Diagram Export – Shapes as Base64 Images</h1>");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    htmlBuilder.AppendLine($"    <h2>Page {pageIndex + 1}</h2>");

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Generate a temporary PNG file for the shape.
                        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

                        // Export the shape to PNG using ImageSaveOptions.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(tempImagePath, imgOptions);

                        // Read the PNG bytes and convert to a Base64 data URI.
                        byte[] imageBytes = File.ReadAllBytes(tempImagePath);
                        string base64 = Convert.ToBase64String(imageBytes);
                        string dataUri = $"data:image/png;base64,{base64}";

                        // Clean up the temporary file.
                        File.Delete(tempImagePath);

                        // Append the shape representation to the HTML.
                        htmlBuilder.AppendLine("    <div style=\"margin-bottom:20px;\">");
                        htmlBuilder.AppendLine($"        <p>Shape ID: {shape.ID}</p>");
                        htmlBuilder.AppendLine($"        <img src=\"{dataUri}\" alt=\"Shape {shape.ID}\" style=\"max-width:100%; height:auto;\"/>");
                        htmlBuilder.AppendLine("    </div>");
                    }
                }

                // Close HTML tags.
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the composed HTML to the output file.
                File.WriteAllText(outputPath, htmlBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}