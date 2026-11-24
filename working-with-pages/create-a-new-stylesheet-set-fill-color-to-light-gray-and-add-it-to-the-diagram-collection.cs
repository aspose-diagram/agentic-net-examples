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
            // Load the existing Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new stylesheet.
                StyleSheet styleSheet = new StyleSheet();

                // Assign a unique ID (next available ID in the collection).
                styleSheet.ID = diagram.StyleSheets.Count + 1;

                // Set the fill pattern to solid (value 1) – required for visible fill.
                styleSheet.Fill.FillPattern.Value = 1;

                // Set the foreground fill color to light gray.
                // Hex code for light gray: #D3D3D3
                styleSheet.Fill.FillForegnd.Value = "#D3D3D3";

                // Add the stylesheet to the diagram's stylesheet collection.
                diagram.StyleSheets.Add(styleSheet);

                // Optionally, you could apply the stylesheet to a page here.
                // For example, apply to the first page:
                // if (diagram.Pages.Count > 0)
                // {
                //     diagram.Pages[0].ApplyStyle(styleSheet.ID, styleSheet.ID, styleSheet.ID);
                // }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}