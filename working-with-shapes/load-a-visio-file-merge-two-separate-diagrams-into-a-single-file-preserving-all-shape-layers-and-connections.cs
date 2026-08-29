using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioMerger
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio files
            string firstFile = "FirstDiagram.vsdx";
            string secondFile = "SecondDiagram.vsdx";

            // Load the first diagram (this will be the target diagram)
            using (Diagram targetDiagram = new Diagram(firstFile))
            {
                // Load the second diagram that will be merged into the target
                using (Diagram sourceDiagram = new Diagram(secondFile))
                {
                    // Combine the source diagram into the target diagram.
                    // This preserves layers, shapes, and connections.
                    targetDiagram.Combine(sourceDiagram);
                }

                // Save the merged diagram to a new file
                string outputFile = "MergedDiagram.vsdx";
                targetDiagram.Save(outputFile, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
