using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape (pin at (5,5), size 4x2 inches)
                long shapeId = page.DrawRectangle(5.0, 5.0, 4.0, 2.0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add new text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Diagonal Text"));

                // Rotate the text 45 degrees (convert degrees to radians)
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                shape.TextXForm.TxtAngle.Value = angleRad;

                // Adjust text block margins (in inches)
                shape.TextBlock.LeftMargin.Value = 0.05;   // 0.05 inch left margin
                shape.TextBlock.RightMargin.Value = 0.05;  // 0.05 inch right margin
                shape.TextBlock.TopMargin.Value = 0.02;    // 0.02 inch top margin
                shape.TextBlock.BottomMargin.Value = 0.02; // 0.02 inch bottom margin

                // Optionally, set a background color for the text block (using RGB string format)
                shape.TextBlock.TextBkgnd.Ufe.F = "RGB(200,200,200)";

                // Save the diagram to a VSDX file
                diagram.Save("DiagonalTextShape.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram created and saved successfully.");
        }
    }