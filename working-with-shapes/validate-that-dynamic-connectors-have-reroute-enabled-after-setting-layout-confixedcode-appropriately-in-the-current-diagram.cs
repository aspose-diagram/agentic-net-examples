using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to set ConFixedCode for dynamic connectors.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify dynamic connector shapes by master name.
                    if (shape.Master != null && shape.Master.Name != null &&
                        shape.Master.Name.Equals("Dynamic connector", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set ConFixedCode to a defined value (Undefined is the only confirmed member).
                        // This represents the appropriate configuration for reroute behavior.
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                    }
                }
            }

            // Validate that all dynamic connectors have the expected ConFixedCode setting.
            bool allRerouteEnabled = true;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name != null &&
                        shape.Master.Name.Equals("Dynamic connector", StringComparison.OrdinalIgnoreCase))
                    {
                        // Check the ConFixedCode value; if it differs from the expected value, flag as failure.
                        if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.Undefined)
                        {
                            allRerouteEnabled = false;
                            Console.Error.WriteLine($"Connector ID {shape.ID} on page {page.ID} does not have the expected ConFixedCode setting.");
                        }
                    }
                }
            }

            if (allRerouteEnabled)
            {
                Console.WriteLine("All dynamic connectors have reroute enabled (ConFixedCode set to Undefined).");
            }
            else
            {
                throw new Exception("One or more dynamic connectors do not have reroute enabled.");
            }

            // Optional: save the modified diagram if an output path is provided.
            if (args.Length > 1)
            {
                string outputPath = args[1];
                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}