using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_dedup.vsdx");

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
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Hyperlinks collection is not null
                    if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                        continue;

                    // Dictionary to track unique hyperlink names (case‑insensitive)
                    Dictionary<string, Hyperlink> uniqueLinks = new Dictionary<string, Hyperlink>(StringComparer.OrdinalIgnoreCase);

                    // Iterate backwards so we can safely remove items
                    for (int i = shape.Hyperlinks.Count - 1; i >= 0; i--)
                    {
                        Hyperlink link = shape.Hyperlinks[i];

                        // If the hyperlink has no name, treat it as unique
                        string linkName = link.Name ?? string.Empty;

                        if (uniqueLinks.ContainsKey(linkName))
                        {
                            // Duplicate found – remove this hyperlink
                            shape.Hyperlinks.RemoveAt(i);
                        }
                        else
                        {
                            // First occurrence – keep it
                            uniqueLinks[linkName] = link;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Duplicate hyperlinks consolidated. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}