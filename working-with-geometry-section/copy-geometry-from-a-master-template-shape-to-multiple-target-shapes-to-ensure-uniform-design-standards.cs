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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // -----------------------------------------------------------------
            // 1. Retrieve the master that contains the template geometry.
            //    Assume the master is identified by its ID (replace with actual ID or name).
            // -----------------------------------------------------------------
            int masterId = 1;                     // TODO: set the correct master ID
            Master master = diagram.Masters[masterId];

            // The master usually contains a single shape that defines the geometry.
            // If there are multiple shapes, select the appropriate one (e.g., by name).
            Shape templateShape = master.Shapes[0];

            // -----------------------------------------------------------------
            // 2. Define the target shapes that should receive the template geometry.
            //    Here we use a list of shape IDs on the first page as an example.
            // -----------------------------------------------------------------
            Page page = diagram.Pages[0];
            List<long> targetShapeIds = new List<long> { 5, 7, 9 }; // TODO: replace with actual IDs

            // -----------------------------------------------------------------
            // 3. Copy the geometry (and related properties) from the template shape
            //    to each target shape using Shape.Copy.
            // -----------------------------------------------------------------
            foreach (long shapeId in targetShapeIds)
            {
                Shape targetShape = page.Shapes.GetShape(shapeId);
                targetShape.Copy(templateShape);
            }

            // -----------------------------------------------------------------
            // 4. Save the modified diagram.
            // -----------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
