using System;
using System.IO;
using Aspose.Diagram;

class ApplyUniformTheme
{
    static void Main(string[] args)
    {
        // Expect two arguments: the folder containing VSDX files and a template file that has the desired theme.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ApplyUniformTheme <folderPath> <themeTemplatePath>");
            return;
        }

        string folderPath = args[0];
        string themeTemplatePath = args[1];

        // Load the template diagram that already contains the preset theme.
        Diagram themeDiagram = new Diagram(themeTemplatePath);

        // Retrieve all VSDX files in the specified folder (non‑recursive).
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string filePath in diagramFiles)
        {
            // Load each diagram file.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Copy the theme from the template diagram to the current diagram.
                diagram.CopyTheme(themeDiagram);

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }

        // Clean up the template diagram.
        themeDiagram.Dispose();
    }
}
