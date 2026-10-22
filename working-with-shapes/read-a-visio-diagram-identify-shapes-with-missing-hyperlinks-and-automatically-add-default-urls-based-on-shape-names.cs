using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // If the shape has no hyperlinks, add a default one.
                        // Hyperlinks collection provides a Count property.
                        if (shape.Hyperlinks.Count == 0)
                        {
                            // Create a new hyperlink instance.
                            Hyperlink link = new Hyperlink();

                            // Assign a name for the hyperlink (optional).
                            link.Name = "DefaultLink";

                            // Build a default URL based on the shape's NameU.
                            // Ensure the URL is well‑formed.
                            string safeName = Uri.EscapeDataString(shape.NameU ?? "shape");
                            link.Address.Value = $"https://example.com/{safeName}";

                            // Add the hyperlink to the shape's collection.
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Processing completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}