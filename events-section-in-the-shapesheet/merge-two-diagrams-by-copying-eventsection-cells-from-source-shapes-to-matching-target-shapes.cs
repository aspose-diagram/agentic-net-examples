using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: source diagram path, target diagram path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramMergeExample <source.vsdx> <target.vsdx> <output.vsdx>");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];
            string outputPath = args[2];

            try
            {
                // Load source and target diagrams
                using (Diagram sourceDiagram = new Diagram(sourcePath))
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Iterate through pages (assumes same page count and order)
                    int pageCount = Math.Min(sourceDiagram.Pages.Count, targetDiagram.Pages.Count);
                    for (int i = 0; i < pageCount; i++)
                    {
                        Page sourcePage = sourceDiagram.Pages[i];
                        Page targetPage = targetDiagram.Pages[i];

                        // Build a lookup of target shapes by universal name for quick matching
                        var targetShapeLookup = new System.Collections.Generic.Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                        foreach (Shape tgtShape in targetPage.Shapes)
                        {
                            if (!string.IsNullOrEmpty(tgtShape.NameU))
                            {
                                targetShapeLookup[tgtShape.NameU] = tgtShape;
                            }
                        }

                        // Iterate source shapes and copy event cells to matching target shapes
                        foreach (Shape srcShape in sourcePage.Shapes)
                        {
                            if (string.IsNullOrEmpty(srcShape.NameU))
                                continue; // Skip shapes without a universal name

                            if (targetShapeLookup.TryGetValue(srcShape.NameU, out Shape matchingTargetShape))
                            {
                                CopyEventCells(srcShape, matchingTargetShape);
                            }
                        }
                    }

                    // Save the merged diagram
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Merged diagram saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during merging: {ex.Message}");
                throw;
            }
        }

        // Copies all supported event formulas from source shape to target shape
        private static void CopyEventCells(Shape sourceShape, Shape targetShape)
        {
            // EventXFMod
            targetShape.Event.EventXFMod.Ufe.F = sourceShape.Event.EventXFMod.Ufe.F;
            // EventDblClick
            targetShape.Event.EventDblClick.Ufe.F = sourceShape.Event.EventDblClick.Ufe.F;
            // EventDrop
            targetShape.Event.EventDrop.Ufe.F = sourceShape.Event.EventDrop.Ufe.F;
            // EventMultiDrop
            targetShape.Event.EventMultiDrop.Ufe.F = sourceShape.Event.EventMultiDrop.Ufe.F;
            // TheText
            targetShape.Event.TheText.Ufe.F = sourceShape.Event.TheText.Ufe.F;
            // TheData
            targetShape.Event.TheData.Ufe.F = sourceShape.Event.TheData.Ufe.F;
        }
    }