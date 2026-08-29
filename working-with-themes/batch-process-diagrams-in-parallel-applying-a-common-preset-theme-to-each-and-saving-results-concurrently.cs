using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the preset theme diagram (source of the theme)
            string themePath = @"C:\Themes\PresetTheme.vsdx";

            // Folder containing diagrams to process
            string inputFolder = @"C:\Diagrams\Input";

            // Folder where processed diagrams will be saved
            string outputFolder = @"C:\Diagrams\Output";

            // Load the theme diagram once (read‑only, can be shared across threads)
            using (var themeDiagram = new Diagram(themePath))
            {
                // Get all diagram files in the input folder (adjust extensions as needed)
                var diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly)
                                            .Where(f => f.EndsWith(".vsdx", StringComparison.OrdinalIgnoreCase) ||
                                                        f.EndsWith(".vsd", StringComparison.OrdinalIgnoreCase) ||
                                                        f.EndsWith(".vdx", StringComparison.OrdinalIgnoreCase))
                                            .ToArray();

                // Process each diagram in parallel
                Parallel.ForEach(diagramFiles, inputPath =>
                {
                    // Determine output file path (preserve file name)
                    string fileName = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(outputFolder, fileName + ".vsdx");

                    // Load the source diagram
                    using (var diagram = new Diagram(inputPath))
                    {
                        // Apply the preset theme from the theme diagram
                        diagram.CopyTheme(themeDiagram);

                        // Save the modified diagram using the same format as the source
                        diagram.Save(outputPath, SaveFileFormat.Vdx);
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
