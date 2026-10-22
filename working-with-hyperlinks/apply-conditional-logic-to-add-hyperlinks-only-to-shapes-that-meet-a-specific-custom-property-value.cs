using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the custom property name and the value that qualifies a shape for hyperlink addition
            const string targetPropName = "TargetProperty";
            const string targetPropValue = "DesiredValue";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine whether the shape contains the target custom property with the required value
                    bool shouldAddLink = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                        {
                            shouldAddLink = true;
                            break;
                        }
                    }

                    // If condition met, add a hyperlink to the shape
                    if (shouldAddLink)
                    {
                        // Create a new hyperlink instance
                        Hyperlink link = new Hyperlink();
                        link.Name = "MyLink";
                        link.Address.Value = "https://example.com";

                        // Ensure the Hyperlinks collection exists and add the new link
                        if (shape.Hyperlinks == null)
                        {
                            // The Hyperlinks collection is normally initialized by the API,
                            // but this guard protects against unexpected null references.
                            // If null, we cannot add a hyperlink, so we skip this shape.
                            Console.Error.WriteLine($"Shape ID {shape.ID} has a null Hyperlinks collection; skipping hyperlink addition.");
                            continue;
                        }

                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}