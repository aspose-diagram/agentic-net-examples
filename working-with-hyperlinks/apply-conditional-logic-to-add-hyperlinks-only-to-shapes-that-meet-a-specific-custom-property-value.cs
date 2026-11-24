using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output diagram path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        // Custom property name to evaluate
        string targetPropName = args.Length > 2 ? args[2] : "MyProp";
        // Expected value of the custom property
        string targetPropValue = args.Length > 3 ? args[3] : "AddLink";
        // Hyperlink URL to add when condition is met
        string hyperlinkUrl = args.Length > 4 ? args[4] : "https://example.com";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine whether the shape has the target custom property with the required value
                    bool matchesCondition = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                        {
                            matchesCondition = true;
                            break;
                        }
                    }

                    // If condition is satisfied, add a hyperlink to the shape
                    if (matchesCondition)
                    {
                        Hyperlink link = new Hyperlink();
                        link.Name = "Link";
                        link.Address.Value = hyperlinkUrl;
                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}