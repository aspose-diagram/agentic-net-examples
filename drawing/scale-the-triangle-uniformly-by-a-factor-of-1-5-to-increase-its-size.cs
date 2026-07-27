using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file containing the triangle
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                double scaleFactor = 1.5; // Uniform scaling factor

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify triangle shapes by their master name
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Scale width and height uniformly; PinX/PinY remain unchanged to keep the center position
                            shape.XForm.Width.Value *= scaleFactor;
                            shape.XForm.Height.Value *= scaleFactor;
                        }
                    }
                }

                // Save the updated diagram
                string outputPath = "output_scaled.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
