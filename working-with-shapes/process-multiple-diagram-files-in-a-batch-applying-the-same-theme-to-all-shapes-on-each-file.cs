using System;
using System.IO;
using Aspose.Diagram;

class BatchThemeApplier
{
    static void Main()
    {
        try
        {

            // Path to the diagram that contains the desired theme (source diagram)
            string themeDiagramPath = @"C:\Themes\ThemeTemplate.vsdx";

            // Folder containing diagrams to which the theme will be applied
            string inputFolder = @"C:\Diagrams\Input";

            // Folder where the themed diagrams will be saved
            string outputFolder = @"C:\Diagrams\Output";

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Load the source diagram that holds the theme
            Diagram sourceDiagram = new Diagram(themeDiagramPath);

            // Process each diagram file in the input folder
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                // Load the target diagram
                Diagram targetDiagram = new Diagram(filePath);

                // Copy the theme from the source diagram to the target diagram
                targetDiagram.CopyTheme(sourceDiagram);

                // Build the output file path (overwrite name in output folder)
                string fileName = Path.GetFileName(filePath);
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the themed diagram (preserving original format)
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Dispose diagrams to release resources
            sourceDiagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
