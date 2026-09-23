using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify rectangle shapes by their master name
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Retrieve original dimensions
                        double originalWidth = shape.XForm.Width.Value;
                        double originalHeight = shape.XForm.Height.Value;

                        // Scale width and height proportionally (double size)
                        shape.XForm.Width.Value = originalWidth * 2;
                        shape.XForm.Height.Value = originalHeight * 2;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
