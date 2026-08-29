using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify side‑label shapes.
                    // Adjust the condition according to how side‑labels are named in your diagram.
                    // Here we assume the shape's NameU contains the word "SideLabel".
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("SideLabel"))
                    {
                        // Set the text rotation angle to 270 degrees.
                        // TxtAngle is a DoubleValue; assign the numeric value to its Value property.
                        shape.TextXForm.TxtAngle.Value = 270;
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Verification: output the TxtAngle of each side‑label shape to the console
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("SideLabel"))
                    {
                        double angle = shape.TextXForm.TxtAngle.Value;
                        Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) TxtAngle = {angle}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
