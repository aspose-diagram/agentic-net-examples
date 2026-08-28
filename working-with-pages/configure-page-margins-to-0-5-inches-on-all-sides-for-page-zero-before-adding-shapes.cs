using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram instance
                using (Diagram diagram = new Diagram())
                {
                    // Access the first page (page index 0)
                    Page page = diagram.Pages[0];

                    // Configure margins: 0.5 inches on each side
                    page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

                    // Add a rectangle shape after setting margins
                    long shapeId = diagram.AddShape(2.0, 2.0, 1.5, 1.0, "Rectangle", 0);
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Sample Shape"));

                    // Save the diagram to a VSDX file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }