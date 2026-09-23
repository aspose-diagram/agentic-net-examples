using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Prompt user for line thickness (in inches)
            Console.Write("Enter line thickness for the circle (in inches, e.g., 0.02): ");
            string input = Console.ReadLine();

            if (!double.TryParse(input, out double lineThickness) || lineThickness <= 0)
            {
                Console.WriteLine("Invalid input. Please provide a positive numeric value.");
                return;
            }

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Draw a circle (ellipse with equal width and height)
            // Parameters: pinX, pinY (center), width, height
            double centerX = 5.0; // inches
            double centerY = 5.0; // inches
            double diameter = 2.0; // inches
            long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

            // Retrieve the created shape
            Shape circleShape = page.Shapes.GetShape(shapeId);

            // Adjust the line thickness
            circleShape.Line.LineWeight.Value = lineThickness;

            // Optional: set a visible line color
            circleShape.Line.LineColor.Value = "#0000FF"; // blue

            // Save the diagram to a VSDX file
            string outputPath = "CircleWithCustomLineThickness.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}' with line thickness {lineThickness} inches.");
        }
    }