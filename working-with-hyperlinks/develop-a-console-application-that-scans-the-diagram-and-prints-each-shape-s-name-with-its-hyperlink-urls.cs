using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file (provide via command line or use default)
            string filePath = args.Length > 0 ? args[0] : "sample.vsdx";

            // Load the diagram using the Diagram constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Output the shape's name
                        Console.WriteLine($"Shape: {shape.Name}");

                        // If the shape contains hyperlinks, list each URL
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
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
