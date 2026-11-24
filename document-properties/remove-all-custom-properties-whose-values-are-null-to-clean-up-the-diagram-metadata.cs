using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine output path (same folder, new file name).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                         Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the custom properties collection.
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // Iterate backwards to safely remove items while iterating.
                for (int i = customProps.Count - 1; i >= 0; i--)
                {
                    CustomProp prop = customProps[i];

                    // If the property value is null (or the CustomValue object itself is null), remove it.
                    if (prop.CustomValue == null || prop.CustomValue.ValueString == null)
                    {
                        customProps.Remove(prop);
                    }
                }

                // Save the cleaned diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram cleaned and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}