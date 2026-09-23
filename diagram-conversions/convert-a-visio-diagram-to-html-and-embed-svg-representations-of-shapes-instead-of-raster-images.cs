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

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Directory where HTML export will place images
            string htmlOutputPath = "output.html";

            // Export each shape to an individual SVG file
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique SVG file name for the shape
                    string svgFileName = $"shape_{shape.ID}.svg";

                    // Export the shape to SVG
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    shape.ToSvg(svgFileName, svgOptions);
                }
            }

            // Export the whole diagram to HTML (default raster PNG images)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            diagram.Save(htmlOutputPath, htmlOptions);

            // The HTML export creates an auxiliary folder with the same name as the HTML file (without extension)
            string htmlFolder = Path.Combine(Path.GetDirectoryName(htmlOutputPath) ?? "", Path.GetFileNameWithoutExtension(htmlOutputPath));

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(htmlOutputPath);

            // Replace each PNG image reference with the corresponding inline SVG
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string pngFileName = $"shape_{shape.ID}.png";
                    string svgFileName = $"shape_{shape.ID}.svg";

                    string pngPath = Path.Combine(htmlFolder, pngFileName);
                    string svgPath = Path.Combine(Directory.GetCurrentDirectory(), svgFileName);

                    if (File.Exists(pngPath) && File.Exists(svgPath))
                    {
                        // Load SVG content
                        string svgContent = File.ReadAllText(svgPath);

                        // Remove XML declaration if present (HTML cannot have it inside <svg>)
                        if (svgContent.StartsWith("<?xml"))
                        {
                            int idx = svgContent.IndexOf("?>");
                            if (idx > -1)
                            {
                                svgContent = svgContent.Substring(idx + 2).TrimStart();
                            }
                        }

                        // Build the replacement string: <img src="..."> => inline SVG
                        string imgTagPattern = $"src=\"{pngFileName}\"";
                        string replacement = $"src=\"data:image/svg+xml;base64,{Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svgContent))}\"";

                        // Perform the replacement in the HTML content
                        htmlContent = htmlContent.Replace(imgTagPattern, replacement);
                    }
                }
            }

            // Write the modified HTML with embedded SVGs
            string finalHtmlPath = "output_embedded.html";
            File.WriteAllText(finalHtmlPath, htmlContent);

            Console.WriteLine("Conversion completed.");
            Console.WriteLine($"HTML with embedded SVG saved to: {finalHtmlPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
