using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Threshold height (in inches) and the height to set when below the threshold
            double thresholdHeight = 2.0;
            double newHeight = 3.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Current height of the shape (in drawing units, typically inches)
                    double currentHeight = shape.XForm.Height.Value;

                    // Apply new height only if the current height is less than the threshold
                    if (currentHeight < thresholdHeight)
                    {
                        shape.SetHeight(newHeight);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
