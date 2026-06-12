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
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (index 0) and select a shape by its ID.
            // Replace 1 with the actual shape ID you want to modify.
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Convert 2.5 centimeters to inches (Aspose.Diagram uses inches for SetHeight).
            double heightInInches = 2.5 * 0.393701; // 1 cm = 0.393701 inches

            // Set the new height of the shape.
            shape.SetHeight(heightInInches);

            // Save the modified diagram.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
