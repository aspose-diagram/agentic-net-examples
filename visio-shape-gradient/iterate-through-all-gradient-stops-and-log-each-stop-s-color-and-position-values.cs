using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (the load rule will replace this line with the appropriate code)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a gradient fill
                    if (shape.Fill != null && shape.Fill.GradientFill != null)
                    {
                        GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;

                        // Iterate over each gradient stop
                        for (int i = 0; i < stops.Count; i++)
                        {
                            GradientStop stop = stops[i];

                            // Log the color and position of the stop
                            // Position is a DoubleValue; its numeric value is accessed via the Value property
                            // Color is a ColorValue; its string representation can be used directly
                            Console.WriteLine(
                                $"Shape ID {shape.ID}, Stop {i}: Color = {stop.Color}, Position = {stop.Position.Value}");
                        }
                    }
                }
            }

            // Save the diagram if any modifications were made (the save rule will replace this line)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
