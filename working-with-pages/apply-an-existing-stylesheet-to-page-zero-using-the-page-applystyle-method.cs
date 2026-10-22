using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the document contains at least one stylesheet.
                if (diagram.StyleSheets.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any stylesheets to apply.");
                    return;
                }

                // Retrieve the first stylesheet and its ID.
                StyleSheet style = diagram.StyleSheets[0];
                // Page.ApplyStyle expects int parameters, so cast the long ID.
                int styleId = (int)style.ID;

                // Access the first page (page zero).
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                Page pageZero = diagram.Pages[0];

                // Apply the stylesheet to the page.
                // ApplyStyle takes textStyleId, lineStyleId, fillStyleId.
                pageZero.ApplyStyle(styleId, styleId, styleId);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Stylesheet applied and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}