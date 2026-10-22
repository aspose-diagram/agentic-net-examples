using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, new hyperlink URL
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <newHyperlinkUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newUrl = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Loop over each hyperlink in the shape
                        for (int i = 0; i < shape.Hyperlinks.Count; i++)
                        {
                            // Retrieve the hyperlink instance
                            Hyperlink link = shape.Hyperlinks[i];

                            // Preserve the existing description (no change needed)
                            // Update the hyperlink's target address to the new URL
                            if (link != null && link.Address != null)
                            {
                                link.Address.Value = newUrl;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}