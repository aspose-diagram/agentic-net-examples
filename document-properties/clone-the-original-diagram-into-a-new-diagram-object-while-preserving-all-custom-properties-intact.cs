using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the source Visio file as the first argument.");
            return;
        }

        string sourcePath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Determine output path (same directory with suffix "_clone.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(sourcePath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(sourcePath) + "_clone.vsdx");

        try
        {
            // Load the original diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Create a new empty diagram
            Diagram clonedDiagram = new Diagram();

            // Combine all pages, masters, and other content from the source diagram
            clonedDiagram.Combine(sourceDiagram);

            // Preserve custom document properties
            foreach (CustomProp srcProp in sourceDiagram.DocumentProps.CustomProps)
            {
                // Create a new custom property and copy its details
                CustomProp newProp = new CustomProp
                {
                    Name = srcProp.Name,
                    PropType = srcProp.PropType
                };

                // Copy the custom value if present
                if (srcProp.CustomValue != null)
                {
                    newProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                }

                // Add the custom property to the cloned diagram
                clonedDiagram.DocumentProps.CustomProps.Add(newProp);
            }

            // Save the cloned diagram with all content and custom properties
            clonedDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram cloned successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}