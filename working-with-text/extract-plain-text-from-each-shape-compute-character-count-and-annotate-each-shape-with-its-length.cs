using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare output file path (append "_annotated" before extension)
        string outputDirectory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_annotated.vsdx";
        string outputPath = Path.Combine(outputDirectory, outputFileName);

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape
                    string plainText = shape.Text.Value.ToString();

                    // Compute character count
                    int charCount = plainText.Length;

                    // Create annotated text (original text followed by length)
                    string annotatedText = $"{plainText} ({charCount})";

                    // Replace the shape's text with the annotated version
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(annotatedText));
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Annotated diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}