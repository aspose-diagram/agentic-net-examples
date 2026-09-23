using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
    {
        // Interval for periodic validation (e.g., every 5 minutes)
        private static readonly TimeSpan ValidationInterval = TimeSpan.FromMinutes(5);

        // Folder containing Visio diagram files to validate
        private const string DiagramsFolder = "Diagrams";

        static void Main()
        {
            // Ensure the diagrams folder exists
            if (!Directory.Exists(DiagramsFolder))
            {
                Console.WriteLine($"Folder \"{DiagramsFolder}\" does not exist. Creating it.");
                Directory.CreateDirectory(DiagramsFolder);
            }

            // Set up a timer that triggers the validation routine at the defined interval
            Timer timer = new Timer(ValidateAllDiagrams, null, TimeSpan.Zero, ValidationInterval);

            Console.WriteLine("Diagram geometry validator started.");
            Console.WriteLine($"Scanning folder \"{DiagramsFolder}\" every {ValidationInterval.TotalMinutes} minutes.");
            Console.WriteLine("Press Enter to exit.");

            // Keep the application running until the user decides to stop it
            Console.ReadLine();

            // Dispose the timer before exiting
            timer.Dispose();
        }

        // Timer callback that processes all diagram files in the target folder
        private static void ValidateAllDiagrams(object state)
        {
            try
            {
                string[] diagramFiles = Directory.GetFiles(DiagramsFolder, "*.vsdx", SearchOption.AllDirectories);

                if (diagramFiles.Length == 0)
                {
                    Console.WriteLine($"[{DateTime.Now}] No diagram files found in \"{DiagramsFolder}\".");
                    return;
                }

                foreach (string filePath in diagramFiles)
                {
                    Console.WriteLine($"[{DateTime.Now}] Validating geometry for \"{Path.GetFileName(filePath)}\".");

                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Perform geometry validation
                    ValidateGeometry(diagram, filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during validation: {ex.Message}");
            }
        }

        // Scans a diagram for missing or corrupted geometry entries
        private static void ValidateGeometry(Diagram diagram, string diagramPath)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // If the shape has no geometry sections, report it
                    if (shape.Geoms == null || shape.Geoms.Count == 0)
                    {
                        ReportIssue(diagramPath, page, shape, "Missing geometry sections.");
                        continue;
                    }

                    // Examine each geometry section
                    foreach (Geom geom in shape.Geoms)
                    {
                        // If a geometry section has no coordinate entries, report it
                        if (geom.CoordinateCol == null || geom.CoordinateCol.Count == 0)
                        {
                            ReportIssue(diagramPath, page, shape, "Geometry section contains no coordinate data.");
                            continue;
                        }

                        // Check each coordinate segment for explicit deletion flag
                        foreach (object segment in geom.CoordinateCol)
                        {
                            // MoveTo segment
                            if (segment is MoveTo move && move.Del == BOOL.True)
                            {
                                ReportIssue(diagramPath, page, shape, "MoveTo segment marked as deleted.");
                            }
                            // LineTo segment
                            else if (segment is LineTo line && line.Del == BOOL.True)
                            {
                                ReportIssue(diagramPath, page, shape, "LineTo segment marked as deleted.");
                            }
                            // ArcTo segment
                            else if (segment is ArcTo arc && arc.Del == BOOL.True)
                            {
                                ReportIssue(diagramPath, page, shape, "ArcTo segment marked as deleted.");
                            }
                            // SplineKnot segment
                            else if (segment is SplineKnot spline && spline.Del == BOOL.True)
                            {
                                ReportIssue(diagramPath, page, shape, "SplineKnot segment marked as deleted.");
                            }
                            // Additional segment types can be added here following the same pattern
                        }
                    }
                }
            }
        }

        // Helper method to output a validation issue
        private static void ReportIssue(string diagramPath, Page page, Shape shape, string message)
        {
            Console.WriteLine($"[Issue] Diagram: {Path.GetFileName(diagramPath)} | Page: {page.Name} (ID={page.ID}) | Shape ID: {shape.ID} | {message}");
        }
    }