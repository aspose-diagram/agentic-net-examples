using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ApplyCommonTheme
{
    static void Main()
    {
        try
        {

            // Path to the directory containing Visio files to process
            string inputDirectory = @"C:\VisioFiles";

            // Path to the Visio file that holds the desired theme
            string themeFilePath = @"C:\Theme\theme.vsdx";

            // Load the theme diagram once
            using (Diagram themeDiagram = new Diagram(themeFilePath))
            {
                // Get all Visio files (VSDX) in the input directory
                string[] visioFiles = Directory.GetFiles(inputDirectory, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string filePath in visioFiles)
                {
                    // Load the current Visio file
                    using (Diagram targetDiagram = new Diagram(filePath))
                    {
                        // Apply the common theme from the theme diagram
                        targetDiagram.CopyTheme(themeDiagram);

                        // Save the modified diagram, overwriting the original file
                        targetDiagram.Save(filePath, SaveFileFormat.Vsdx);
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
