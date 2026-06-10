using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

public class Program
    {
        public static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add two shapes with the same initial position
                // Using the default page (index 0) and the built‑in "Rectangle" master
                long shapeId1 = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                long shapeId2 = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

                // Retrieve the shape objects
                Shape shape1 = diagram.Pages[0].Shapes.GetShape(shapeId1);
                Shape shape2 = diagram.Pages[0].Shapes.GetShape(shapeId2);

                // Store original coordinates for later comparison
                double originalPinX1 = shape1.XForm.PinX.Value;
                double originalPinY1 = shape1.XForm.PinY.Value;
                double originalPinX2 = shape2.XForm.PinX.Value;
                double originalPinY2 = shape2.XForm.PinY.Value;

                // Configure auto‑spacing options: 2 inches horizontal and vertical distance
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0,
                    DistanceInVertical = 2.0
                };

                // Apply auto‑spacing to all shapes on the first page
                diagram.Pages[0].AutoSpaceShapes(diagram.Pages[0].Shapes, options);

                // Retrieve positions after auto‑spacing
                double newPinX1 = shape1.XForm.PinX.Value;
                double newPinY1 = shape1.XForm.PinY.Value;
                double newPinX2 = shape2.XForm.PinX.Value;
                double newPinY2 = shape2.XForm.PinY.Value;

                // Tolerance for floating‑point comparison
                const double epsilon = 1e-4;

                // Verify that the first shape has not moved (single shape should stay unchanged)
                if (Math.Abs(newPinX1 - originalPinX1) > epsilon || Math.Abs(newPinY1 - originalPinY1) > epsilon)
                {
                    throw new Exception($"Shape 1 position changed after auto‑spacing. Expected ({originalPinX1}, {originalPinY1}), got ({newPinX1}, {newPinY1}).");
                }
                else
                {
                    Console.WriteLine("Shape 1 position unchanged as expected.");
                }

                // Verify that the two shapes are spaced at least the requested distance apart
                double deltaX = Math.Abs(newPinX2 - newPinX1);
                double deltaY = Math.Abs(newPinY2 - newPinY1);

                if (deltaX + epsilon < options.DistanceInHorizontal || deltaY + epsilon < options.DistanceInVertical)
                {
                    throw new Exception($"Shapes are not spaced correctly after auto‑spacing. DistanceX={deltaX}, DistanceY={deltaY}, required >= {options.DistanceInHorizontal} and {options.DistanceInVertical}.");
                }
                else
                {
                    Console.WriteLine($"Shapes spaced correctly: DistanceX={deltaX}, DistanceY={deltaY}.");
                }

                // Optional: Save the diagram to verify visually (not required for the test)
                // diagram.Save("AutoSpaceResult.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }