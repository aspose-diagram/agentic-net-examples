using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and custom font folder path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <fontFolderPath>");
            return;
        }

        string diagramPath = args[0];
        string fontFolderPath = args[1];

        // Guard: verify diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: verify font folder exists
        if (!Directory.Exists(fontFolderPath))
        {
            Console.Error.WriteLine($"Font folder not found: {fontFolderPath}");
            return;
        }

        try
        {
            // Configure Aspose.Diagram to search the custom font folder (recursive)
            // The second parameter expects a plain bool, not BOOL
            FontConfigs.SetFontFolder(fontFolderPath, true);

            // Optionally set a default fallback font name
            FontConfigs.DefaultFontName = "Arial";

            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // List fonts used in the diagram
                Console.WriteLine("Fonts referenced by the diagram:");
                foreach (Font font in diagram.Fonts)
                {
                    Console.WriteLine($"- {font.Name}");
                }

                // List installed system fonts (using Aspose.Drawing.Text)
                Console.WriteLine("\nInstalled system fonts:");
                InstalledFontCollection installedFonts = new InstalledFontCollection();
                foreach (var family in installedFonts.Families)
                {
                    // family.Name provides the font family name
                    Console.WriteLine($"- {family.Name}");
                }

                // Save the diagram to PDF to verify rendering with the custom fonts
                string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? string.Empty, "output.pdf");
                diagram.Save(outputPath, SaveFileFormat.Pdf);
                Console.WriteLine($"\nDiagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}