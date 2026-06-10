using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Configuration: list of source diagram file paths and the target output path.
                // In a real scenario these could be read from a config file or command‑line arguments.
                string[] sourceFiles = new string[]
                {
                    "SourceDiagram1.vsdx",
                    "SourceDiagram2.vsdx",
                    "SourceDiagram3.vsdx"
                };

                string targetFilePath = "MergedDiagram.vsdx";

                // Create an empty target diagram.
                Diagram targetDiagram = new Diagram();

                // Iterate over each source diagram, load it, and combine its pages into the target.
                foreach (string sourcePath in sourceFiles)
                {
                    // Load the source diagram.
                    Diagram sourceDiagram = new Diagram(sourcePath);

                    // Merge the source diagram into the target diagram.
                    targetDiagram.Combine(sourceDiagram);
                }

                // After combining, the target diagram may contain an initial empty page.
                // Remove it if it has no shapes.
                if (targetDiagram.Pages.Count > 0)
                {
                    Page firstPage = targetDiagram.Pages[0];
                    if (firstPage.Shapes.Count == 0)
                    {
                        targetDiagram.Pages.Remove(firstPage);
                    }
                }

                // Save the merged diagram to the desired format (VSDX in this example).
                targetDiagram.Save(targetFilePath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }