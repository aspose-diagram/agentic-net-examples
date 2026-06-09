using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // The hyperlink to add to each shape
                string hyperlinkUrl = "https://example.com";
                string hyperlinkDescription = "Navigate to Example";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Create a new hyperlink instance
                            Hyperlink link = new Hyperlink();
                            link.Name = "NavLink";
                            link.Address.Value = hyperlinkUrl;          // Set the external URL
                            link.Description.Value = hyperlinkDescription; // Optional tooltip text

                            // Add the hyperlink to the shape's Hyperlinks collection
                            shape.Hyperlinks.Add(link);
                        }
                    }

                    // Save the modified diagram to a new file (VSDX format)
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Hyperlinks added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }