using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the first page (page index 0)
                // Parameters: pinX, pinY, width, height, master name, page index
                long shapeId = diagram.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", 0);

                // Retrieve the shape object using the returned ID
                Shape shape = diagram.Pages[0].Shapes.GetShape((int)shapeId);

                // Create a new hyperlink that points to an external URL
                Hyperlink link = new Hyperlink();
                link.Name = "ExternalLink";
                link.Address.Value = "https://www.example.com";
                link.Description.Value = "Open Example.com";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Configure PDF save options (default options are sufficient for hyperlinks)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Save the diagram as a PDF file; the hyperlink will be clickable in the output PDF
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }