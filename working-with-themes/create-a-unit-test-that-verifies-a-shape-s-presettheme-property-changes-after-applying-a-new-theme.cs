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

                // Add a rectangle shape to the active page
                // Parameters: PinX, PinY, master name
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Apply the first preset theme (Bubble)
                try
                {
                    shape.PresetTheme = PresetThemeValue.Bubble;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to apply first preset theme: " + ex.Message);
                }

                // Apply a different preset theme (Clouds)
                try
                {
                    shape.PresetTheme = PresetThemeValue.Clouds;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to apply second preset theme: " + ex.Message);
                }

                // If we reach this point, both theme assignments succeeded,
                // indicating that the shape's PresetTheme property can be changed.
                Console.WriteLine("Shape PresetTheme property changed successfully.");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }