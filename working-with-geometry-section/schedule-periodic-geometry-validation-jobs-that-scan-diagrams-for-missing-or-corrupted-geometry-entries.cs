using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    // Interval in minutes for periodic validation
    private const int ValidationIntervalMinutes = 60;

    static void Main(string[] args)
    {
        // Expect a folder path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No folder path provided.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Error: Folder does not exist: {folderPath}");
            return;
        }

        // Create a timer that triggers validation at the defined interval
        Timer timer = new Timer(state => RunValidation(folderPath), null,
            TimeSpan.Zero, TimeSpan.FromMinutes(ValidationIntervalMinutes));

        Console.WriteLine($"Geometry validation scheduled every {ValidationIntervalMinutes} minute(s) for folder: {folderPath}");
        Console.WriteLine("Press Enter to exit...");

        // Keep the application running until the user presses Enter
        Console.ReadLine();

        // Dispose the timer before exiting
        timer.Dispose();
    }

    // Executes validation for all diagram files in the specified folder
    private static void RunValidation(string folderPath)
    {
        try
        {
            Console.WriteLine($"[{DateTime.Now}] Starting geometry validation...");

            // Get all supported Visio files (VSD, VSDX, VSTX, VSSX, VTX, etc.)
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in diagramFiles)
            {
                // Simple filter based on file extension
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsd" && ext != ".vsdx" && ext != ".vstx" && ext != ".vssx" && ext != ".vtx")
                {
                    continue;
                }

                // Guard: ensure the file exists (redundant but follows the rule)
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                ValidateDiagram(filePath);
            }

            Console.WriteLine($"[{DateTime.Now}] Geometry validation completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during validation: {ex.Message}");
        }
    }

    // Validates a single diagram file for missing or corrupted geometry entries
    private static void ValidateDiagram(string diagramPath)
    {
        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool diagramHasIssues = false;

                // Iterate through each page explicitly typed as Page
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Guard: ensure the page and its shape collection are not null
                    if (page == null || page.Shapes == null)
                    {
                        continue;
                    }

                    // Iterate through each shape on the page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                        {
                            continue;
                        }

                        // Check for missing geometry sections
                        if (shape.Geoms == null || shape.Geoms.Count == 0)
                        {
                            Console.WriteLine($"[Warning] Shape ID {shape.ID} on page {pageIndex + 1} is missing geometry.");
                            diagramHasIssues = true;
                            continue;
                        }

                        // Validate each geometry within the shape
                        for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                        {
                            Geom geom = shape.Geoms[geomIdx];

                            // Guard: ensure the geometry's coordinate collection exists
                            if (geom.CoordinateCol == null || geom.CoordinateCol.Count == 0)
                            {
                                Console.WriteLine($"[Warning] Shape ID {shape.ID} on page {pageIndex + 1} has a geometry with no coordinates (Geom index {geomIdx}).");
                                diagramHasIssues = true;
                            }
                        }
                    }
                }

                if (!diagramHasIssues)
                {
                    Console.WriteLine($"[Info] No geometry issues found in diagram: {Path.GetFileName(diagramPath)}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
        }
    }
}