using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through all custom properties of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Check for the custom property named "Version".
                        if (prop.Name == "Version")
                        {
                            // Ensure the property value is not null.
                            if (prop.Value != null && prop.Value.Val != null)
                            {
                                // Perform the find‑and‑replace if the old text is present.
                                if (prop.Value.Val.Contains("v1.0"))
                                {
                                    string updated = prop.Value.Val.Replace("v1.0", "v2.0");
                                    prop.Value.Val = updated;
                                    Console.WriteLine($"Updated shape ID {shape.ID} on page {page.ID}.");
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}