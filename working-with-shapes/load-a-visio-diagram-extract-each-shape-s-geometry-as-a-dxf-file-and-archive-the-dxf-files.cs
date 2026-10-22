using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a Visio file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare a folder to store the exported shape files
        string exportFolder = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "ShapeExports");
        try
        {
            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create export folder: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                int shapeCounter = 0;
                int pageNumber = 0;

                // Iterate through all pages (Page does not have an Index property, so we use a manual counter)
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        shapeCounter++;

                        // Build a unique file name for the shape export
                        string shapeFileName = $"Shape_{shapeCounter}_Page_{pageNumber}_ID_{shape.ID}.svg";
                        string shapeFilePath = Path.Combine(exportFolder, shapeFileName);

                        // Export the shape geometry to SVG (DXF export is not supported; SVG is used as the closest alternative)
                        try
                        {
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            // Export only the current shape; no additional page settings are required for shape-level export
                            shape.ToSvg(shapeFilePath, svgOptions);
                        }
                        catch (Exception exportEx)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {pageNumber}: {exportEx.Message}");
                        }
                    }
                }

                // After exporting all shapes, create a ZIP archive of the SVG files
                string zipPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "ShapeExports.zip");
                try
                {
                    if (File.Exists(zipPath))
                        File.Delete(zipPath);

                    ZipFile.CreateFromDirectory(exportFolder, zipPath);
                    Console.WriteLine($"Export completed. SVG files are archived at: {zipPath}");
                }
                catch (Exception zipEx)
                {
                    Console.Error.WriteLine($"Failed to create ZIP archive: {zipEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}