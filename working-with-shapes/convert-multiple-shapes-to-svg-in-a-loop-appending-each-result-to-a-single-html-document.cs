using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the HTML output file.
            using (StreamWriter htmlWriter = new StreamWriter(outputHtmlPath, false))
            {
                // Write basic HTML header.
                htmlWriter.WriteLine("<!DOCTYPE html>");
                htmlWriter.WriteLine("<html lang=\"en\">");
                htmlWriter.WriteLine("<head><meta charset=\"UTF-8\"><title>Shapes SVG Export</title></head>");
                htmlWriter.WriteLine("<body>");

                // Iterate through each page.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        try
                        {
                            // Create a temporary file path for the SVG output.
                            string tempSvgPath = Path.Combine(Path.GetTempPath(),
                                $"shape_{shape.ID}_{Guid.NewGuid()}.svg");

                            // Export the shape to SVG using SVGSaveOptions.
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            shape.ToSvg(tempSvgPath, svgOptions);

                            // Read the generated SVG content.
                            string svgContent = File.ReadAllText(tempSvgPath);

                            // Append the SVG markup to the HTML document.
                            htmlWriter.WriteLine(svgContent);

                            // Clean up the temporary SVG file.
                            File.Delete(tempSvgPath);
                        }
                        catch (Exception exShape)
                        {
                            // Log shape-specific errors but continue processing other shapes.
                            Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {exShape.Message}");
                        }
                    }
                }

                // Write HTML footer.
                htmlWriter.WriteLine("</body>");
                htmlWriter.WriteLine("</html>");
            }

            Console.WriteLine($"SVG export completed. HTML file created at: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or overall processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}