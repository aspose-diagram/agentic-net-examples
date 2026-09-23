using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Access the first page (page index 0)
                Page page = diagram.Pages[0];

                // Set all four page margins to 0.5 inches
                page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

                // Add a rectangle shape to the page after configuring margins
                // Parameters: pinX, pinY, master name, isCalculate (bool)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set some sample text on the shape
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Sample shape"));

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }