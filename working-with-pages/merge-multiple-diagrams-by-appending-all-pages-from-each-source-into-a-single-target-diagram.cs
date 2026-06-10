using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramMerger
{
    static void Main()
    {
        try
        {

            // Paths of the diagrams to be merged
            var sourceFiles = new List<string>
            {
                "diagram1.vsdx",
                "diagram2.vsdx",
                "diagram3.vsdx"
            };

            // Create an empty target diagram
            using (var targetDiagram = new Diagram())
            {
                // Load each source diagram and combine its pages into the target
                foreach (var filePath in sourceFiles)
                {
                    using (var sourceDiagram = new Diagram(filePath))
                    {
                        targetDiagram.Combine(sourceDiagram);
                    }
                }

                // Save the merged diagram to a new file
                targetDiagram.Save("mergedDiagram.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
