using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // The hyperlink address to add to each shape
                const string hyperlinkAddress = "https://example.com";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the Hyperlinks collection is available
                            if (shape.Hyperlinks == null)
                                continue;

                            // Create a new hyperlink instance
                            Hyperlink link = new Hyperlink();
                            link.Name = "WebLink";
                            link.Address.Value = hyperlinkAddress;

                            // Add the hyperlink to the shape's collection
                            shape.Hyperlinks.Add(link);
                        }
                    }

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Hyperlinks added and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }