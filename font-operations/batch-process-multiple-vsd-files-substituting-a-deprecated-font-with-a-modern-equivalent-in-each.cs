using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    // Define the deprecated font and its modern replacement
    private const string DeprecatedFont = "Arial Unicode MS";
    private const string ModernFont = "Arial";

    static void Main(string[] args)
    {
        // Determine folder to process: argument or current directory
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Retrieve all Visio files with common extensions
        string[] visioFiles = Directory.GetFiles(folderPath, "*.vsd*");

        // Process each file individually
        foreach (string filePath in visioFiles)
        {
            try
            {
                ProcessDiagram(filePath);
                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that escape inner handling
                Console.Error.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }

    private static void ProcessDiagram(string filePath)
    {
        // Guard: ensure the file actually exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Verify that the modern replacement font is installed on the system
        InstalledFontCollection fontCollection = new InstalledFontCollection();
        bool modernFontAvailable = false;
        foreach (var family in fontCollection.Families)
        {
            if (family.Name.Equals(ModernFont, StringComparison.OrdinalIgnoreCase))
            {
                modernFontAvailable = true;
                break;
            }
        }

        if (!modernFontAvailable)
        {
            throw new Exception($"Modern font \"{ModernFont}\" is not installed on this machine.");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages and shapes to replace deprecated font
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Replace font in character formatting (per‑character)
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        if (ch.FontName.Value != null &&
                            ch.FontName.Value.Equals(DeprecatedFont, StringComparison.OrdinalIgnoreCase))
                        {
                            ch.FontName.Value = ModernFont;
                        }
                    }

                    // No direct Txt.Font property exists; font changes are handled via Char objects above
                }
            }

            // Set the default fallback font for the diagram (used when a font is missing)
            FontConfigs.DefaultFontName = ModernFont;

            // Save the diagram, overwriting the original file in VSDX format
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Capture any Aspose‑related errors and report them
            Console.Error.WriteLine($"Aspose error processing {Path.GetFileName(filePath)}: {ex.Message}");
            throw; // Re‑throw to allow outer handler to report the file as failed
        }
    }
}