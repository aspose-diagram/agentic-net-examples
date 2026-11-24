using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------
                // Retrieve and log document-level protection status
                // ------------------------------
                Console.WriteLine("Document Protection Status:");
                if (diagram.DocumentSettings.ProtectBkgnds == BOOL.True)
                    Console.WriteLine(" - Backgrounds are protected");
                if (diagram.DocumentSettings.ProtectMasters == BOOL.True)
                    Console.WriteLine(" - Masters (stencils) are protected");
                if (diagram.DocumentSettings.ProtectShapes == BOOL.True)
                    Console.WriteLine(" - Shapes are protected");
                if (diagram.DocumentSettings.ProtectStyles == BOOL.True)
                    Console.WriteLine(" - Styles are protected");

                // ------------------------------
                // Iterate through pages and shapes to log locked shape properties
                // ------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
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

                        // If any protection flags are set, log the shape information
                        if (lockedProperties.Count > 0)
                        {
                            string shapeInfo = $"Shape ID {shape.ID}";
                            if (!string.IsNullOrEmpty(shape.Name))
                                shapeInfo += $" (Name: {shape.Name})";

                            Console.WriteLine($"{shapeInfo} locked properties: {string.Join(", ", lockedProperties)}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}