using System;
using System.IO;
using Aspose.Diagram;

class ApplyCommonTheme
{
    static void Main()
    {
        try
        {

            // Path to the directory containing Visio files to process
            string inputDirectory = @"C:\VisioFiles\Input";

            // Path to the directory where modified files will be saved
            string outputDirectory = @"C:\VisioFiles\Output";

            // Path to the source diagram that holds the desired theme
            string themeDiagramPath = @"C:\VisioFiles\Theme\theme.vsdx";

            // Ensure output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Load the source diagram that contains the theme to be copied
            using (Diagram themeDiagram = new Diagram(themeDiagramPath))
            {
                // Get all Visio files (VSDX) in the input directory
                string[] visioFiles = Directory.GetFiles(inputDirectory, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string filePath in visioFiles)
                {
                    // Load the target diagram
                    using (Diagram targetDiagram = new Diagram(filePath))
                    {
                        // Copy the theme from the source diagram to the target diagram
                        targetDiagram.CopyTheme(themeDiagram);

                        // Determine output file path (overwrite original or save to separate folder)
                        string fileName = Path.GetFileName(filePath);
                        string outputPath = Path.Combine(outputDirectory, fileName);

                        // Save the modified diagram using the same format (VSDX)
                        targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
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
