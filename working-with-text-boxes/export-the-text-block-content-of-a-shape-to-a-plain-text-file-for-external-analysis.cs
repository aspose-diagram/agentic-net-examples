using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output directory path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists or create it
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block (required by API)
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 1; // Manual page counter (Page does not have PageNum)

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape's text block
                        // shape.Text.Value.Text concatenates all Txt runs
                        string plainText = shape.Text.Value.Text;

                        // Skip shapes with no text
                        if (string.IsNullOrWhiteSpace(plainText))
                            continue;

                        // Build a unique file name using page index and shape ID
                        string fileName = $"Page{pageIndex}_Shape{shape.ID}.txt";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the plain text to the file
                        File.WriteAllText(outputPath, plainText);
                    }

                    pageIndex++;
                }

                Console.WriteLine("Text export completed successfully.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}