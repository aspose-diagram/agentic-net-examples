using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with the actual load rule if defined elsewhere)
            Diagram diagram = new Diagram("input.vsdx");

            // IDs of the shapes involved – adjust as needed
            long sourceShapeId = 1;   // Shape that already has the hyperlink
            long targetShapeId = 2;   // Shape to receive the cloned hyperlink

            // Retrieve the source shape
            Shape sourceShape = diagram.Pages[0].Shapes.GetShape(sourceShapeId);

            // Ensure the source shape contains at least one hyperlink
            if (sourceShape.Hyperlinks.Count > 0)
            {
                // Get the first hyperlink from the source shape
                Hyperlink originalLink = sourceShape.Hyperlinks[0];

                // Clone the hyperlink (deep copy)
                Hyperlink clonedLink = (Hyperlink)originalLink.Clone();

                // Modify the description – Aspose.Diagram exposes the description via the Name property
                clonedLink.Name = "Modified description";

                // Retrieve the target shape
                Shape targetShape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

                // Attach the cloned (and modified) hyperlink to the target shape
                targetShape.Hyperlinks.Add(clonedLink);
            }

            // Save the updated diagram (replace with the actual save rule if defined elsewhere)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
