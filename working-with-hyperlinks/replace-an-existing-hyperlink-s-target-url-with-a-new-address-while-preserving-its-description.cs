using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, new hyperlink URL
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <newUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newUrl = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Iterate over each hyperlink in the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Preserve the existing description and replace the address
                            string originalDescription = link.Description?.Value;
                            link.Address.Value = newUrl;
                            // Re‑assign the original description (if any) to be explicit
                            if (!string.IsNullOrEmpty(originalDescription))
                            {
                                link.Description.Value = originalDescription;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}