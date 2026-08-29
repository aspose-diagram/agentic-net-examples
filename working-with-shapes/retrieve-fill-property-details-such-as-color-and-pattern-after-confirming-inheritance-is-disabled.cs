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

            // Access a specific shape; here we use the first shape on the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Determine whether the shape inherits fill formatting from a style/master
            // If FillStyle is null, inheritance is disabled and the shape has its own Fill values
            bool inheritanceDisabled = shape.FillStyle == null;

            if (inheritanceDisabled)
            {
                // Retrieve the shape's own fill properties
                Fill fill = shape.Fill;

                // Output fill pattern and colors
                Console.WriteLine($"Fill Pattern   : {fill.FillPattern}");
                Console.WriteLine($"Foreground Color: {fill.FillForegnd}");
                Console.WriteLine($"Background Color: {fill.FillBkgnd}");
            }
            else
            {
                Console.WriteLine("Inheritance is enabled; fill values are inherited from the style/master.");
            }

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
