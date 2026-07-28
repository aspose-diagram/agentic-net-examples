using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ApplyThemeExample
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, the theme diagram and the output file
            string sourceDiagramPath = "input.vsdx";
            string themeDiagramPath = "theme.vsdx";
            string outputDiagramPath = "output.vsdx";

            // Load the diagram that will receive the theme
            Diagram targetDiagram = new Diagram(sourceDiagramPath);

            // Load the diagram that contains the desired preset theme
            Diagram themeDiagram = new Diagram(themeDiagramPath);

            // Apply the theme from the source diagram to the target diagram
            targetDiagram.CopyTheme(themeDiagram);

            // Save the modified diagram back to disk (preserving VSDX format)
            targetDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            // Clean up resources
            targetDiagram.Dispose();
            themeDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
