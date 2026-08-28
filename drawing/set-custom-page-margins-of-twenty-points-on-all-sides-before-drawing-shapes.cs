using System.IO;
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
                // Define margin: 20 points = 20/72 inches
                double marginInches = 20.0 / 72.0;

                // Apply the margin to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
                }

                // Example shape creation after margins are set
                Page firstPage = diagram.Pages[0];
                double pinX = 2.0;   // X position in inches
                double pinY = 2.0;   // Y position in inches
                double width = 3.0;  // Width in inches
                double height = 1.5; // Height in inches

                // Add a rectangle shape (master name "Rectangle")
                long shapeId = firstPage.AddShape(pinX, pinY, width, height, "Rectangle");
                Shape rectangle = firstPage.Shapes.GetShape(shapeId);

                // Optional: set a fill color for the rectangle
                rectangle.Fill.FillForegnd.Value = "#FFCC00";

                // Save the diagram to a VSDX file
                diagram.Save("Output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
