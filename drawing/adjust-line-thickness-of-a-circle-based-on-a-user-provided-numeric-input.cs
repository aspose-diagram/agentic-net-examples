using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Prompt the user for the desired line thickness (in inches)
            Console.Write("Enter line thickness (in inches, e.g., 0.02): ");
            string input = Console.ReadLine();

            if (!double.TryParse(input, out double lineThickness) || lineThickness <= 0)
            {
                Console.WriteLine("Invalid input. Please provide a positive numeric value.");
                return;
            }

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (default page is always present)
            Page page = diagram.Pages[0];

            // Draw a circle (ellipse with equal width and height)
            // Parameters: PinX, PinY (center), Width, Height
            double centerX = 5.0;   // inches from the left edge
            double centerY = 5.0;   // inches from the top edge
            double diameter = 4.0;  // inches

            long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

            // Retrieve the shape object to modify its line properties
            Shape circleShape = page.Shapes.GetShape(shapeId);

            // Set the line thickness (weight) using the user-provided value
            circleShape.Line.LineWeight.Value = lineThickness;

            // Save the diagram as a PNG image
            string outputPath = "CircleWithCustomThickness.png";
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram saved to '{outputPath}' with line thickness {lineThickness} inches.");
        }
    }