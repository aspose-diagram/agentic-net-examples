using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "merged_output.vsdx";

                // Load the source and target diagrams
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Iterate through pages (assumes same page order in both diagrams)
                for (int pageIndex = 0; pageIndex < sourceDiagram.Pages.Count; pageIndex++)
                {
                    Page sourcePage = sourceDiagram.Pages[pageIndex];
                    // Try to get the corresponding page in the target diagram
                    Page targetPage = null;
                    if (pageIndex < targetDiagram.Pages.Count)
                    {
                        targetPage = targetDiagram.Pages[pageIndex];
                    }
                    else
                    {
                        // If the target diagram has fewer pages, skip remaining source pages
                        Console.WriteLine($"Target diagram does not have page index {pageIndex}, skipping.");
                        continue;
                    }

                    // Build a lookup of target shapes by their universal name for fast matching
                    var targetShapeLookup = new System.Collections.Generic.Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                    foreach (Shape tgtShape in targetPage.Shapes)
                    {
                        if (!string.IsNullOrEmpty(tgtShape.NameU))
                        {
                            targetShapeLookup[tgtShape.NameU] = tgtShape;
                        }
                    }

                    // Process each shape in the source page
                    foreach (Shape srcShape in sourcePage.Shapes)
                    {
                        if (string.IsNullOrEmpty(srcShape.NameU))
                            continue; // Skip shapes without a universal name

                        if (!targetShapeLookup.TryGetValue(srcShape.NameU, out Shape matchingTargetShape))
                        {
                            // No matching shape found in target page
                            Console.WriteLine($"No matching target shape for source shape '{srcShape.NameU}' on page {pageIndex}.");
                            continue;
                        }

                        // Copy event cells from source to target
                        CopyEventSection(srcShape, matchingTargetShape);
                    }
                }

                // Save the modified target diagram
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Merged diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies all supported event cells from the source shape to the target shape.
        /// </summary>
        /// <param name="source">Shape from the source diagram.</param>
        /// <param name="target">Corresponding shape in the target diagram.</param>
        private static void CopyEventSection(Shape source, Shape target)
        {
            // Event on double‑click
            if (source.Event.EventDblClick != null && source.Event.EventDblClick.Ufe != null)
                target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;

            // Event on drop
            if (source.Event.EventDrop != null && source.Event.EventDrop.Ufe != null)
                target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;

            // Event on multiple drop
            if (source.Event.EventMultiDrop != null && source.Event.EventMultiDrop.Ufe != null)
                target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;

            // Event on shape modification (XFMod)
            if (source.Event.EventXFMod != null && source.Event.EventXFMod.Ufe != null)
                target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;

            // TheText event (used for custom text handling)
            if (source.Event.TheText != null && source.Event.TheText.Ufe != null)
                target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;

            // TheData event (used for custom data handling)
            if (source.Event.TheData != null && source.Event.TheData.Ufe != null)
                target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
        }
    }