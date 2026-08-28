using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Interval for the periodic job (e.g., 5 minutes)
        private const int ValidationIntervalMs = 5 * 60 * 1000;

        // Folder containing Visio files to validate
        private static readonly string DiagramsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Diagrams");

        // Timer that triggers the validation job
        private static Timer _validationTimer;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Diagram Geometry Validation Service...");

            // Ensure the diagrams folder exists
            if (!Directory.Exists(DiagramsFolder))
            {
                Console.WriteLine($"Diagrams folder not found: {DiagramsFolder}");
                return;
            }

            // Set up the timer to run the validation method periodically
            _validationTimer = new Timer(ValidateAllDiagrams, null, 0, ValidationIntervalMs);

            // Prevent the application from exiting
            Console.WriteLine("Press Enter to stop the service.");
            Console.ReadLine();

            // Clean up timer
            _validationTimer.Dispose();
            Console.WriteLine("Service stopped.");
        }

        // Timer callback that validates all diagrams in the folder
        private static void ValidateAllDiagrams(object state)
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now}] Validation run started.");

                string[] diagramFiles = Directory.GetFiles(DiagramsFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
                foreach (string filePath in diagramFiles)
                {
                    ValidateDiagram(filePath);
                }

                Console.WriteLine($"[{DateTime.Now}] Validation run completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during validation: {ex.Message}");
            }
        }

        // Loads a diagram, scans for geometry issues, and reports them
        private static void ValidateDiagram(string filePath)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Collect problematic shape IDs
                List<long> problematicShapeIds = new List<long>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check for missing geometry (no Geoms)
                        if (shape.Geoms == null || shape.Geoms.Count == 0)
                        {
                            problematicShapeIds.Add(shape.ID);
                            continue;
                        }

                        // Examine each Geom for deleted segments
                        foreach (Geom geom in shape.Geoms)
                        {
                            if (geom == null || geom.CoordinateCol == null)
                                continue;

                            foreach (object segmentObj in geom.CoordinateCol)
                            {
                                // All geometry segment types inherit a Del property of type BOOL
                                // Use dynamic to access it safely
                                dynamic segment = segmentObj;
                                try
                                {
                                    if (segment.Del == BOOL.True)
                                    {
                                        problematicShapeIds.Add(shape.ID);
                                        // No need to check further segments for this shape
                                        break;
                                    }
                                }
                                catch
                                {
                                    // Segment does not have a Del property; ignore
                                }
                            }

                            // If already flagged, skip remaining Geoms
                            if (problematicShapeIds.Contains(shape.ID))
                                break;
                        }
                    }
                }

                // Report results
                if (problematicShapeIds.Count > 0)
                {
                    Console.WriteLine($"Diagram: {Path.GetFileName(filePath)}");
                    Console.WriteLine("  Shapes with missing or corrupted geometry:");
                    foreach (long shapeId in problematicShapeIds)
                    {
                        Console.WriteLine($"    Shape ID: {shapeId}");
                    }
                }
                else
                {
                    Console.WriteLine($"Diagram: {Path.GetFileName(filePath)} - No geometry issues found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }