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

                // Use the first page (a new diagram contains one default page)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: PinX, PinY, Width, Height, Master name, page index
                long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Add multi‑line text to the shape
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("First line\nSecond line\nThird line"));

                // Create a custom stylesheet that will be used for text formatting
                StyleSheet style = new StyleSheet();
                // Assign a unique ID (must be greater than existing count)
                style.ID = diagram.StyleSheets.Count + 1;
                // (Optional) give the style a name for identification
                style.Name = "CustomLineHeightStyle";

                // Add the stylesheet to the diagram
                diagram.StyleSheets.Add(style);

                // Apply the stylesheet to the shape's text style
                shape.TextStyle = style;

                // Set the desired line (paragraph) spacing on the shape's first paragraph
                // SpLine.Value defines the line spacing (in inches)
                double desiredLineSpacing = 0.3; // example line height
                shape.Paras[0].SpLine.Value = desiredLineSpacing;

                // Verify that the line spacing was applied correctly
                double actualLineSpacing = shape.Paras[0].SpLine.Value;
                if (Math.Abs(actualLineSpacing - desiredLineSpacing) > 0.0001)
                {
                    throw new Exception($"Line spacing verification failed. Expected: {desiredLineSpacing}, Actual: {actualLineSpacing}");
                }
                else
                {
                    Console.WriteLine("Line spacing verified successfully.");
                }

                // Save the diagram to a VSDX file (optional, demonstrates a valid save operation)
                diagram.Save("LineHeightDemo.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }