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
                    // Get the first (default) page
                    Page page = diagram.Pages[0];

                    // Add a rectangle shape to the page
                    // Parameters: PinX, PinY, Width, Height, MasterName
                    double pinX = 5.0;   // center X position (in inches)
                    double pinY = 5.0;   // center Y position (in inches)
                    double width = 4.0;  // shape width (in inches)
                    double height = 2.0; // shape height (in inches)
                    long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

                    // Retrieve the shape object using its ID
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and add new text
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Diagonal Text"));

                    // Rotate the text inside the shape (45 degrees)
                    // TextXForm.TxtAngle expects radians
                    double angleDeg = 45.0;
                    double angleRad = (Math.PI / 180.0) * angleDeg;
                    shape.TextXForm.TxtAngle.Value = angleRad;

                    // Adjust text block margins (in inches)
                    // Left, Right, Top, Bottom margins set to 0.05 inches
                    double margin = 0.05;
                    shape.TextBlock.LeftMargin.Value = margin;
                    shape.TextBlock.RightMargin.Value = margin;
                    shape.TextBlock.TopMargin.Value = margin;
                    shape.TextBlock.BottomMargin.Value = margin;

                    // Save the diagram to a VSDX file
                    diagram.Save("DiagonalText.vsdx", SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created and saved as DiagonalText.vsdx");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }