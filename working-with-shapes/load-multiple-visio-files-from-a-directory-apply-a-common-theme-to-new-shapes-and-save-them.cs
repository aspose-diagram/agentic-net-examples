using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchThemeApplier
{
    // Path to the directory containing Visio files to process
    private const string InputDirectory = @"C:\VisioFiles\Input";

    // Path to the directory where processed files will be saved
    private const string OutputDirectory = @"C:\VisioFiles\Output";

    // Path to a Visio file that contains the desired theme
    private const string ThemeFilePath = @"C:\VisioFiles\Theme\theme.vsdx";

    static void Main()
    {
        try
        {

            // Ensure output directory exists
            Directory.CreateDirectory(OutputDirectory);

            // Load the diagram that holds the theme to be copied
            using (Diagram themeDiagram = new Diagram(ThemeFilePath))
            {
                // Get all Visio files (VSDX) in the input directory
                string[] visioFiles = Directory.GetFiles(InputDirectory, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string filePath in visioFiles)
                {
                    // Load the current Visio file
                    using (Diagram doc = new Diagram(filePath))
                    {
                        // Apply the theme from the source diagram
                        doc.CopyTheme(themeDiagram);

                        // Determine output file path (overwrite in output folder)
                        string fileName = Path.GetFileName(filePath);
                        string outputPath = Path.Combine(OutputDirectory, fileName);

                        // Save the modified diagram using the same format (VSDX)
                        doc.Save(outputPath, SaveFileFormat.Vsdx);
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
