using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Access the first page in the diagram
                    Page page = diagram.Pages[0];

                    // Retrieve the first shape on the page (if any)
                    Shape shape = null;
                    foreach (Shape s in page.Shapes)
                    {
                        shape = s;
                        break;
                    }

                    if (shape == null)
                    {
                        Console.WriteLine("No shapes found on the first page.");
                        return;
                    }

                    // Read the gradient angle (in degrees) from the shape's fill settings
                    // GradientAngle is a cell; its numeric value is accessed via .Value
                    double gradientAngle = shape.Fill.GradientFill.GradientAngle.Value;

                    Console.WriteLine($"Gradient angle of shape ID {shape.ID} is {gradientAngle} degrees.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }