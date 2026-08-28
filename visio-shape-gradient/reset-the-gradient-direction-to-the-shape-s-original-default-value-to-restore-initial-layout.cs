using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses a gradient fill (FillPattern value 25)
                    if (shape.Fill != null && shape.Fill.FillPattern != null && shape.Fill.FillPattern.Value == 25)
                    {
                        // Ensure the gradient fill structure exists
                        if (shape.Fill.GradientFill != null && shape.Fill.GradientFill.GradientDir != null)
                        {
                            // Reset the gradient direction to the default value (0)
                            shape.Fill.GradientFill.GradientDir.Value = 0;
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
}
