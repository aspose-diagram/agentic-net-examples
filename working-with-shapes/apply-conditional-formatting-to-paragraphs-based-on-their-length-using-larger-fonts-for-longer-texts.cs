using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through all shapes on the current page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains text and character runs
                        if (shape.Text == null || shape.Chars == null || shape.Chars.Count == 0)
                            continue;

                        // Retrieve the plain text of the shape
                        string fullText = shape.Text.Value.Text ?? string.Empty;

                        // Split the text into paragraphs (lines) using newline delimiters
                        string[] paragraphs = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                        // Determine the longest paragraph length
                        int maxParagraphLength = 0;
                        foreach (string para in paragraphs)
                        {
                            if (para.Length > maxParagraphLength)
                                maxParagraphLength = para.Length;
                        }

                        // Decide font size based on the longest paragraph length
                        double fontSizePoints;
                        if (maxParagraphLength < 20)
                            fontSizePoints = 10.0;
                        else if (maxParagraphLength < 50)
                            fontSizePoints = 12.0;
                        else
                            fontSizePoints = 14.0;

                        // Apply the calculated font size to all characters in the shape
                        for (int charIdx = 0; charIdx < shape.Chars.Count; charIdx++)
                        {
                            Aspose.Diagram.Char ch = shape.Chars[charIdx];
                            // Size is in points; Aspose expects points directly
                            ch.Size.Value = fontSizePoints;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}