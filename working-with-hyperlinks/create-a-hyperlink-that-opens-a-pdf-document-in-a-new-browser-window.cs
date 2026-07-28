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
                // Parameters: PinX, PinY, Width, Height, Master name, isCalculate (bool)
                long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Add a hyperlink that points to a PDF document
                Hyperlink link = new Hyperlink();
                link.Address.Value = "https://example.com/document.pdf"; // URL of the PDF
                link.Description.Value = "Open PDF in new browser window";
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                diagram.Save("HyperlinkDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }