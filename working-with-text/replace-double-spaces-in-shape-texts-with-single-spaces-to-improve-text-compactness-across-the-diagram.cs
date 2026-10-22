using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Append suffix to input file name if output not specified
            string dir = Path.GetDirectoryName(inputPath) ?? "";
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{fileNameWithoutExt}_cleaned.vsdx");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text object
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Retrieve the plain text of the shape
                    string originalText = shape.Text.Value.Text ?? string.Empty;

                    // Skip processing if the text is empty or does not contain double spaces
                    if (string.IsNullOrWhiteSpace(originalText) || !originalText.Contains("  "))
                        continue;

                    // Replace consecutive double spaces with a single space iteratively
                    string cleanedText = originalText;
                    while (cleanedText.Contains("  "))
                    {
                        cleanedText = cleanedText.Replace("  ", " ");
                    }

                    // Update the shape's text only if it changed
                    if (!cleanedText.Equals(originalText))
                    {
                        // Clear existing text fragments
                        shape.Text.Value.Clear();

                        // Add the cleaned text as a new Txt object
                        shape.Text.Value.Add(new Txt(cleanedText));
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}