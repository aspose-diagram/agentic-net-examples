using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify a circle/ellipse shape.
                        // In Visio, circles are typically created using the "Ellipse" master.
                        // Adjust the condition as needed for your specific diagram.
                        if (shape.Master != null && shape.Master.Name == "Ellipse")
                        {
                            // The Aspose.Diagram API does not expose a LineJoin property.
                            // To achieve sharper corners on a shape's outline, we can set the rounding to zero.
                            // This removes any corner rounding that might be applied.
                            shape.Line.Rounding.Value = 0.0;

                            // Optionally, you can also set the line pattern or weight if desired.
                            // shape.Line.LinePattern.Value = LinePatternValue.Solid;
                            // shape.Line.LineWeight.Value = 0.02; // thickness in inches

                            Console.WriteLine($"Adjusted line rounding for shape ID {shape.ID} on page '{page.Name}'.");
                        }
                    }
                }

                // Save the modified diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }