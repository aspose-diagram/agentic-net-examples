using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape whose master information we want.
            // Here we assume the shape ID is known; replace with the actual ID as needed.
            long shapeId = 1;

            // Access the first page (or any specific page) and retrieve the shape.
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);

            // Obtain the master associated with the shape.
            Master master = shape.Master;

            // Log the master name if it exists.
            if (master != null)
            {
                Console.WriteLine("Master Name: " + master.Name);
            }
            else
            {
                Console.WriteLine("The selected shape does not have an associated master.");
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
