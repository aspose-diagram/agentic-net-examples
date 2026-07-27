using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a blank page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Define page size (optional, 10 inches x 10 inches)
                page.PageSheet.PageProps.PageWidth.Value = 10.0;
                page.PageSheet.PageProps.PageHeight.Value = 10.0;

                // -----------------------------------------------------------------
                // Create guide lines (thin gray lines) on the page
                // -----------------------------------------------------------------

                // Horizontal guide at Y = 5 inches (across the page width)
                long horizGuideId = page.DrawLine(0.0, 5.0, 10.0, 5.0);
                Shape horizGuide = page.Shapes.GetShape(horizGuideId);
                horizGuide.Line.LineColor.Value = "#CCCCCC";   // Light gray color
                horizGuide.Line.LineWeight.Value = 0.01;      // Very thin line

                // Vertical guide at X = 5 inches (across the page height)
                long vertGuideId = page.DrawLine(5.0, 0.0, 5.0, 10.0);
                Shape vertGuide = page.Shapes.GetShape(vertGuideId);
                vertGuide.Line.LineColor.Value = "#CCCCCC";
                vertGuide.Line.LineWeight.Value = 0.01;

                // -----------------------------------------------------------------
                // Add a rectangle shape and align it precisely to the guides
                // -----------------------------------------------------------------

                // Add a rectangle shape (2 inches wide, 1 inch high) centered at (5,5)
                long rectId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);
                Shape rectangle = page.Shapes.GetShape(rectId);

                // Ensure the rectangle is exactly on the intersection of the guides
                rectangle.XForm.PinX.Value = 5.0; // Align X to vertical guide
                rectangle.XForm.PinY.Value = 5.0; // Align Y to horizontal guide

                // Optional styling for the rectangle
                rectangle.Fill.FillForegnd.Value = "#FFCC00"; // Fill color
                rectangle.Line.LineColor.Value = "#000000";   // Border color
                rectangle.Line.LineWeight.Value = 0.02;      // Border thickness

                // Add some text to the rectangle
                rectangle.Text.Value.Clear();
                rectangle.Text.Value.Add(new Txt("Aligned Box"));

                // -----------------------------------------------------------------
                // Save the diagram to a VSDX file
                // -----------------------------------------------------------------
                diagram.Save("GuidesDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }