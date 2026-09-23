using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the custom property name and the value that qualifies a shape for hyperlink addition
                const string targetPropName = "Category";
                const string targetPropValue = "ExternalLink";

                // Define the hyperlink details to be added
                const string hyperlinkAddress = "https://www.example.com";
                const string hyperlinkDescription = "Visit Example";

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has custom properties
                        if (shape.Props == null)
                            continue;

                        // Look for the target custom property
                        bool matches = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                matches = true;
                                break;
                            }
                        }

                        // If the shape meets the condition, add a hyperlink
                        if (matches)
                        {
                            // Ensure the Hyperlinks collection exists
                            if (shape.Hyperlinks == null)
                                continue; // Should not happen, but safety check

                            // Create and configure the hyperlink
                            Hyperlink link = new Hyperlink();
                            link.Name = "AutoLink";
                            link.Address.Value = hyperlinkAddress;
                            link.Description.Value = hyperlinkDescription;

                            // Add the hyperlink to the shape
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }