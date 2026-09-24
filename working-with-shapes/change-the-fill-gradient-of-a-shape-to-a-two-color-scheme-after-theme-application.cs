using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Del == BOOL.False)
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found.");
                    return;
                }

                // Apply a two‑color gradient fill
                // Set fill pattern to gradient (value 25)
                targetShape.Fill.FillPattern.Value = 25;

                // Enable gradient fill
                targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction (0 = horizontal, adjust as needed)
                targetShape.Fill.GradientFill.GradientDir.Value = 0;

                // Clear any existing gradient stops
                targetShape.Fill.GradientFill.GradientStops.Clear();

                // Add first gradient stop (position 0, red color)
                targetShape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                // Add second gradient stop (position 1, green color)
                targetShape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Gradient fill applied and diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }