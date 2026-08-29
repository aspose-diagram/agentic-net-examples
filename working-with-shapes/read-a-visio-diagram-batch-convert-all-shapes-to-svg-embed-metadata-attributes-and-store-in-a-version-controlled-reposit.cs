using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file or pass as argument.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory for the generated SVG files.
        string outputDir = args.Length > 1 ? args[1] : "SvgExport";
        // Guard: ensure the output directory path is not null or empty.
        if (string.IsNullOrWhiteSpace(outputDir))
        {
            Console.Error.WriteLine("Output directory path is invalid.");
            return;
        }
        // Create the output directory if it does not already exist.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare SVG save options – hide hidden pages and export guides.
                    SVGSaveOptions svgOptions = new SVGSaveOptions
                    {
                        ExportHiddenPage = false,
                        ExportGuideShapes = false,
                        SVGFitToViewPort = true,
                        ExportElementAsRectTag = true
                    };

                    // Build a unique file name for the shape SVG.
                    string svgFileName = $"shape_{shape.ID}.svg";
                    string svgPath = Path.Combine(outputDir, svgFileName);

                    // Export the shape to an SVG file.
                    shape.ToSvg(svgPath, svgOptions);

                    // Embed simple metadata into the generated SVG.
                    // Read the SVG content.
                    string svgContent = File.ReadAllText(svgPath);

                    // Find the opening <svg> tag to insert metadata after it.
                    int svgTagEnd = svgContent.IndexOf('>');
                    if (svgTagEnd > -1)
                    {
                        // Build a metadata block with shape ID, name, and master name.
                        string metadata = $"\n  <metadata>\n    <shapeId>{shape.ID}</shapeId>\n    <shapeName>{System.Security.SecurityElement.Escape(shape.NameU ?? string.Empty)}</shapeName>\n    <masterName>{System.Security.SecurityElement.Escape(shape.Master?.Name ?? string.Empty)}</masterName>\n  </metadata>\n";

                        // Insert the metadata block right after the <svg> tag.
                        svgContent = svgContent.Insert(svgTagEnd + 1, metadata);

                        // Write the modified content back to the SVG file.
                        File.WriteAllText(svgPath, svgContent);
                    }
                }
            }

            // Indicate successful batch export.
            Console.WriteLine($"All shapes exported to SVG in folder: {Path.GetFullPath(outputDir)}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}