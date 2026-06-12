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

            // Paths to input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve LocPinX and LocPinY values
                    double locPinX = shape.XForm.LocPinX.Value;
                    double locPinY = shape.XForm.LocPinY.Value;

                    // Validate that the values are numeric
                    if (double.IsNaN(locPinX) || double.IsInfinity(locPinX))
                        throw new Exception($"LocPinX is not a valid number for shape ID {shape.ID}");

                    if (double.IsNaN(locPinY) || double.IsInfinity(locPinY))
                        throw new Exception($"LocPinY is not a valid number for shape ID {shape.ID}");

                    // Perform absolute pin calculations
                    shape.XForm.PinX.Value = shape.XForm.PinX.Value + locPinX;
                    shape.XForm.PinY.Value = shape.XForm.PinY.Value + locPinY;
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
