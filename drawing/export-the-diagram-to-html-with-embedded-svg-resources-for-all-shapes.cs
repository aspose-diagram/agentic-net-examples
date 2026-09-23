using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Implements IStreamProvider to supply SVG streams for each shape during HTML export
    public class SvgStreamProvider : IStreamProvider
    {
        private readonly Diagram _diagram;

        public SvgStreamProvider(Diagram diagram)
        {
            _diagram = diagram;
        }

        // Called by Aspose.Diagram when a resource stream is required
        public void InitStream(StreamProviderOptions options)
        {
            // The requested file name (e.g., "shape_12.svg")
            string fileName = options.DefaultPath;

            // Determine a temporary file path to store the generated SVG
            string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            // Extract the shape ID from the file name using a regular expression
            long shapeId = 0;
            Match match = Regex.Match(fileName, @"shape_(\d+)\.svg", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                long.TryParse(match.Groups[1].Value, out shapeId);
            }

            // If a valid shape ID was found, locate the shape and export it to SVG
            if (shapeId > 0)
            {
                Shape targetShape = null;
                foreach (Page page in _diagram.Pages)
                {
                    // GetShape returns null if the ID does not exist on this page
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                        break;
                }

                if (targetShape != null)
                {
                    // Export the shape to an SVG file using SVGSaveOptions
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    targetShape.ToSvg(tempFilePath, svgOptions);
                }
            }

            // Open a read‑only stream to the generated SVG (or an empty stream if not found)
            if (File.Exists(tempFilePath))
                options.Stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
            else
                options.Stream = Stream.Null;
        }

        // Called after the resource has been consumed
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Guard to ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options (no CustomPath property exists)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    ExportHiddenPage = false,
                    IsExportComments = false
                };

                // Assign the custom stream provider that supplies SVG data for each shape
                htmlOptions.StreamProvider = new SvgStreamProvider(diagram);

                // Export the diagram to HTML with embedded SVG resources
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML with embedded SVG at: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error console
                Console.Error.WriteLine($"Error during export: {ex.Message}");
            }
        }
    }
}