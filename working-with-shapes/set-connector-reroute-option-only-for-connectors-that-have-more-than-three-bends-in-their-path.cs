using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // TODO: Determine the number of bends in the connector's path.
                            // The Aspose.Diagram API does not expose a direct bend count property.
                            // Replace the following placeholder with the appropriate logic when available.
                            int bendCount = 0; // Placeholder value

                            // Apply reroute option only if there are more than three bends
                            if (bendCount > 3)
                            {
                                // TODO: Set the reroute option correctly.
                                // The ConFixedCode enum currently only defines Undefined.
                                // Casting an integer may be required to enable rerouting, but verify with the library documentation.
                                shape.Layout.ConFixedCode.Value = (ConFixedCodeValue)1;
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}