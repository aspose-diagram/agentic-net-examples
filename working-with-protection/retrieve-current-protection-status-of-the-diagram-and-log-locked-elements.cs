using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file to be examined
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Log global document protection settings
            // -----------------------------------------------------------------
            Console.WriteLine("=== Global Document Protection ===");
            Console.WriteLine($"Protect Backgrounds : {diagram.DocumentSettings.ProtectBkgnds}");
            Console.WriteLine($"Protect Masters     : {diagram.DocumentSettings.ProtectMasters}");
            Console.WriteLine($"Protect Shapes      : {diagram.DocumentSettings.ProtectShapes}");
            Console.WriteLine($"Protect Styles      : {diagram.DocumentSettings.ProtectStyles}");
            Console.WriteLine();

            // -----------------------------------------------------------------
            // Iterate through all pages and shapes to find locked elements
            // -----------------------------------------------------------------
            Console.WriteLine("=== Locked Shapes ===");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Collect names of protection flags that are set to TRUE
                    List<string> lockedProperties = new List<string>();

                    if (shape.Protection.LockAspect.Value == BOOL.True) lockedProperties.Add("LockAspect");
                    if (shape.Protection.LockBegin.Value == BOOL.True) lockedProperties.Add("LockBegin");
                    if (shape.Protection.LockCalcWH.Value == BOOL.True) lockedProperties.Add("LockCalcWH");
                    if (shape.Protection.LockCrop.Value == BOOL.True) lockedProperties.Add("LockCrop");
                    if (shape.Protection.LockCustProp.Value == BOOL.True) lockedProperties.Add("LockCustProp");
                    if (shape.Protection.LockDelete.Value == BOOL.True) lockedProperties.Add("LockDelete");
                    if (shape.Protection.LockEnd.Value == BOOL.True) lockedProperties.Add("LockEnd");
                    if (shape.Protection.LockFormat.Value == BOOL.True) lockedProperties.Add("LockFormat");
                    if (shape.Protection.LockFromGroupFormat.Value == BOOL.True) lockedProperties.Add("LockFromGroupFormat");
                    if (shape.Protection.LockGroup.Value == BOOL.True) lockedProperties.Add("LockGroup");
                    if (shape.Protection.LockHeight.Value == BOOL.True) lockedProperties.Add("LockHeight");
                    if (shape.Protection.LockMoveX.Value == BOOL.True) lockedProperties.Add("LockMoveX");
                    if (shape.Protection.LockMoveY.Value == BOOL.True) lockedProperties.Add("LockMoveY");
                    if (shape.Protection.LockRotate.Value == BOOL.True) lockedProperties.Add("LockRotate");
                    if (shape.Protection.LockSelect.Value == BOOL.True) lockedProperties.Add("LockSelect");
                    if (shape.Protection.LockTextEdit.Value == BOOL.True) lockedProperties.Add("LockTextEdit");
                    if (shape.Protection.LockThemeColors.Value == BOOL.True) lockedProperties.Add("LockThemeColors");
                    if (shape.Protection.LockThemeEffects.Value == BOOL.True) lockedProperties.Add("LockThemeEffects");
                    if (shape.Protection.LockVtxEdit.Value == BOOL.True) lockedProperties.Add("LockVtxEdit");
                    if (shape.Protection.LockWidth.Value == BOOL.True) lockedProperties.Add("LockWidth");

                    // Output shape information if any protection flags are set
                    if (lockedProperties.Count > 0)
                    {
                        Console.WriteLine($"Page: {page.Name} | Shape ID: {shape.ID} | Name: {shape.Name}");
                        Console.WriteLine("  Locked Properties: " + string.Join(", ", lockedProperties));
                    }
                }
            }

            // -----------------------------------------------------------------
            // Save the diagram (no modifications made, just to satisfy lifecycle rule)
            // -----------------------------------------------------------------
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}