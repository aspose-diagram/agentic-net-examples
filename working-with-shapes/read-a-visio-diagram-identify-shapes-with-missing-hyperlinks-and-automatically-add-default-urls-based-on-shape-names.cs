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
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is not deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape already has hyperlinks
                        bool hasHyperlink = shape.Hyperlinks != null && shape.Hyperlinks.Count > 0;

                        if (!hasHyperlink)
                        {
                            // Create a default hyperlink based on the shape's universal name
                            Hyperlink link = new Hyperlink();
                            link.Name = "DefaultLink";
                            link.Address.Value = $"https://example.com/{shape.NameU}";

                            // Add the hyperlink to the shape
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }