using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSD, VDX, VSDX, etc.)
                // You can pass the path as a command‑line argument or set it directly here.
                string diagramPath = args.Length > 0 ? args[0] : "sample.vsdx";

                // Load the diagram using Aspose.Diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape has any hyperlinks
                            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            {
                                // Print the shape's name
                                Console.WriteLine($"Shape: {shape.Name}");

                                // Iterate through each hyperlink and print its address (URL)
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    Console.WriteLine($"  Hyperlink: {link.Address}");
                                }
                            }
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