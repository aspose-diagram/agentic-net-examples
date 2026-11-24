using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that already have at least one hyperlink
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            continue;

                        // Determine a default URL based on the shape's name (NameU)
                        string shapeName = shape.NameU;
                        if (string.IsNullOrEmpty(shapeName))
                        {
                            // Fallback to a generic name if NameU is not set
                            shapeName = "Shape";
                        }

                        // Construct the default URL (example pattern)
                        string defaultUrl = $"https://default.example.com/{shapeName}";

                        // Create a new Hyperlink instance
                        Hyperlink link = new Hyperlink();
                        link.Name = "DefaultLink";
                        link.Address.Value = defaultUrl;
                        // Open in the same window (new window = false)
                        link.NewWindow.Value = BOOL.False;

                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}