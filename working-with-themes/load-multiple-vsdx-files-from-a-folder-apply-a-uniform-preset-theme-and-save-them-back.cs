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
            string sourceFolder = @"C:\Diagrams";

            // Path to a template diagram that already has the desired theme applied
            string themeTemplatePath = @"C:\Template\themeTemplate.vsdx";

            // Load the template diagram once
            Diagram templateDiagram = new Diagram(themeTemplatePath);

            // Process each VSDX file in the folder
            foreach (string filePath in Directory.GetFiles(sourceFolder, "*.vsdx"))
            {
                // Load the current diagram
                Diagram diagram = new Diagram(filePath);

                // Apply the preset theme from the template diagram
                diagram.CopyTheme(templateDiagram);

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            // Clean up the template diagram
            templateDiagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
