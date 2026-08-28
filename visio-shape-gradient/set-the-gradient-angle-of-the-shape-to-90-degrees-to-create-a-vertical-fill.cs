using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Apply a vertical gradient fill to each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Set the fill pattern to gradient (value 25)
                shape.Fill.FillPattern.Value = 25;

                // Enable the gradient fill
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set the gradient angle to 90 degrees (vertical fill)
                shape.Fill.GradientFill.GradientAngle.Value = 90;
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
