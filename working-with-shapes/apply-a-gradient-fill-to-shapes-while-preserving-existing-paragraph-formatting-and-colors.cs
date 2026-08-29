using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Preserve existing fill colors (if any)
                        string foreColor = shape.Fill.FillForegnd.Value;
                        string backColor = shape.Fill.FillBkgnd.Value;

                        // Apply gradient fill
                        shape.Fill.FillPattern.Value = 25; // Gradient pattern
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                        shape.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right direction

                        // Clear any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add start color stop (position 0)
                        if (!string.IsNullOrWhiteSpace(foreColor))
                        {
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue(foreColor, MeasureConst.Undefined));
                        }
                        else
                        {
                            // Fallback to white if no foreground color is set
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue("#FFFFFF", MeasureConst.Undefined));
                        }

                        // Add end color stop (position 1)
                        if (!string.IsNullOrWhiteSpace(backColor))
                        {
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue(backColor, MeasureConst.Undefined));
                        }
                        else
                        {
                            // Fallback to black if no background color is set
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue("#000000", MeasureConst.Undefined));
                        }

                        // Paragraph formatting (Paras) is left untouched to preserve existing formatting
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }