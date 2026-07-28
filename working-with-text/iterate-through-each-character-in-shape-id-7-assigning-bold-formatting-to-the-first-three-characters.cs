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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the shape with ID 7 from the first page
            long shapeId = 7;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Ensure the shape and its character collection are valid
            if (shape != null && shape.Chars != null)
            {
                int index = 0;
                // Iterate through each character formatting entry
                foreach (Aspose.Diagram.Char ch in shape.Chars)
                {
                    // Apply bold style to the first three characters
                    if (index < 3)
                    {
                        ch.Style.Value = StyleValue.Bold;
                    }
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Shape with ID 7 not found or has no character data.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
