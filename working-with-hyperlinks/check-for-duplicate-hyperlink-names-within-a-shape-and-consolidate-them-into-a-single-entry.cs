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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or auto-generated)
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

                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks == null || shape.Hyperlinks.Count <= 1)
                        continue;

                    // Dictionary to track first occurrence of each hyperlink name
                    var seenNames = new System.Collections.Generic.Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    // Iterate backwards to safely remove duplicates
                    for (int i = shape.Hyperlinks.Count - 1; i >= 0; i--)
                    {
                        Hyperlink link = shape.Hyperlinks[i];
                        string name = link.Name ?? string.Empty;

                        if (seenNames.ContainsKey(name))
                        {
                            // Duplicate found – remove this hyperlink
                            shape.Hyperlinks.RemoveAt(i);
                        }
                        else
                        {
                            // First occurrence – remember its index
                            seenNames[name] = i;
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
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}