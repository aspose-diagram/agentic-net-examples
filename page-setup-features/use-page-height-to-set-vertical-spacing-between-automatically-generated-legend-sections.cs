using System.IO;
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
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page
                Page page = diagram.Pages[0];

                // Set page size (optional, using default size here)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define legend parameters
                int legendCount = 3;                     // Number of legend sections to create
                double legendWidth = 2.0;                // Width in inches
                double legendHeight = 0.5;               // Height in inches
                double legendPinX = 1.0;                 // Horizontal position (in inches) from left edge

                // Calculate vertical spacing based on page height
                // Spacing is the distance between the top edges of consecutive legends
                double verticalSpacing = pageHeight / (legendCount + 1);

                for (int i = 1; i <= legendCount; i++)
                {
                    // Compute the Y coordinate for the current legend
                    double legendPinY = verticalSpacing * i;

                    // Add a rectangle shape as a legend entry
                    // The last parameter 'false' indicates that the shape should not be auto‑calculated
                    long shapeId = page.AddShape(legendPinX, legendPinY, legendWidth, legendHeight, "Rectangle", false);
                    Shape legendShape = page.Shapes.GetShape(shapeId);

                    // Set a light fill color for visual distinction
                    legendShape.Fill.FillForegnd.Value = "#D3E4F1";

                    // Add descriptive text to the legend shape
                    legendShape.Text.Value.Clear();
                    legendShape.Text.Value.Add(new Txt($"Legend {i}"));

                    // Center the text horizontally within the shape
                    legendShape.TextXForm.TxtLocPinX.Value = 0.5; // Relative position (0 = left, 1 = right)
                    legendShape.TextXForm.TxtLocPinY.Value = 0.5; // Relative position (0 = bottom, 1 = top)
                    legendShape.TextXForm.TxtPinX.Value = legendPinX + legendWidth / 2;
                    legendShape.TextXForm.TxtPinY.Value = legendPinY + legendHeight / 2;
                }

                // Save the diagram to a VSDX file
                diagram.Save("LegendDiagram.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
