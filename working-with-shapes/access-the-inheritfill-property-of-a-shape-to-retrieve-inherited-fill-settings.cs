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

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Access the inherited fill settings of the shape
            Fill inheritFill = shape.InheritFill;

            // Output some inherited fill properties
            Console.WriteLine("Inherited Fill Pattern: " + inheritFill.FillPattern);
            Console.WriteLine("Inherited Foreground Color: " + inheritFill.FillForegnd);
            Console.WriteLine("Inherited Background Color: " + inheritFill.FillBkgnd);
            Console.WriteLine("Inherited Foreground Transparency: " + inheritFill.FillForegndTrans);
            Console.WriteLine("Inherited Background Transparency: " + inheritFill.FillBkgndTrans);

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
