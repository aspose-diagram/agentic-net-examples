using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (same folder with "_updated" suffix).
        string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_updated.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through custom properties (Props) of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Look for a property named "Version".
                        if (string.Equals(prop.Name, "Version", StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the property has a value.
                            if (prop.Value != null && prop.Value.Val != null)
                            {
                                string currentValue = prop.Value.Val;
                                // Replace 'v1.0' with 'v2.0' if present.
                                if (currentValue.Contains("v1.0"))
                                {
                                    string newValue = currentValue.Replace("v1.0", "v2.0");
                                    prop.Value.Val = newValue;
                                    Console.WriteLine($"Updated shape ID {shape.ID} on page {page.ID}: Version = \"{newValue}\"");
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}