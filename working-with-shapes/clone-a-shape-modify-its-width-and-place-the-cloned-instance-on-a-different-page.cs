using System.IO;
using System;
using Aspose.Diagram;

class CloneShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Source page (first page) and shape to clone (first shape on the page)
            Page sourcePage = diagram.Pages[0];
            Shape originalShape = sourcePage.Shapes[0];

            // Create a new shape instance and copy the properties of the original shape
            Shape clonedShape = new Shape();
            clonedShape.Copy(originalShape);

            // Modify the width of the cloned shape (e.g., set to 2 inches)
            clonedShape.SetWidth(2.0);

            // Target page (second page) where the cloned shape will be placed
            Page targetPage = diagram.Pages[1];

            // Add the cloned shape to the target page using the same master as the original shape
            // The AddShape method on Page adds the shape to that specific page.
            targetPage.AddShape(clonedShape, originalShape.Master.NameU);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
