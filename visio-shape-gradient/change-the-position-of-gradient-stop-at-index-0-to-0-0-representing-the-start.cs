using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                bool modified = false;

                // Iterate over shapes on the first page
                if (diagram.Pages.Count > 0)
                {
                    var page = diagram.Pages[0];

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a gradient fill with stops
                        if (shape.Fill != null &&
                            shape.Fill.GradientFill != null &&
                            shape.Fill.GradientFill.GradientStops != null)
                        {
                            // Find the first gradient stop (index 0)
                            GradientStop firstStop = null;
                            int index = 0;
                            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                            {
                                if (index == 0)
                                {
                                    firstStop = stop;
                                    break;
                                }
                                index++;
                            }

                            if (firstStop != null)
                            {
                                // Change the position of the gradient stop at index 0 to 0.0 (start)
                                firstStop.Position.Value = 0.0;
                                modified = true;
                                Console.WriteLine($"Modified gradient stop position for shape ID {shape.ID}.");
                            }

                            // Only modify the first applicable shape
                            break;
                        }
                    }
                }

                if (!modified)
                {
                    Console.WriteLine("No shape with gradient stops was found to modify.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}