using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "cloned.vsdx";

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original diagram inside a using block
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Create a new empty diagram inside a using block
                using (Diagram clonedDiagram = new Diagram())
                {
                    // ------------------------------------------------------------
                    // Copy custom document properties from source to clone
                    // ------------------------------------------------------------
                    foreach (CustomProp srcProp in sourceDiagram.DocumentProps.CustomProps)
                    {
                        // Create a new custom property and copy its fields
                        CustomProp newProp = new CustomProp();
                        newProp.Name = srcProp.Name;
                        newProp.PropType = srcProp.PropType;

                        // Copy the custom value (string representation)
                        if (srcProp.CustomValue != null && srcProp.CustomValue.ValueString != null)
                        {
                            newProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                        }

                        // Add the new property to the cloned diagram
                        clonedDiagram.DocumentProps.CustomProps.Add(newProp);
                    }

                    // ------------------------------------------------------------
                    // Merge all pages, masters, and other content from the source diagram
                    // ------------------------------------------------------------
                    clonedDiagram.Combine(sourceDiagram);

                    // ------------------------------------------------------------
                    // Save the cloned diagram to the specified output path
                    // ------------------------------------------------------------
                    clonedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Cloned diagram saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}