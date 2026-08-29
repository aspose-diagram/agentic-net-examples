using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Identifier of the parent shape (could be name or ID)
            int parentShapeId = 1; // replace with actual ID or use GetShapeIncludingChild(string)

            // Retrieve the parent shape including its child shapes
            Shape parentShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(parentShapeId);

            // Collect IDs of all direct child shapes
            List<long> childShapeIds = new List<long>();
            foreach (Shape child in parentShape.Shapes)
            {
                childShapeIds.Add(child.ID);
            }

            // Example usage: output the collected IDs
            foreach (long id in childShapeIds)
            {
                Console.WriteLine(id);
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
