using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : GetCompactOutputPath(inputPath);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text collection
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Iterate through each text element within the shape
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Process only Txt objects (actual text runs)
                        if (fmt is Txt txt)
                        {
                            // Replace consecutive double spaces with a single space
                            // Repeat until no double spaces remain
                            while (txt.Text.Contains("  "))
                            {
                                txt.Text = txt.Text.Replace("  ", " ");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper to generate an output file name with "_compact" suffix
    private static string GetCompactOutputPath(string inputPath)
    {
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string extension = Path.GetExtension(inputPath);
        return Path.Combine(directory, $"{fileNameWithoutExt}_compact{extension}");
    }
}