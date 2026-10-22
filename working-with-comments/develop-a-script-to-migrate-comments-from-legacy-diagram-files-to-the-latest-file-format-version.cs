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
            Console.Error.WriteLine("Usage: Program <legacyDiagramPath> <outputDiagramPath>");
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

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the legacy diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                Console.WriteLine($"Loaded diagram: {Path.GetFileName(inputPath)}");
                Console.WriteLine($"Diagram version: {diagram.Version}");

                // Iterate through all pages and list their comments.
                foreach (Page page in diagram.Pages)
                {
                    // Page.Name is a plain string, not a cell with .Value.
                    string pageName = page.Name ?? string.Empty; // Fixed: removed .Value
                    Console.WriteLine($"Page ID {page.ID} Name: \"{pageName}\"");

                    // Access comments via the page's PageSheet.Annotations collection.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve comment text.
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Retrieve reviewer ID (may be null).
                        string reviewerId = annotation.ReviewerID?.Value.ToString() ?? "N/A";

                        // Retrieve comment creation date.
                        DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                        Console.WriteLine($"  Comment ID {annotation.MarkerIndex?.Value} by Reviewer {reviewerId} on {commentDate}: {commentText}");
                    }
                }

                // Save the diagram in the latest VSDX format, preserving all comments.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to latest format: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
