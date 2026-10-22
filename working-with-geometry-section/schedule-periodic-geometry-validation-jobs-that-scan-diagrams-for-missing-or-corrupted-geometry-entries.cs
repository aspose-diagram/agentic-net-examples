using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    // Interval for periodic validation (in milliseconds). Adjust as needed.
    private const int ValidationIntervalMs = 5 * 60 * 1000; // 5 minutes

    static void Main(string[] args)
    {
        // Expect a single argument: the folder containing diagram files.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <DiagramsFolderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Create a timer that triggers the validation routine periodically.
        Timer timer = new Timer(state => ValidateAllDiagrams(folderPath), null, 0, ValidationIntervalMs);

        Console.WriteLine("Periodic geometry validation started. Press Enter to stop.");
        // Wait for user to press Enter to exit.
        Console.ReadLine();

        // Dispose the timer before exiting.
        timer.Dispose();
        Console.WriteLine("Validation stopped.");
    }

    /// <summary>
    /// Scans all supported diagram files in the specified folder and validates their geometry sections.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing diagram files.</param>
    private static void ValidateAllDiagrams(string folderPath)
    {
        // Define supported extensions (add more if needed).
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

        foreach (string ext in extensions)
        {
            string[] files = Directory.GetFiles(folderPath, "*" + ext, SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Guard: ensure the file still exists before processing.
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found (skipped): {filePath}");
                    continue;
                }

                try
                {
                    ValidateDiagram(filePath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Loads a diagram and checks each shape for missing or corrupted geometry entries.
    /// </summary>
    /// <param name="diagramPath">Full path to the diagram file.</param>
    private static void ValidateDiagram(string diagramPath)
    {
        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool diagramHasIssues = false;

                // Iterate through each page explicitly typed as Page.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape has at least one geometry.
                        if (shape.Geoms == null || shape.Geoms.Count == 0)
                        {
                            Console.WriteLine($"[Missing Geometry] File: {Path.GetFileName(diagramPath)}, Page: {pageIndex + 1}, Shape ID: {shape.ID}");
                            diagramHasIssues = true;
                            continue;
                        }

                        // Validate each geometry's coordinate collection.
                        for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                        {
                            // Cast the collection item to Geom.
                            Geom geom = (Geom)shape.Geoms[geomIdx];

                            // Ensure the geometry has coordinate data.
                            if (geom.CoordinateCol == null || geom.CoordinateCol.Count == 0)
                            {
                                Console.WriteLine($"[Corrupted Geometry] File: {Path.GetFileName(diagramPath)}, Page: {pageIndex + 1}, Shape ID: {shape.ID}, Geometry Index: {geomIdx}");
                                diagramHasIssues = true;
                            }
                        }
                    }
                }

                if (!diagramHasIssues)
                {
                    Console.WriteLine($"[OK] No geometry issues found in '{Path.GetFileName(diagramPath)}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any errors that occur while loading or processing the diagram.
            Console.Error.WriteLine($"Failed to validate diagram '{diagramPath}': {ex.Message}");
        }
    }
}