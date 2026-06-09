using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (provide via command line or use a default)
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks == null)
                            continue;

                        bool hasExternalLink = false;

                        // Enumerate hyperlinks with explicit type (no var)
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Ensure the Address cell exists and has a value
                            if (link.Address == null || link.Address.Value == null)
                                continue;

                            string address = link.Address.Value;

                            // Simple check for external HTTP/HTTPS URLs
                            if (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                address.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                            {
                                hasExternalLink = true;
                                break; // No need to check further links for this shape
                            }
                        }

                        if (hasExternalLink)
                        {
                            // Output the identifier of the shape
                            Console.WriteLine($"Shape ID: {shape.ID}");
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }