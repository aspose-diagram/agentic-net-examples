using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page; add a blank page if none exist
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first page (index 0)
                int pageIndex = 0;

                // Retrieve the master named "Rectangle"
                // If the master does not exist, this will throw; in a real scenario ensure the master is loaded.
                Master rectangleMaster = diagram.Masters.GetMasterByName("Rectangle");
                string masterName = rectangleMaster.Name;

                // Define position for the new rectangle (in inches)
                double pinX = 2.0; // X coordinate of the shape's pin (center)
                double pinY = 2.0; // Y coordinate of the shape's pin (center)

                // Add the rectangle shape to the diagram using the master name and page index
                long shapeId = diagram.AddShape(pinX, pinY, masterName, pageIndex);

                // Retrieve the created shape if further modifications are needed
                Shape rectangleShape = diagram.Pages[pageIndex].Shapes.GetShape(shapeId);

                // Example: set some basic properties (optional)
                rectangleShape.XForm.Width.Value = 1.5;   // width in inches
                rectangleShape.XForm.Height.Value = 1.0; // height in inches
                rectangleShape.Fill.FillForegnd.Value = "#FF0000"; // fill color red
                rectangleShape.Line.LineColor.Value = "#000000";   // line color black

                // The diagram now contains the new rectangle shape.
                // (Optional) Save the diagram to verify the result:
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }