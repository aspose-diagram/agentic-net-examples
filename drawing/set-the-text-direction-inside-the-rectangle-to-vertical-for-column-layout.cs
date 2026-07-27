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

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Add a rectangle shape using the built‑in "Rectangle" master
                // Parameters: PinX, PinY, master name
                long rectangleId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape object by its ID
                Shape rectangle = page.Shapes.GetShape(rectangleId);

                // Clear any existing text and add new text
                rectangle.Text.Value.Clear();
                rectangle.Text.Value.Add(new Txt("Column Text"));

                // Set the text direction of the shape to vertical (column layout)
                rectangle.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

                // Save the diagram to a VSDX file
                diagram.Save("RectangleVerticalText.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }