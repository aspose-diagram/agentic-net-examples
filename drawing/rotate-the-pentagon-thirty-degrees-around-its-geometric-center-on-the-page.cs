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

            // Load the Visio diagram (create/load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through pages and shapes to locate the pentagon
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Assuming the shape's universal name is "Pentagon"
                    if (shape.NameU == "Pentagon")
                    {
                        // Convert 30 degrees to radians (SetAngle expects radians)
                        double angleRad = Math.PI / 6.0; // 30° = π/6 rad

                        // Rotate the shape around its geometric center (pin)
                        shape.SetAngle(angleRad);
                    }
                }
            }

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
