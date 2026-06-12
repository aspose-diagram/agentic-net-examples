using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the diagram that contains the desired theme
            string themeDiagramPath = @"C:\Diagrams\ThemeTemplate.vdx";

            // List of diagram files to process
            List<string> sourceFiles = new List<string>
            {
                @"C:\Diagrams\Diagram1.vdx",
                @"C:\Diagrams\Diagram2.vdx",
                @"C:\Diagrams\Diagram3.vdx"
            };

            // Folder where the themed diagrams will be saved
            string outputFolder = @"C:\Diagrams\ThemedOutput";
            Directory.CreateDirectory(outputFolder);

            // Load the source diagram that holds the theme (once)
            using (Diagram themeDiagram = new Diagram(themeDiagramPath))
            {
                foreach (string sourcePath in sourceFiles)
                {
                    // Load each target diagram
                    using (Diagram targetDiagram = new Diagram(sourcePath))
                    {
                        // Apply the theme from the source diagram to the target diagram
                        targetDiagram.CopyTheme(themeDiagram);

                        // Build the output file name
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
                        string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_Themed.vdx");

                        // Save the themed diagram (using the provided Save method)
                        targetDiagram.Save(outputPath, SaveFileFormat.Vdx);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
