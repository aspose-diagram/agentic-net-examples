using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: source diagram path, target diagram path, output diagram path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramMergeExample <sourcePath> <targetPath> <outputPath>");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];
            string outputPath = args[2];

            // Load source and target diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Iterate through pages (assumes same page count and order)
            int pageCount = Math.Min(sourceDiagram.Pages.Count, targetDiagram.Pages.Count);
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                Page sourcePage = sourceDiagram.Pages[pageIndex];
                Page targetPage = targetDiagram.Pages[pageIndex];

                // Iterate through shapes on the source page
                foreach (Shape sourceShape in sourcePage.Shapes)
                {
                    // Find matching shape on target page by universal name (NameU)
                    Shape targetShape = null;
                    foreach (Shape candidate in targetPage.Shapes)
                    {
                        if (candidate.NameU == sourceShape.NameU)
                        {
                            targetShape = candidate;
                            break;
                        }
                    }

                    // If no matching shape, skip
                    if (targetShape == null)
                        continue;

                    // Copy EventSection cells
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

            // Save the modified target diagram
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
    }