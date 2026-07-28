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

            // Retrieve the sub‑shape (replace with actual ID or name)
            // Example using shape ID 5:
            Shape subShape = diagram.Pages[0].Shapes.GetShape(5);
            // If you prefer to use the shape name, use:
            // Shape subShape = diagram.Pages[0].Shapes.GetShape("SubShapeName");

            // Obtain the parent shape of the sub‑shape
            Shape parentShape = subShape.ParentShape;

            // Display parent shape information
            if (parentShape != null)
            {
                Console.WriteLine($"Parent Shape ID: {parentShape.ID}, Name: {parentShape.Name}");
            }
            else
            {
                Console.WriteLine("The specified shape has no parent (it is a top‑level shape).");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
