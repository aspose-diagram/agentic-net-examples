using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load the first diagram inside a using block.
            using (Diagram diagram1 = new Diagram(diagramPath1))
            {
                // Load the second diagram inside a nested using block.
                using (Diagram diagram2 = new Diagram(diagramPath2))
                {
                    // Retrieve global protection settings from both diagrams.
                    BOOL protectBkgnds1 = diagram1.DocumentSettings.ProtectBkgnds;
                    BOOL protectBkgnds2 = diagram2.DocumentSettings.ProtectBkgnds;

                    BOOL protectMasters1 = diagram1.DocumentSettings.ProtectMasters;
                    BOOL protectMasters2 = diagram2.DocumentSettings.ProtectMasters;

                    BOOL protectShapes1 = diagram1.DocumentSettings.ProtectShapes;
                    BOOL protectShapes2 = diagram2.DocumentSettings.ProtectShapes;

                    BOOL protectStyles1 = diagram1.DocumentSettings.ProtectStyles;
                    BOOL protectStyles2 = diagram2.DocumentSettings.ProtectStyles;

                    bool anyMismatch = false;

                    // Compare each protection flag and report mismatches.
                    anyMismatch |= ReportMismatch("ProtectBkgnds", protectBkgnds1, protectBkgnds2);
                    anyMismatch |= ReportMismatch("ProtectMasters", protectMasters1, protectMasters2);
                    anyMismatch |= ReportMismatch("ProtectShapes", protectShapes1, protectShapes2);
                    anyMismatch |= ReportMismatch("ProtectStyles", protectStyles1, protectStyles2);

                    if (!anyMismatch)
                    {
                        Console.WriteLine("All global protection settings match between the two diagrams.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    /// <summary>
    /// Compares two BOOL protection values and writes a message if they differ.
    /// Returns true if a mismatch was found.
    /// </summary>
    private static bool ReportMismatch(string settingName, BOOL value1, BOOL value2)
    {
        if (value1 != value2)
        {
            Console.WriteLine($"Mismatch in {settingName}: Diagram1 = {value1}, Diagram2 = {value2}");
            return true;
        }
        return false;
    }
}