using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify the circle shape.
                // Common ways: master name "Ellipse" (used for circles/ovals) or shape name containing "Circle".
                bool isCircle = false;

                if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                {
                    // Master name comparison (case‑insensitive)
                    if (shape.Master.Name.Equals("Ellipse", StringComparison.OrdinalIgnoreCase))
                        isCircle = true;
                }

                if (!isCircle && !string.IsNullOrEmpty(shape.NameU))
                {
                    // Fallback: check shape's universal name for the word "Circle"
                    if (shape.NameU.IndexOf("Circle", StringComparison.OrdinalIgnoreCase) >= 0)
                        isCircle = true;
                }

                // Bring the identified circle shape to the front of the Z‑order
                if (isCircle)
                    shape.BringToFront();
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
