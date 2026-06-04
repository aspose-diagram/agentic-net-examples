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

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Apply a light‑gray background to every page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center of the page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Draw a rectangle that spans the whole page
                    long rectId = page.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                    Shape rectShape = page.Shapes.GetShape((int)rectId);

                    // Fill with solid light gray (#D3D3D3)
                    rectShape.Fill.FillPattern.Value = 1;                     // solid fill
                    rectShape.Fill.FillForegnd.Value = "#D3D3D3";             // light gray color

                    // Remove the rectangle border
                    rectShape.Line.LinePattern.Value = (LinePatternValue)0;   // no line

                    // Send the rectangle to the back so other shapes appear above it
                    rectShape.SendToBack();

                    // Make the background shape non‑selectable
                    rectShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Export the diagram to PNG (image) format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save("output.png", saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
