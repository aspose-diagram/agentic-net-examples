using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the diagram from a file (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape (using shape ID 1 as an example)
                Shape shape = page.Shapes.GetShape(1);

                // Get the gradient fill of the shape
                GradientFill gradientFill = shape.Fill.GradientFill;

                // Check if gradient fill is enabled
                if (gradientFill.GradientEnabled.Value == BOOL.True)
                {
                    // Locate the gradient stop at index 0
                    GradientStop firstStop = null;
                    int currentIndex = 0;
                    foreach (GradientStop stop in gradientFill.GradientStops)
                    {
                        if (currentIndex == 0)
                        {
                            firstStop = stop;
                            break;
                        }
                        currentIndex++;
                    }

                    if (firstStop != null)
                    {
                        // Read the color value (hex string) of the first gradient stop
                        string colorHex = firstStop.Color.Value;
                        Console.WriteLine($"First gradient stop color: {colorHex}");
                    }
                    else
                    {
                        Console.WriteLine("No gradient stop found at index 0.");
                    }
                }
                else
                {
                    Console.WriteLine("Gradient fill is not enabled for this shape.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }