using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a gradient fill defined
                        if (shape.Fill != null && shape.Fill.GradientFill != null)
                        {
                            var gradientFill = shape.Fill.GradientFill;

                            // Iterate through each gradient stop
                            foreach (GradientStop stop in gradientFill.GradientStops)
                            {
                                // Position is stored as a DoubleValue (0..1)
                                double position = stop.Position.Value;

                                // Color is stored as a ColorValue (hex string)
                                string color = stop.Color.Value;

                                Console.WriteLine($"Shape ID {shape.ID}: Stop Position = {position}, Color = {color}");
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }