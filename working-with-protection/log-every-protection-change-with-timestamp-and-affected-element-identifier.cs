using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Helper method to log protection changes with timestamp and element identifier
    static void LogChange(string elementId, string changeDescription)
    {
        // Write log entry to standard error to separate from normal output
        Console.Error.WriteLine($"{DateTime.Now:O} | Element: {elementId} | Change: {changeDescription}");
    }

    static void Main(string[] args)
    {
        // Input Visio file path (first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // ---------- Global document protection changes ----------
            // Protect backgrounds
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            LogChange("Document", "ProtectBkgnds set to TRUE");

            // Protect masters
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            LogChange("Document", "ProtectMasters set to TRUE");

            // Protect shapes
            diagram.DocumentSettings.ProtectShapes = BOOL.True;
            LogChange("Document", "ProtectShapes set to TRUE");

            // Protect styles
            diagram.DocumentSettings.ProtectStyles = BOOL.True;
            LogChange("Document", "ProtectStyles set to TRUE");

            // ---------- Shape-level protection changes ----------
            // Iterate through all pages and shapes to apply locks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example: lock movement on X axis
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockMoveX set to TRUE");

                    // Example: lock movement on Y axis
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockMoveY set to TRUE");

                    // Example: lock width resizing
                    shape.Protection.LockWidth.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockWidth set to TRUE");

                    // Example: lock height resizing
                    shape.Protection.LockHeight.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockHeight set to TRUE");

                    // Example: lock rotation
                    shape.Protection.LockRotate.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockRotate set to TRUE");

                    // Example: lock deletion
                    shape.Protection.LockDelete.Value = BOOL.True;
                    LogChange($"Shape ID {shape.ID}", "LockDelete set to TRUE");
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}