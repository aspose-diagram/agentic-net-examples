using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class BatchThemeProcessor
{
    static void Main()
    {
        try
        {

            // Path to the preset theme diagram (must be a Visio file)
            string themePath = @"C:\Themes\presetTheme.vsdx";

            // Directory containing source diagrams
            string inputDirectory = @"C:\Diagrams\Input";

            // Directory where themed diagrams will be saved
            string outputDirectory = @"C:\Diagrams\Output";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Load the theme diagram once (shared across all tasks)
            using (Diagram themeDiagram = new Diagram(themePath))
            {
                // Get all Visio files to process
                string[] inputFiles = Directory.GetFiles(inputDirectory, "*.vsdx");

                // Process each file in parallel
                Parallel.ForEach(inputFiles, inputFile =>
                {
                    // Determine output file path
                    string fileName = Path.GetFileNameWithoutExtension(inputFile);
                    string outputPath = Path.Combine(outputDirectory, $"{fileName}_themed.vsdx");

                    // Load the source diagram
                    using (Diagram diagram = new Diagram(inputFile))
                    {
                        // Apply the preset theme
                        diagram.CopyTheme(themeDiagram);

                        // Save the themed diagram using the standard Save method
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                });
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
