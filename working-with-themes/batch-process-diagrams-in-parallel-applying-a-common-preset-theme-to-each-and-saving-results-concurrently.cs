using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class BatchDiagramProcessor
{
    static void Main()
    {
        try
        {

            // Path to the preset theme diagram
            string themePath = @"C:\Themes\preset.vsdx";

            // Folder containing diagrams to process
            string inputFolder = @"C:\Diagrams\Input";

            // Folder where themed diagrams will be saved
            string outputFolder = @"C:\Diagrams\Output";

            // Load the theme diagram once (read‑only)
            Diagram themeDiagram = new Diagram(themePath);

            // Get all Visio files to process
            string[] files = Directory.GetFiles(inputFolder, "*.vsdx");

            // Process each diagram in parallel
            Parallel.ForEach(files, inputFile =>
            {
                // Load the source diagram
                using (Diagram doc = new Diagram(inputFile))
                {
                    // Apply the preset theme
                    doc.CopyTheme(themeDiagram);

                    // Build output file name
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(inputFile) + "_themed.vsdx");

                    // Save the themed diagram using the provided Save method
                    doc.Save(outputPath, SaveFileFormat.Vsdx);
                }
            });

            // Clean up the theme diagram
            themeDiagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
