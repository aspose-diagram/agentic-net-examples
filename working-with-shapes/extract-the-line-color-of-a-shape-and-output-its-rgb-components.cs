using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through pages and shapes to find the first shape.
                // Adjust the logic as needed to target a specific shape.
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        targetShape = shape;
                        break;
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape found in the diagram.");
                    return;
                }

                // Retrieve the line color value (hex string, e.g., "#FF0000").
                string lineColorHex = targetShape.Line.LineColor.Value;

                if (string.IsNullOrWhiteSpace(lineColorHex) || !lineColorHex.StartsWith("#") || lineColorHex.Length != 7)
                {
                    Console.WriteLine("Line color is not defined or not in expected hex format.");
                    return;
                }

                // Parse the hex string to obtain RGB components.
                int red   = Convert.ToInt32(lineColorHex.Substring(1, 2), 16);
                int green = Convert.ToInt32(lineColorHex.Substring(3, 2), 16);
                int blue  = Convert.ToInt32(lineColorHex.Substring(5, 2), 16);

                // Output the RGB components.
                Console.WriteLine($"Line Color Hex: {lineColorHex}");
                Console.WriteLine($"Red:   {red}");
                Console.WriteLine($"Green: {green}");
                Console.WriteLine($"Blue:  {blue}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }