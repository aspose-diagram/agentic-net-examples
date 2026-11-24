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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                                                   Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

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
                    // Ensure the shape has custom properties
                    if (shape.Props == null)
                        continue;

                    // Iterate through each custom property
                    foreach (Prop prop in shape.Props)
                    {
                        // Check for the "Priority" property with value "Low"
                        if (prop.Name == "Priority" && prop.Value != null && prop.Value.Val == "Low")
                        {
                            // Update the value to "High"
                            prop.Value.Val = "High";
                            Console.WriteLine($"Updated shape ID {shape.ID} on page {page.ID} - Priority set to High.");
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}