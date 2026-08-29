using System;
using System.IO;
using Aspose.Diagram;

class BatchThemeApplier
{
    static void Main()
    {
        try
        {

            // Path to the diagram that contains the desired theme.
            string themeDiagramPath = @"C:\Themes\ThemeDiagram.vsdx";

            // Folder containing the diagrams to which the theme will be applied.
            string inputFolder = @"C:\Diagrams\Input";

            // Folder where the themed diagrams will be saved (can be the same as inputFolder to overwrite).
            string outputFolder = @"C:\Diagrams\Output";

            // Ensure the output folder exists.
            Directory.CreateDirectory(outputFolder);

            // Load the source diagram that holds the theme once.
            using (Diagram sourceThemeDiagram = new Diagram(themeDiagramPath))
            {
                // Process each diagram file in the input folder.
                foreach (string inputFilePath in Directory.GetFiles(inputFolder, "*.vsdx"))
                {
                    // Load the target diagram.
                    using (Diagram targetDiagram = new Diagram(inputFilePath))
                    {
                        // Copy the theme from the source diagram to the target diagram.
                        targetDiagram.CopyTheme(sourceThemeDiagram);

                        // Determine the output file path.
                        string fileName = Path.GetFileName(inputFilePath);
                        string outputFilePath = Path.Combine(outputFolder, fileName);

                        // Save the themed diagram, preserving the original format.
                        targetDiagram.Save(outputFilePath, SaveFileFormat.Vsdx);
                    }
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
