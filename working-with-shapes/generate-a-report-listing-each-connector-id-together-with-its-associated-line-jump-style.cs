using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input‑visio‑file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify dynamic connector shapes.
                        if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Retrieve the line jump style from the shape's layout.
                            var layout = shape.Layout;
                            var jumpStyleValue = layout?.ConLineJumpStyle?.Value;

                            // Convert the enum value to a readable string; handle undefined cases.
                            string jumpStyle = jumpStyleValue.HasValue
                                ? jumpStyleValue.Value.ToString()
                                : "Undefined";

                            // Output the connector ID and its associated jump style.
                            Console.WriteLine($"Connector ID: {shape.ID}, Jump Style: {jumpStyle}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}