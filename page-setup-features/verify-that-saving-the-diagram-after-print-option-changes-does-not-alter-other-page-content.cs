using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PrintOptionSaveVerification
{
    static void Main()
    {
        try
        {

            // Load the original diagram from a file
            string inputPath = "input.vsdx";
            Diagram originalDiagram = new Diagram(inputPath);

            // Record the number of shapes on each page of the original diagram
            int[] originalShapeCounts = new int[originalDiagram.Pages.Count];
            for (int i = 0; i < originalDiagram.Pages.Count; i++)
            {
                originalShapeCounts[i] = originalDiagram.Pages[i].Shapes.Count;
            }

            // Create PrintSaveOptions and modify a print-related setting
            PrintSaveOptions printOptions = new PrintSaveOptions
            {
                // Example: print only foreground pages
                SaveForegroundPagesOnly = true,
                // Ensure all pages are considered (optional)
                PageCount = int.MaxValue
            };

            // Save the diagram using the modified print options
            string outputPath = "output.vsdx";
            originalDiagram.Save(outputPath, printOptions);

            // Reload the saved diagram
            Diagram savedDiagram = new Diagram(outputPath);

            // Verify that page content (shape counts) remains unchanged
            bool contentUnchanged = true;
            if (savedDiagram.Pages.Count != originalDiagram.Pages.Count)
            {
                contentUnchanged = false;
            }
            else
            {
                for (int i = 0; i < savedDiagram.Pages.Count; i++)
                {
                    int savedShapeCount = savedDiagram.Pages[i].Shapes.Count;
                    if (savedShapeCount != originalShapeCounts[i])
                    {
                        contentUnchanged = false;
                        break;
                    }
                }
            }

            // Output verification result
            Console.WriteLine(contentUnchanged
                ? "Page content unchanged after saving with print options."
                : "Page content altered after saving with print options.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
