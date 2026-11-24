using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: input diagram file path
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-diagram-path]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional output path; if not provided, overwrite the input file
        string outputPath = args.Length >= 2 ? args[1] : inputPath;

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify dynamic connector shapes by master name
                        if (shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Ensure the Layout object exists
                            if (shape.Layout != null)
                            {
                                // Set ConFixedCode to enable rerouting
                                shape.Layout.ConFixedCode.Value = ConFixedCodeValue.RerouteFreely;

                                // Validate that reroute is now enabled
                                if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.RerouteFreely)
                                {
                                    Console.Error.WriteLine($"Connector ID {shape.ID} reroute not enabled after setting.");
                                }
                            }
                            else
                            {
                                Console.Error.WriteLine($"Connector ID {shape.ID} has no Layout object.");
                            }
                        }
                    }
                }

                // Save the modified diagram (using SaveFileFormat to preserve format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}