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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a single stylesheet that defines a solid white fill.
                StyleSheet whiteStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet.
                whiteStyle.ID = diagram.StyleSheets.Count + 1;

                // Set fill pattern to solid (value 1) and foreground color to white.
                whiteStyle.Fill.FillPattern.Value = 1;
                whiteStyle.Fill.FillForegnd.Value = "#FFFFFF";

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(whiteStyle);

                // Loop through all pages and apply the stylesheet as background.
                foreach (Page page in diagram.Pages)
                {
                    // Apply the same style ID for fill, line, and character styles.
                    page.ApplyStyle(whiteStyle.ID, whiteStyle.ID, whiteStyle.ID);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}