using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioFile = @"C:\Diagrams\sample.vsdx";

            // Load the diagram using the built‑in constructor (lifecycle rule)
            Diagram diagram = new Diagram(visioFile);

            // Identifier of the shape we want to locate
            long shapeId = 5; // replace with the actual ID

            // Get the first page (or use diagram.ActivePage)
            Page page = diagram.Pages[0];

            // Locate the shape by its ID using the ShapeCollection.GetShape method
            Shape targetShape = page.Shapes.GetShape(shapeId);

            // Example processing: output shape name and text
            Console.WriteLine($"Shape ID: {targetShape.ID}");
            Console.WriteLine($"Shape Name: {targetShape.Name}");
            Console.WriteLine($"Shape Text: {targetShape.Text}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
