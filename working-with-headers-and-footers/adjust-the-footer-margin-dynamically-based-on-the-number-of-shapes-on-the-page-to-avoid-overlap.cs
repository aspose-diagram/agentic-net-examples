using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum number of shapes on any page
            int maxShapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                int shapeCount = page.Shapes.Count;
                if (shapeCount > maxShapeCount)
                    maxShapeCount = shapeCount;
            }

            // Base footer margin (in inches)
            double baseMargin = 0.5;

            // Increase margin if the diagram is dense (more than 20 shapes on a page)
            double additionalMargin = maxShapeCount > 20 ? 0.5 : 0.0;

            // Apply the calculated footer margin globally
            diagram.HeaderFooter.FooterMargin.Value = baseMargin + additionalMargin;

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
