using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Select the shape you want to modify (e.g., first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Convert 2.5 centimeters to inches (Aspose.Diagram uses inches for size)
            double heightCm = 2.5;
            double heightInches = heightCm * 0.393701; // 1 cm = 0.393701 inches

            // Set the height of the shape using the SetHeight method
            shape.SetHeight(heightInches);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
