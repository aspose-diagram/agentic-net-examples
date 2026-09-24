using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file stream
            using (FileStream stream = new FileStream("input.vsdx", FileMode.Open))
            {
                Diagram diagram = new Diagram(stream);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify side‑label shapes by their master name
                        if (shape.Master != null && shape.Master.Name == "SideLabel")
                        {
                            // Set text rotation to 270 degrees (convert to radians)
                            double radians = (Math.PI / 180.0) * 270.0;
                            shape.TextXForm.TxtAngle.Value = radians;

                            // Verify the rotation was applied correctly
                            double actualDegrees = shape.TextXForm.TxtAngle.Value * 180.0 / Math.PI;
                            if (Math.Abs(actualDegrees - 270.0) > 0.1)
                            {
                                throw new Exception($"Shape ID {shape.ID} rotation verification failed. Expected 270°, got {actualDegrees}°.");
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID} rotation set to 270° successfully.");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
