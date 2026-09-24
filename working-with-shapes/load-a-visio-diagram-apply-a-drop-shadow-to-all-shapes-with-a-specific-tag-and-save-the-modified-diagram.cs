using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define the custom property (tag) name and value that indicate a shape should receive a drop shadow
                const string tagName = "ShadowTag";
                const string tagValue = "Apply";

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

                        // Check if the shape has the specified custom property (tag)
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == tagName && prop.Value.Val == tagValue)
                                {
                                    ApplyDropShadow(shape);
                                    break; // Tag found, no need to check other properties on this shape
                                }
                            }
                        }
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

        // Applies a simple drop shadow to the given shape
        private static void ApplyDropShadow(Shape shape)
        {
            // Enable simple shadow type
            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

            // Shadow color (black)
            shape.Fill.ShdwForegnd.Value = "#000000";

            // Shadow transparency (30% transparent)
            shape.Fill.ShdwForegndTrans.Value = 0.3;

            // Shadow offset (in inches)
            shape.Fill.ShapeShdwOffsetX.Value = 0.1;
            shape.Fill.ShapeShdwOffsetY.Value = 0.1;
        }
    }