using System;
using Aspose.Diagram;

// Simple console application that simulates a web API endpoint for geometry manipulation.
    // Usage: VisioGeometryApi.exe <inputVisioPath> <shapeId> <newPinX> <newPinY> <newWidth> <newHeight> <newAngleDeg> <outputVisioPath>
    // All numeric values are in inches (except angle which is in degrees).
    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate argument count
            if (args.Length != 8)
            {
                Console.WriteLine("Incorrect number of arguments.");
                Console.WriteLine("Expected: <inputVisioPath> <shapeId> <newPinX> <newPinY> <newWidth> <newHeight> <newAngleDeg> <outputVisioPath>");
                return;
            }

            // Parse arguments
            string inputPath = args[0];
            if (!long.TryParse(args[1], out long shapeId))
            {
                Console.WriteLine("Invalid shapeId.");
                return;
            }

            if (!double.TryParse(args[2], out double newPinX) ||
                !double.TryParse(args[3], out double newPinY) ||
                !double.TryParse(args[4], out double newWidth) ||
                !double.TryParse(args[5], out double newHeight) ||
                !double.TryParse(args[6], out double newAngleDeg))
            {
                Console.WriteLine("One or more numeric parameters are invalid.");
                return;
            }

            string outputPath = args[7];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (you can adapt to other pages if needed)
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                }

                // Modify geometry
                shape.XForm.PinX.Value = newPinX;               // Set new X position (center)
                shape.XForm.PinY.Value = newPinY;               // Set new Y position (center)
                shape.XForm.Width.Value = newWidth;             // Set new width
                shape.XForm.Height.Value = newHeight;           // Set new height

                // Angle is stored in radians; convert from degrees
                double angleRad = (Math.PI / 180.0) * newAngleDeg;
                shape.XForm.Angle.Value = angleRad;             // Set rotation

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape {shapeId} updated and diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Report any errors
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }