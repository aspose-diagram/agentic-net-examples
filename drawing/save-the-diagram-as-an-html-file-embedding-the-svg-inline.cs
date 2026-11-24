using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output HTML file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: verify input file exists.
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
                // Prepare a StringWriter to build the final HTML content.
                using (StringWriter htmlWriter = new StringWriter())
                {
                    // Write basic HTML skeleton.
                    htmlWriter.WriteLine("<!DOCTYPE html>");
                    htmlWriter.WriteLine("<html>");
                    htmlWriter.WriteLine("<head>");
                    htmlWriter.WriteLine("<meta charset=\"UTF-8\">");
                    htmlWriter.WriteLine("<title>Visio Diagram Export</title>");
                    htmlWriter.WriteLine("</head>");
                    htmlWriter.WriteLine("<body>");

                    // Iterate over each page and export it to SVG, then embed inline.
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Define a temporary SVG file path for the current page.
                        string tempSvgPath = Path.Combine(Path.GetTempPath(), $"visio_page_{i}_{Guid.NewGuid()}.svg");

                        // Configure SVG save options to export only the current page.
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        svgOptions.PageIndex = i; // Export this page.

                        // Save the page as SVG.
                        diagram.Save(tempSvgPath, svgOptions);

                        // Read the generated SVG content.
                        string svgContent = File.ReadAllText(tempSvgPath);

                        // Embed the SVG inline within a div.
                        htmlWriter.WriteLine($"<div class=\"page\" id=\"page_{i + 1}\">");
                        htmlWriter.WriteLine(svgContent);
                        htmlWriter.WriteLine("</div>");

                        // Clean up the temporary SVG file.
                        try
                        {
                            File.Delete(tempSvgPath);
                        }
                        catch
                        {
                            // If deletion fails, ignore – the OS will clean up temp files eventually.
                        }
                    }

                    // Close HTML tags.
                    htmlWriter.WriteLine("</body>");
                    htmlWriter.WriteLine("</html>");

                    // Write the assembled HTML to the output file.
                    File.WriteAllText(outputHtmlPath, htmlWriter.ToString());
                }
            }

            Console.WriteLine($"Diagram successfully exported to HTML with inline SVG: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}