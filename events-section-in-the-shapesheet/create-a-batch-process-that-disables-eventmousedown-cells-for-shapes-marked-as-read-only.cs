using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchProcessReadOnlyShapes <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the diagram from the specified input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine if the shape is marked as read‑only via a custom property named "ReadOnly"
                    bool isReadOnly = false;
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "ReadOnly" &&
                                string.Equals(prop.Value.Val, "True", StringComparison.OrdinalIgnoreCase))
                            {
                                isReadOnly = true;
                                break;
                            }
                        }
                    }

                    // If the shape is read‑only, disable a relevant event cell (EventDrop used as a representative)
                    if (isReadOnly)
                    {
                        // Setting the formula to an empty string disables the event
                        shape.Event.EventDrop.Ufe.F = "";
                    }
                }
            }

            // Save the modified diagram using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing complete. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}