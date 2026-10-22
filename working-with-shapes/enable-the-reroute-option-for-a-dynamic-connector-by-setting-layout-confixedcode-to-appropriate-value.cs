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
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify dynamic connector shapes:
                        // 1. Must be a 1‑D shape (connector).
                        // 2. Master name should contain "Dynamic connector".
                        if (!shape.OneD)
                            continue;

                        if (shape.Master == null || shape.Master.Name == null)
                            continue;

                        if (shape.Master.Name.IndexOf("Dynamic connector", StringComparison.OrdinalIgnoreCase) < 0)
                            continue;

                        // Enable reroute by setting ConFixedCode to Undefined (default behavior).
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                    }
                }

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}