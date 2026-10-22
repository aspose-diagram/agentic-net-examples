using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- Document protection changes ----------
                // Example: protect backgrounds and masters
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                LogProtectionChange("Document", "ProtectBkgnds", BOOL.True);

                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                LogProtectionChange("Document", "ProtectMasters", BOOL.True);

                // ---------- Shape protection changes ----------
                // Iterate through all pages and shapes
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over each shape in the page
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;

                        // Example: lock aspect ratio for the shape
                        shape.Protection.LockAspect.Value = BOOL.True;
                        LogProtectionChange($"Shape ID {shapeId}", "LockAspect", BOOL.True);

                        // Example: lock movement on X axis
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        LogProtectionChange($"Shape ID {shapeId}", "LockMoveX", BOOL.True);

                        // Example: lock deletion
                        shape.Protection.LockDelete.Value = BOOL.True;
                        LogProtectionChange($"Shape ID {shapeId}", "LockDelete", BOOL.True);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Logs a protection change with timestamp, element identifier, property name, and new value.
    /// </summary>
    static void LogProtectionChange(string elementIdentifier, string propertyName, BOOL newValue)
    {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
        Console.WriteLine($"{timestamp} - {elementIdentifier} - {propertyName} set to {newValue}");
    }
}