using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string diagramPath = "input.vsdx"; // replace with your diagram file path
                Diagram diagram = new Diagram(diagramPath);

                // Prepare HTML output
                string htmlHeader = "<!DOCTYPE html>\n<html>\n<head>\n<meta charset=\"UTF-8\">\n<title>Shapes SVG Export</title>\n</head>\n<body>\n";
                string htmlFooter = "\n</body>\n</html>";
                string htmlContent = string.Empty;

                // Use the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Loop through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Generate a temporary SVG file path
                    string tempSvgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".svg");

                    // Export the shape to SVG
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    shape.ToSvg(tempSvgPath, svgOptions);

                    // Read the SVG content
                    string svgData = File.ReadAllText(tempSvgPath);

                    // Append the SVG markup to the HTML content
                    htmlContent += $"<div style=\"margin:10px; display:inline-block;\">\n{svgData}\n</div>\n";

                    // Clean up the temporary file
                    File.Delete(tempSvgPath);
                }

                // Combine header, content, and footer
                string finalHtml = htmlHeader + htmlContent + htmlFooter;

                // Save the HTML document
                string outputHtmlPath = "ShapesExport.html";
                File.WriteAllText(outputHtmlPath, finalHtml);

                Console.WriteLine($"Export completed. HTML file saved to: {outputHtmlPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }