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

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                throw new Exception("No shape found on the first page.");
            }

            // New line weight value (in inches)
            double newLineWeight = 0.05;

            // Set the line weight
            shape.Line.LineWeight.Value = newLineWeight;

            // Verify that the line weight was updated
            if (Math.Abs(shape.Line.LineWeight.Value - newLineWeight) > 0.0001)
            {
                throw new Exception($"Line weight verification failed. Expected {newLineWeight}, but got {shape.Line.LineWeight.Value}.");
            }
            else
            {
                Console.WriteLine($"Line weight successfully set to {shape.Line.LineWeight.Value} inches.");
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
