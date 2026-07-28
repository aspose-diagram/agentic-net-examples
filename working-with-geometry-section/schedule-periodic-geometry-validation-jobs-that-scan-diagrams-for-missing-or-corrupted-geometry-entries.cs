using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

namespace DiagramGeometryValidator
{
    // Helper class that performs geometry validation on Visio diagrams
    public static class GeometryValidator
    {
        // Scans all diagram files in the specified folder
        public static void ValidateAll(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.AllDirectories);
            if (diagramFiles.Length == 0)
            {
                Console.WriteLine($"No Visio files found in folder: {folderPath}");
                return;
            }

            foreach (string file in diagramFiles)
            {
                Console.WriteLine($"Validating diagram: {Path.GetFileName(file)}");
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(file);

                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check for missing geometry
                            if (shape.Geoms == null || shape.Geoms.Count == 0)
                            {
                                Console.WriteLine($"  Shape ID {shape.ID} ('{shape.NameU}') has no geometry.");
                                continue;
                            }

                            // Validate each geometry segment
                            foreach (Geom geom in shape.Geoms)
                            {
                                if (geom == null)
                                {
                                    Console.WriteLine($"  Shape ID {shape.ID} contains a null Geom object.");
                                    continue;
                                }

                                if (geom.CoordinateCol == null)
                                {
                                    Console.WriteLine($"  Shape ID {shape.ID} has a Geom with null CoordinateCol.");
                                    continue;
                                }

                                // Simple sanity check: ensure at least one coordinate exists
                                if (geom.CoordinateCol.Count == 0)
                                {
                                    Console.WriteLine($"  Shape ID {shape.ID} has an empty CoordinateCol.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{file}': {ex.Message}");
                }
            }
        }
    }

    // Main program that schedules periodic validation
    public class Program
    {
        // Interval for periodic validation (e.g., 1 hour)
        private static readonly TimeSpan ValidationInterval = TimeSpan.FromHours(1);
        // Folder containing Visio diagrams to validate
        private const string DiagramsFolder = "Diagrams";

        private static Timer _validationTimer;

        public static void Main(string[] args)
        {
            // Initial run
            GeometryValidator.ValidateAll(DiagramsFolder);

            // Schedule periodic validation
            _validationTimer = new Timer(
                callback: state => GeometryValidator.ValidateAll(DiagramsFolder),
                state: null,
                dueTime: ValidationInterval,
                period: ValidationInterval);

            Console.WriteLine("Geometry validation scheduler started. Press Enter to exit.");
            Console.ReadLine();

            // Clean up timer
            _validationTimer?.Dispose();
        }
    }
}