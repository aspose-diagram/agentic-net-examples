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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify header shapes by name (contains "Header", case-insensitive)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Header", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Replace the font of each character run within the shape
                            for (int i = 0; i < shape.Chars.Count; i++)
                            {
                                // Set the font index to 0 (assumes the desired localized font is at index 0)
                                // Adjust this index as needed for your specific font collection.
                                shape.Chars[i].Font.Value = 0;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
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