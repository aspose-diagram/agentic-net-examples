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

            // Identify the sub‑shape (by ID or name). Here we use an example ID = 5.
            // You can also use GetShapeIncludingChild(string) if you know the shape name.
            Shape subShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(5);

            // Retrieve the parent shape of the sub‑shape.
            Shape parentShape = subShape.ParentShape;

            // Display the parent shape's ID and name.
            if (parentShape != null)
            {
                Console.WriteLine($"Parent Shape ID: {parentShape.ID}");
                Console.WriteLine($"Parent Shape Name: {parentShape.Name}");
            }
            else
            {
                Console.WriteLine("The specified shape does not have a parent shape.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
