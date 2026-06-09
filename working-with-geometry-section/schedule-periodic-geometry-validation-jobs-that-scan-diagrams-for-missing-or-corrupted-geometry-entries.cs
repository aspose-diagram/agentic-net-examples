using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Interval for the periodic job (e.g., 60 seconds)
    private const int ValidationIntervalMs = 60_000;

    // Path to the Visio diagram to be validated
    private static readonly string DiagramPath = @"C:\Diagrams\sample.vsdx";

    // Timer that triggers the validation job
    private static Timer _validationTimer;

    static void Main()
    {
        Console.WriteLine("Starting periodic geometry validation job...");

        // Set up the timer to run ValidateAndReport every ValidationIntervalMs milliseconds
        _validationTimer = new Timer(ValidateAndReport, null, 0, ValidationIntervalMs);

        Console.WriteLine("Press ENTER to stop the scheduler.");
        Console.ReadLine(); // Wait for user input to stop

        // Clean up
        _validationTimer.Dispose();
        Console.WriteLine("Scheduler stopped.");
    }

    // Timer callback that loads the diagram, validates geometry, and reports results
    private static void ValidateAndReport(object state)
    {
        try
        {
            Console.WriteLine($"[{DateTime.Now}] Validation started.");

            // Load the diagram (using the constructor that accepts a file path)
            using (Diagram diagram = new Diagram(DiagramPath))
            {
                bool hasIssues = ValidateDiagramGeometry(diagram, out string report);

                // Output the validation report
                Console.WriteLine(report);

                // If issues are found, optionally take action (e.g., throw an exception)
                if (hasIssues)
                {
                    // Throwing an exception will surface the problem in logs
                    throw new Exception("Geometry validation failed: missing or corrupted geometry detected.");
                }
                else
                {
                    Console.WriteLine("Geometry validation passed. No issues found.");
                }
            }

            Console.WriteLine($"[{DateTime.Now}] Validation completed.\n");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during validation
            Console.WriteLine($"Error during validation: {ex.Message}");
        }
    }

    // Scans all pages and shapes for missing or corrupted geometry entries
    private static bool ValidateDiagramGeometry(Diagram diagram, out string report)
    {
        bool hasIssues = false;
        var reportBuilder = new System.Text.StringBuilder();

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Check for missing geometry (no Geoms defined)
                if (shape.Geoms == null || shape.Geoms.Count == 0)
                {
                    hasIssues = true;
                    reportBuilder.AppendLine($"[Missing Geometry] Page '{page.Name}' Shape ID {shape.ID} ('{shape.Name}') has no geometry.");
                    continue;
                }

                // Check each Geom for corrupted segments (Del flag set)
                for (int g = 0; g < shape.Geoms.Count; g++)
                {
                    Geom geom = (Geom)shape.Geoms[g];
                    if (geom == null || geom.CoordinateCol == null)
                    {
                        hasIssues = true;
                        reportBuilder.AppendLine($"[Corrupted Geometry] Page '{page.Name}' Shape ID {shape.ID} ('{shape.Name}') has a null Geom or CoordinateCol.");
                        continue;
                    }

                    foreach (var segment in geom.CoordinateCol)
                    {
                        // All geometry segment types inherit from a base class that contains a Del property
                        // The Del property is of type BOOL; true indicates the segment is marked for deletion
                        // Use reflection to safely access the Del property without assuming a specific segment type
                        var delProp = segment.GetType().GetProperty("Del");
                        if (delProp != null)
                        {
                            var delValue = delProp.GetValue(segment) as BOOL?;
                            if (delValue == BOOL.True)
                            {
                                hasIssues = true;
                                reportBuilder.AppendLine($"[Corrupted Geometry] Page '{page.Name}' Shape ID {shape.ID} ('{shape.Name}') has a deleted geometry segment.");
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (!hasIssues)
        {
            report = "No geometry issues detected.";
        }
        else
        {
            report = reportBuilder.ToString();
        }

        return hasIssues;
    }
}
