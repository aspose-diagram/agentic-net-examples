using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (adjust the file path as needed)
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the shape to locate (case‑insensitive)
            string targetName = "MyShape";

            // Variable to hold the found shape
            Shape foundShape = null;

            // Search through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Name != null && 
                        string.Equals(shape.Name, targetName, StringComparison.OrdinalIgnoreCase))
                    {
                        foundShape = shape;
                        break; // Exit inner loop once found
                    }
                }
                if (foundShape != null)
                    break; // Exit outer loop if shape is already found
            }

            // Report the result
            if (foundShape != null)
            {
                Console.WriteLine($"Shape found: ID = {foundShape.ID}, Name = {foundShape.Name}");
            }
            else
            {
                Console.WriteLine("Shape not found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
