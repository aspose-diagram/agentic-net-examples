using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramMerger
{
    static void Main()
    {
        try
        {

            // Paths to the source VSDX files and the output file
            string firstDiagramPath = @"C:\Diagrams\FirstDiagram.vsdx";
            string secondDiagramPath = @"C:\Diagrams\SecondDiagram.vsdx";
            string mergedDiagramPath = @"C:\Diagrams\MergedDiagram.vsdx";

            // Load the first diagram (this will be the target diagram)
            using (Diagram targetDiagram = new Diagram(firstDiagramPath))
            {
                // Load the second diagram that will be merged into the target
                using (Diagram sourceDiagram = new Diagram(secondDiagramPath))
                {
                    // Combine the source diagram into the target diagram.
                    // Aspose.Diagram.Combine automatically handles duplicate shape IDs.
                    targetDiagram.Combine(sourceDiagram);
                }

                // Optional: configure save options (e.g., auto‑fit page to content)
                DiagramSaveOptions saveOptions = new DiagramSaveOptions
                {
                    AutoFitPageToDrawingContent = true,
                    SaveFormat = SaveFileFormat.Vdx // VDX is the XML format for Visio diagrams
                };

                // Save the merged diagram to the specified file
                targetDiagram.Save(mergedDiagramPath, saveOptions);
            }

            Console.WriteLine("Diagrams merged successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
