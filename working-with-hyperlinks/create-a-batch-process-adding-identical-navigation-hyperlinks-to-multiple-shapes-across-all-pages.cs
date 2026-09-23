using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be passed as command‑line arguments)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through every page
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through every shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.False)
                            {
                                // Ensure the Hyperlinks collection exists
                                if (shape.Hyperlinks != null)
                                {
                                    // Create a new hyperlink pointing to the desired URL
                                    Hyperlink link = new Hyperlink
                                    {
                                        Name = "NavLink"
                                    };
                                    link.Address.Value = "https://example.com";

                                    // Add the hyperlink to the shape
                                    shape.Hyperlinks.Add(link);
                                }
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Hyperlinks added and diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }