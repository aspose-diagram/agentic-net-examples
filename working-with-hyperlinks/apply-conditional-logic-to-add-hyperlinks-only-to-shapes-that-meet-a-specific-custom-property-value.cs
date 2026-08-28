using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the custom property name and the value that qualifies a shape for hyperlink addition
                const string targetPropName = "Category";
                const string targetPropValue = "ExternalLink";

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection
                        if (shape.Props == null) continue;

                        bool qualifies = false;

                        // Search for the custom property with the specified name and value
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                qualifies = true;
                                break;
                            }
                        }

                        // If the shape meets the condition, add a hyperlink
                        if (qualifies)
                        {
                            // Ensure the Hyperlinks collection is available
                            if (shape.Hyperlinks == null) continue;

                            // Create a new hyperlink instance
                            Hyperlink link = new Hyperlink();
                            link.Name = "ExternalWebsite";
                            link.Address.Value = "https://www.example.com";

                            // Add the hyperlink to the shape
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