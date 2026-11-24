using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Set the default fallback font to Arial before loading the diagram.
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a stylesheet that defines the desired font family.
                // ------------------------------------------------------------
                StyleSheet fontStyle = new StyleSheet();

                // Assign a unique ID (next available index).
                fontStyle.ID = diagram.StyleSheets.Count + 1;

                // Configure character style: set FontName to Arial.
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0; // character index
                ch.FontName.Value = "Arial";
                fontStyle.Chars.Add(ch);

                // Add the new stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(fontStyle);

                // ------------------------------------------------------------
                // Apply the stylesheet to every page in the diagram.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Apply the same stylesheet for characters, lines, and fills.
                    page.ApplyStyle(fontStyle.ID, fontStyle.ID, fontStyle.ID);
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}