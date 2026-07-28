using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Retrieve the shape you want to move.
            // Here we get the shape with ID 1; adjust the ID as needed.
            Shape shape = page.Shapes.GetShape(1);

            // Move the shape 50 units to the right (X) and 30 units down (Y).
            // The Move method shifts the shape by the specified offsets in inches.
            shape.Move(50.0, 30.0);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
