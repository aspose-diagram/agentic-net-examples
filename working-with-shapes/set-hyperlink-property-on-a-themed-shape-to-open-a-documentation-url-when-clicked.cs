using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether a hyperlink was added
            bool hyperlinkAdded = false;

            // Iterate through pages and shapes to find the first non‑deleted shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Add a new hyperlink to this shape
                    Hyperlink link = new Hyperlink();
                    link.Name = "DocumentationLink";
                    link.Address.Value = "https://example.com/documentation";
                    // Open in a new window/tab
                    link.NewWindow.Value = BOOL.True;
                    // Optional tooltip/description
                    link.Description.Value = "Open documentation";

                    // Ensure the Hyperlinks collection is not null before adding
                    if (shape.Hyperlinks != null)
                    {
                        shape.Hyperlinks.Add(link);
                        hyperlinkAdded = true;
                        // Break after adding to the first suitable shape
                        break;
                    }
                }

                if (hyperlinkAdded)
                    break;
            }

            if (!hyperlinkAdded)
            {
                Console.Error.WriteLine("No suitable shape found to add a hyperlink.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with hyperlink to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}