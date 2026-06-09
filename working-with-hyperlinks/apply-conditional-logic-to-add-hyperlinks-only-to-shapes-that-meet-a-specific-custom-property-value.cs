using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Custom property criteria
                string targetPropName = "AddLinkFlag";   // Name of the custom property to evaluate
                string targetPropValue = "True";         // Desired value that triggers hyperlink addition

                // Hyperlink details to be added
                string hyperlinkUrl = "https://www.example.com";
                string hyperlinkName = "ExternalLink";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection
                        if (shape.Props == null)
                            continue;

                        // Look for the custom property with the specified name and value
                        bool shouldAddLink = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                shouldAddLink = true;
                                break;
                            }
                        }

                        // If the condition is met, add a hyperlink to the shape
                        if (shouldAddLink)
                        {
                            // Ensure the Hyperlinks collection is instantiated
                            if (shape.Hyperlinks == null)
                                continue; // Safety check; normally this collection is always available

                            Hyperlink link = new Hyperlink();
                            link.Name = hyperlinkName;
                            link.Address.Value = hyperlinkUrl;
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }