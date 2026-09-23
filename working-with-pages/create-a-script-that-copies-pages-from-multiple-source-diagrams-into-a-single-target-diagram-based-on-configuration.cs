using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Configuration: list of source diagram file paths
            string[] sourceFiles = new string[]
            {
                "SourceDiagram1.vsdx",
                "SourceDiagram2.vsdx",
                "SourceDiagram3.vsdx"
            };

            // Target diagram file path
            string targetFilePath = "MergedDiagram.vsdx";

            // Create an empty target diagram
            Diagram targetDiagram = new Diagram();

            // Iterate over each source file, load it, and merge its pages into the target diagram
            foreach (string srcPath in sourceFiles)
            {
                // Load source diagram
                Diagram sourceDiagram = new Diagram(srcPath);

                // Merge all pages and masters from the source into the target
                targetDiagram.Combine(sourceDiagram);
            }

            // Save the merged diagram to the specified target file
            targetDiagram.Save(targetFilePath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Merged diagram saved to: {targetFilePath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
