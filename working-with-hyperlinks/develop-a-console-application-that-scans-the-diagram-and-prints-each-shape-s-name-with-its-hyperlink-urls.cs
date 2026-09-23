using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Verify that a diagram file path was provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramHyperlinkScanner <diagram file path>");
                return;
            }

            string diagramPath = args[0];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Output the shape's name
                    Console.WriteLine($"Shape Name: {shape.Name}");

                    // If the shape contains hyperlinks, list their URLs
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Hyperlink address is stored in the Address cell; use .Value to retrieve it
                            Console.WriteLine($"  Hyperlink URL: {link.Address.Value}");
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
