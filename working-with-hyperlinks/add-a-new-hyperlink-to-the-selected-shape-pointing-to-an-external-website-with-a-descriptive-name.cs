using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found on the first page.");
                    return;
                }

                // Create a new hyperlink instance
                Hyperlink link = new Hyperlink();
                link.Name = "ExternalSiteLink";                     // Optional internal name
                link.Address.Value = "https://www.example.com";    // External URL
                link.Description.Value = "Visit Example.com";      // Descriptive tooltip

                // Add the hyperlink to the shape's Hyperlinks collection
                targetShape.Hyperlinks.Add(link);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Hyperlink added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }