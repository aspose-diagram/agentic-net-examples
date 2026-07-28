using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // -----------------------------------------------------------------
            // Create a single stylesheet that defines a solid white fill
            // -----------------------------------------------------------------
            StyleSheet whiteStyle = new StyleSheet();
            whiteStyle.ID = diagram.StyleSheets.Count + 1;          // unique ID
            whiteStyle.Fill.FillPattern.Value = 1;                // solid pattern
            whiteStyle.Fill.FillForegnd.Value = "#FFFFFF";        // white color
            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(whiteStyle);

            // -----------------------------------------------------------------
            // Loop through every page and apply the white background
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center of the page (pin coordinates)
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Draw a rectangle that spans the entire page
                long shapeId = page.DrawRectangle(pinX, pinY, pageWidth, pageHeight);

                // Get the shape object from the returned ID
                Shape backgroundShape = page.Shapes.GetShape((int)shapeId);

                // Apply the previously created white stylesheet
                backgroundShape.FillStyle = whiteStyle;

                // Send the shape to the back so other content appears above it
                backgroundShape.SendToBack();

                // Make the background shape non‑selectable
                backgroundShape.Protection.LockSelect.Value = BOOL.True;
            }

            // -----------------------------------------------------------------
            // Save the modified diagram
            // -----------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
