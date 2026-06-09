using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSD, VDX, VSDX, etc.)
                // You can pass the file path as a command‑line argument or modify the string below.
                string diagramPath = args.Length > 0 ? args[0] : "sample.vsdx";

                // Load the diagram using the appropriate constructor.
                // The Diagram class implements IDisposable, so we use a using block.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the shape's name; fallback to NameU if Name is null or empty.
                            string shapeName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : shape.NameU;

                            // Check if the shape contains any hyperlinks.
                            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            {
                                Console.WriteLine($"Shape: {shapeName}");
                                // Iterate through each hyperlink and print its address (URL or file path).
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