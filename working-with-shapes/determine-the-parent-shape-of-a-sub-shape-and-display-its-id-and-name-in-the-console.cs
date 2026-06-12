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
            // Replace the path with the actual file location
            var diagram = new Diagram("input.vsdx");

            // Retrieve the sub‑shape.
            // You can get the shape by its name or ID.
            // Example using the shape name "SubShapeName":
            Shape subShape = diagram.Pages[0].Shapes.GetShape("SubShapeName");

            // If the shape is not found, GetShape returns null.
            if (subShape == null)
            {
                Console.WriteLine("Sub‑shape not found.");
                return;
            }

            // Obtain the parent shape of the sub‑shape.
            Shape parentShape = subShape.ParentShape;

            // Check whether the shape has a parent (it may be a top‑level shape).
            if (parentShape != null)
            {
                Console.WriteLine($"Parent Shape ID: {parentShape.ID}");
                Console.WriteLine($"Parent Shape Name: {parentShape.Name}");
            }
            else
            {
                Console.WriteLine("The specified shape does not have a parent (it is a top‑level shape).");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
