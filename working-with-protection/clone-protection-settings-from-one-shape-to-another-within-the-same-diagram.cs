using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the page that contains the shapes (using the first page as example)
            Page page = diagram.Pages[0];

            // Retrieve the source shape (the one whose protection settings we want to copy)
            // Replace 1 with the actual ID of the source shape
            Shape sourceShape = page.Shapes.GetShape(1);

            // Retrieve the target shape (the one that will receive the protection settings)
            // Replace 2 with the actual ID of the target shape
            Shape targetShape = page.Shapes.GetShape(2);

            // Clone protection settings from source to target.
            // Shape.Copy copies all shape properties, including the Protection flags.
            targetShape.Copy(sourceShape);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
