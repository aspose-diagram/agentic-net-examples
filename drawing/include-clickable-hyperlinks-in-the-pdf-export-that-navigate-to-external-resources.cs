using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("Diagram has no pages.");
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name, page index
                long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Add some visible text to the shape
                shape.Text.Value.Add(new Txt("Click here for Aspose"));

                // Create a new hyperlink and set its address and description
                Hyperlink link = new Hyperlink();
                link.Name = "AsposeLink";
                link.Address.Value = "https://www.aspose.com";
                link.Description.Value = "Visit Aspose website";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Configure PDF save options (no special settings needed for hyperlinks)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Save the diagram as a PDF; hyperlinks will be active in the output file
                diagram.Save("HyperlinkedDiagram.pdf", pdfOptions);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }