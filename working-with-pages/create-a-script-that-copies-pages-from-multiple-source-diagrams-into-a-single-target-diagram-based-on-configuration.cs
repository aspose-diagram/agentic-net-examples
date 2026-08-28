using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Configuration: list of source diagram file paths to be merged
            List<string> sourceFiles = new List<string>
            {
                "Source1.vsdx",
                "Source2.vsdx",
                "Source3.vsdx"
            };

            // Create the target diagram (empty document)
            Diagram targetDiagram = new Diagram();

            // Iterate over each source diagram, load it, and combine its pages into the target
            foreach (string filePath in sourceFiles)
            {
                // Load source diagram from file
                Diagram sourceDiagram = new Diagram(filePath);

                // Combine the source diagram into the target diagram (adds all pages, masters, etc.)
                targetDiagram.Combine(sourceDiagram);

                // Release resources of the source diagram
                sourceDiagram.Dispose();
            }

            // Save the combined diagram to a new file
            string outputPath = "CombinedDiagram.vsdx";
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up the target diagram
            targetDiagram.Dispose();

            Console.WriteLine("Diagrams merged successfully into: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
