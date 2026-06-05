using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Create a unique identifier based on the shape's ID
                    // The identifier is stored as a string literal in the event formula
                    string uniqueIdFormula = $"\"ID_{shape.ID}\"";

                    // Assign the unique identifier to an event cell.
                    // Since EventShapeAdded is not a standard event cell, we use EventDrop as an example.
                    shape.Event.EventDrop.Ufe.F = uniqueIdFormula;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
