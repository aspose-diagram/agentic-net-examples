using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- Document‑level protection status ----------
                Console.WriteLine("=== Document Protection Status ===");

                // These properties are of type BOOL (not BoolValue), so compare directly
                BOOL protectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
                Console.WriteLine($"ProtectBkgnds: {(protectBkgnds == BOOL.True ? "Locked" : "Unlocked")}");

                BOOL protectMasters = diagram.DocumentSettings.ProtectMasters;
                Console.WriteLine($"ProtectMasters: {(protectMasters == BOOL.True ? "Locked" : "Unlocked")}");

                BOOL protectShapes = diagram.DocumentSettings.ProtectShapes;
                Console.WriteLine($"ProtectShapes: {(protectShapes == BOOL.True ? "Locked" : "Unlocked")}");

                BOOL protectStyles = diagram.DocumentSettings.ProtectStyles;
                Console.WriteLine($"ProtectStyles: {(protectStyles == BOOL.True ? "Locked" : "Unlocked")}");

                // ---------- Shape‑level protection (locked elements) ----------
                Console.WriteLine("\n=== Locked Shapes and Their Protection Flags ===");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are logically deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Collect the names of protection flags that are set to TRUE
                        var lockedFlags = new System.Collections.Generic.List<string>();

                        if (shape.Protection.LockAspect.Value == BOOL.True) lockedFlags.Add("LockAspect");
                        if (shape.Protection.LockBegin.Value == BOOL.True) lockedFlags.Add("LockBegin");
                        if (shape.Protection.LockCalcWH.Value == BOOL.True) lockedFlags.Add("LockCalcWH");
                        if (shape.Protection.LockCrop.Value == BOOL.True) lockedFlags.Add("LockCrop");
                        if (shape.Protection.LockCustProp.Value == BOOL.True) lockedFlags.Add("LockCustProp");
                        if (shape.Protection.LockDelete.Value == BOOL.True) lockedFlags.Add("LockDelete");
                        if (shape.Protection.LockEnd.Value == BOOL.True) lockedFlags.Add("LockEnd");
                        if (shape.Protection.LockFormat.Value == BOOL.True) lockedFlags.Add("LockFormat");
                        if (shape.Protection.LockFromGroupFormat.Value == BOOL.True) lockedFlags.Add("LockFromGroupFormat");
                        if (shape.Protection.LockGroup.Value == BOOL.True) lockedFlags.Add("LockGroup");
                        if (shape.Protection.LockHeight.Value == BOOL.True) lockedFlags.Add("LockHeight");
                        if (shape.Protection.LockMoveX.Value == BOOL.True) lockedFlags.Add("LockMoveX");
                        if (shape.Protection.LockMoveY.Value == BOOL.True) lockedFlags.Add("LockMoveY");
                        if (shape.Protection.LockRotate.Value == BOOL.True) lockedFlags.Add("LockRotate");
                        if (shape.Protection.LockSelect.Value == BOOL.True) lockedFlags.Add("LockSelect");
                        if (shape.Protection.LockTextEdit.Value == BOOL.True) lockedFlags.Add("LockTextEdit");
                        if (shape.Protection.LockThemeColors.Value == BOOL.True) lockedFlags.Add("LockThemeColors");
                        if (shape.Protection.LockThemeEffects.Value == BOOL.True) lockedFlags.Add("LockThemeEffects");
                        if (shape.Protection.LockVtxEdit.Value == BOOL.True) lockedFlags.Add("LockVtxEdit");
                        if (shape.Protection.LockWidth.Value == BOOL.True) lockedFlags.Add("LockWidth");

                        // If any protection flags are locked, output shape details
                        if (lockedFlags.Count > 0)
                        {
                            string masterName = shape.Master != null ? shape.Master.Name : "N/A";
                            Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Name: {shape.Name}, Master: {masterName}");
                            Console.WriteLine("  Locked Flags: " + string.Join(", ", lockedFlags));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}