using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define the hyperlink that will be added to each target shape
                    Hyperlink hyperlinkTemplate = new Hyperlink
                    {
                        Name = "WebLink",
                        Address = { Value = "https://example.com" },
                        Description = { Value = "Example Site" }
                    };

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Add a copy of the hyperlink to the shape's Hyperlinks collection
                            // (Hyperlinks collection is always instantiated by Aspose.Diagram)
                            Hyperlink link = new Hyperlink
                            {
                                Name = hyperlinkTemplate.Name,
                                Address = { Value = hyperlinkTemplate.Address.Value },
                                Description = { Value = hyperlinkTemplate.Description.Value }
                            };
                            shape.Hyperlinks.Add(link);
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }