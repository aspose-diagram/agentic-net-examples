using System;
using System.IO;
using Aspose.Diagram;

class ApplyUniformTheme
{
    static void Main()
    {
        try
        {

            // Folder containing the VSDX files to process
            string diagramsFolder = @"C:\Diagrams";

            // Path to a template diagram that has the desired preset theme
            string themeTemplatePath = @"C:\Theme\themeTemplate.vsdx";

            // Load the template diagram (source of the theme)
            using (Diagram themeDiagram = new Diagram(themeTemplatePath))
            {
                // Iterate over all VSDX files in the specified folder
                foreach (string diagramPath in Directory.GetFiles(diagramsFolder, "*.vsdx"))
                {
                    // Load the current diagram
                    using (Diagram diagram = new Diagram(diagramPath))
                    {
                        // Copy the theme from the template diagram to the current diagram
                        diagram.CopyTheme(themeDiagram);

                        // Save the diagram back, overwriting the original file
                        diagram.Save(diagramPath, SaveFileFormat.Vsdx);
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
