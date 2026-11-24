using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: MergeEventSections <sourceVisio> <targetVisio> <outputVisio>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard: ensure source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard: ensure target file exists
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram inside a using block
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram inside a using block
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Iterate through each page in the source diagram
                    foreach (Page srcPage in sourceDiagram.Pages)
                    {
                        // Find a page in the target diagram with the same name
                        Page tgtPage = null;
                        foreach (Page p in targetDiagram.Pages)
                        {
                            if (p.Name == srcPage.Name)
                            {
                                tgtPage = p;
                                break;
                            }
                        }

                        // If no matching page, skip this source page
                        if (tgtPage == null)
                            continue;

                        // Build a lookup of target shapes by NameU for quick matching
                        var targetShapeLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                        foreach (Shape tgtShape in tgtPage.Shapes)
                        {
                            if (tgtShape.Del == BOOL.False && !string.IsNullOrEmpty(tgtShape.NameU))
                            {
                                targetShapeLookup[tgtShape.NameU] = tgtShape;
                            }
                        }

                        // Iterate through each shape in the source page
                        foreach (Shape srcShape in srcPage.Shapes)
                        {
                            // Skip deleted shapes
                            if (srcShape.Del == BOOL.True)
                                continue;

                            // Shape must have a NameU to match
                            if (string.IsNullOrEmpty(srcShape.NameU))
                                continue;

                            // Find matching target shape
                            if (targetShapeLookup.TryGetValue(srcShape.NameU, out Shape tgtShape))
                            {
                                // Copy EventDrop formula
                                if (srcShape.Event.EventDrop?.Ufe != null)
                                    tgtShape.Event.EventDrop.Ufe.F = srcShape.Event.EventDrop.Ufe.F;

                                // Copy EventDblClick formula
                                if (srcShape.Event.EventDblClick?.Ufe != null)
                                    tgtShape.Event.EventDblClick.Ufe.F = srcShape.Event.EventDblClick.Ufe.F;

                                // Copy EventXFMod formula
                                if (srcShape.Event.EventXFMod?.Ufe != null)
                                    tgtShape.Event.EventXFMod.Ufe.F = srcShape.Event.EventXFMod.Ufe.F;

                                // Copy EventMultiDrop formula
                                if (srcShape.Event.EventMultiDrop?.Ufe != null)
                                    tgtShape.Event.EventMultiDrop.Ufe.F = srcShape.Event.EventMultiDrop.Ufe.F;

                                // Copy TheText formula (if present)
                                if (srcShape.Event.TheText?.Ufe != null)
                                    tgtShape.Event.TheText.Ufe.F = srcShape.Event.TheText.Ufe.F;

                                // Copy TheData formula (if present)
                                if (srcShape.Event.TheData?.Ufe != null)
                                    tgtShape.Event.TheData.Ufe.F = srcShape.Event.TheData.Ufe.F;
                            }
                        }
                    }

                    // Save the merged target diagram to the specified output path
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Event sections merged successfully. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during merging: {ex.Message}");
        }
    }
}