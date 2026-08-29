using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Use the active page to add a rectangle shape
                // Parameters: PinX, PinY (in inches), master name, isCalculate flag
                double pinX = 2.0;
                double pinY = 2.0;
                string masterName = "Rectangle";
                bool isCalculate = false;

                // AddShape returns the shape ID (long)
                long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName, isCalculate);

                // Retrieve the Shape object using the ID
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Set the default tab stop to 0.5 inches for the shape's text block
                // This controls the spacing of tab characters within the shape's text
                shape.TextBlock.DefaultTabStop.Value = 0.5;

                // Optionally add some text containing tabs to demonstrate the effect
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Item1\tItem2\tItem3"));

                // Save the diagram to a VSDX file
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }