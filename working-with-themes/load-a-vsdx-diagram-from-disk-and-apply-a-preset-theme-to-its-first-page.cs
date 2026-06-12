using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string targetFilePath = "input.vsdx";          // Path to the diagram to modify
        string themeFilePath = "theme.vsdx";           // Path to a diagram that contains the desired preset theme
        if (!System.IO.File.Exists(themeFilePath))
        {
            Console.Error.WriteLine($"File not found: {themeFilePath}");
            return;
        }
        string outputFilePath = "output.vsdx";        // Path where the themed diagram will be saved

        // Load the target diagram (VSDX) from disk
        Diagram targetDiagram = new Diagram(targetFilePath);

        // Load the source diagram that holds the preset theme
        Diagram themeDiagram = new Diagram(themeFilePath);

        // Apply the theme from the source diagram to the target diagram
        targetDiagram.CopyTheme(themeDiagram);

        // Save the modified diagram back to VSDX format
        targetDiagram.Save(outputFilePath, SaveFileFormat.Vsdx);

        // Clean up resources
        targetDiagram.Dispose();
        themeDiagram.Dispose();
    }
}
