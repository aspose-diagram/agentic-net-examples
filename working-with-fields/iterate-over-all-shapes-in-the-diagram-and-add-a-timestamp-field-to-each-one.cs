using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked for deletion.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Props collection is available.
                        if (shape.Props == null)
                            continue;

                        // Create a new custom property (Prop) to hold the timestamp.
                        Prop timestampProp = new Prop();
                        timestampProp.Name = "Timestamp";
                        timestampProp.Value.Val = DateTime.Now.ToString("o"); // ISO 8601 format

                        // Add the property to the shape's Props collection.
                        shape.Props.Add(timestampProp);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with timestamps to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}