using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the hyperlink address (cell-based, use .Value)
                                string address = link.Address.Value;

                                // Check if the address points to an external HTTP/HTTPS URL
                                if (!string.IsNullOrEmpty(address) &&
                                    (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                     address.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                                {
                                    // Output the shape identifier and the external hyperlink
                                    Console.WriteLine($"Shape ID: {shape.ID} -> External Link: {address}");
                                    // One external link per shape is enough for listing
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }