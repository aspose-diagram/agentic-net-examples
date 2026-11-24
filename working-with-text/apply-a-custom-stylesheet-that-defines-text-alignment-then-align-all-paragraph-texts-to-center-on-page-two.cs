using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_styled.vsdx");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom stylesheet that will be used for page formatting
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();
                // Assign a unique ID (Visio IDs start at 1)
                customStyle.ID = diagram.StyleSheets.Count + 1;
                // Give the stylesheet a recognizable name
                customStyle.Name = "CenterAlignStyle";

                // NOTE: Alignment is a paragraph property, not directly stored in a stylesheet.
                // The stylesheet can still be applied to the page for consistency.
                // Add the stylesheet to the document's collection
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the custom stylesheet to page two (index 1, because pages are zero‑based)
                // ------------------------------------------------------------
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                Page pageTwo = diagram.Pages[1];
                // Apply the style to the page (style ID for text, line, and fill)
                pageTwo.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

                // ------------------------------------------------------------
                // Align all paragraph texts to center on page two
                // ------------------------------------------------------------
                foreach (Shape shape in pageTwo.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through all paragraphs of the shape
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        // Set horizontal alignment to center
                        shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}