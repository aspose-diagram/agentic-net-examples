using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            int modifiedCount = 0;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Access annotations (comments) via the page's PageSheet.
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the current comment text.
                    string commentText = annotation.Comment.Value ?? string.Empty;

                    // Define the criteria: comment contains the word "review" (case‑insensitive).
                    if (commentText.IndexOf("review", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Apply a custom tag if it is not already present.
                        const string tagPrefix = "[CustomTag] ";
                        if (!commentText.StartsWith(tagPrefix, StringComparison.Ordinal))
                        {
                            annotation.Comment.Value = tagPrefix + commentText;
                            modifiedCount++;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Processing completed. Modified {modifiedCount} comment(s). Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}