using System;
using System.IO;
using System.Reflection; // Added for reflection-based Validate call
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Perform automatic spacing/layout on each page using CompactTree.
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Create layout options for compact tree layout.
                    LayoutOptions layoutOptions = new LayoutOptions
                    {
                        LayoutStyle = LayoutStyle.CompactTree,
                        Direction = LayoutDirection.DownThenRight,
                        EnlargePage = false // Changed from BOOL.False to bool false
                    };

                    // Apply layout to the current page.
                    page.Layout(layoutOptions);
                }

                // -----------------------------------------------------------------
                // Validate the diagram after spacing if a Validate method is available.
                // Uses reflection to call Validate only when it exists.
                // -----------------------------------------------------------------
                MethodInfo validateMethod = typeof(Diagram).GetMethod("Validate", BindingFlags.Instance | BindingFlags.Public);
                if (validateMethod != null)
                {
                    validateMethod.Invoke(diagram, null);
                    Console.WriteLine("Diagram validation completed.");
                }
                else
                {
                    Console.WriteLine("Validate method not available; skipping validation.");
                }

                Console.WriteLine("Diagram layout completed.");

                // -----------------------------------------------------------------
                // Save the processed diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
