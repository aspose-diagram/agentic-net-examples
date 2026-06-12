using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (must contain a master named "GradientMaster")
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("Diagram contains no pages.");

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the master that will provide the gradient fill
                string masterName = "GradientMaster";
                Master master = null;
                foreach (Master m in diagram.Masters)
                {
                    if (m.Name == masterName || m.NameU == masterName)
                    {
                        master = m;
                        break;
                    }
                }

                if (master == null)
                    throw new Exception($"Master '{masterName}' not found in the diagram.");

                // Apply a gradient fill to the master
                // Fill pattern 25 corresponds to gradient fill
                master.Shapes[0].Fill.FillPattern.Value = 25;
                master.Shapes[0].Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                master.Shapes[0].Fill.GradientFill.GradientDir.Value = 0; // Left to Right

                // Clear any existing gradient stops and add two stops (blue to green)
                master.Shapes[0].Fill.GradientFill.GradientStops.Clear();
                master.Shapes[0].Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined));
                master.Shapes[0].Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Add a shape that uses the master (inherits the gradient)
                double pinX = 2.0;
                double pinY = 2.0;
                long shapeId = page.AddShape(pinX, pinY, masterName);

                // Retrieve the newly added shape
                Shape shape = page.Shapes.GetShape(shapeId);

                // Validate that the shape inherits the gradient fill from the master
                bool isGradientPattern = shape.Fill.FillPattern.Value == 25;
                bool isGradientEnabled = shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True;
                bool hasTwoStops = shape.Fill.GradientFill.GradientStops.Count == 2;

                // Validate first stop color
                GradientStop firstStop = shape.Fill.GradientFill.GradientStops[0];
                bool firstStopIsBlue = firstStop.Color.Value == "#0000FF";

                // Validate second stop color
                GradientStop secondStop = shape.Fill.GradientFill.GradientStops[1];
                bool secondStopIsGreen = secondStop.Color.Value == "#00FF00";

                if (!isGradientPattern || !isGradientEnabled || !hasTwoStops ||
                    !firstStopIsBlue || !secondStopIsGreen)
                {
                    throw new Exception("Shape does not correctly inherit the master's gradient fill.");
                }

                Console.WriteLine("Gradient inheritance validation passed.");

                // Export the diagram to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                diagram.Save("output.pdf", pdfOptions);

                Console.WriteLine("Diagram exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }