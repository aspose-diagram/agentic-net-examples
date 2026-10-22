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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new stylesheet.
                StyleSheet styleSheet = new StyleSheet();

                // Assign a unique ID for the stylesheet.
                styleSheet.ID = diagram.StyleSheets.Count + 1;

                // Optional: give the stylesheet a name for easier identification.
                styleSheet.Name = "LightGrayFill";

                // Set the fill pattern to solid (value 1) and the foreground color to light gray.
                // Light gray hex code: #D3D3D3
                styleSheet.Fill.FillPattern.Value = 1;
                styleSheet.Fill.FillForegnd.Value = "#D3D3D3";

                // Add the stylesheet to the diagram's stylesheet collection.
                diagram.StyleSheets.Add(styleSheet);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Stylesheet added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}