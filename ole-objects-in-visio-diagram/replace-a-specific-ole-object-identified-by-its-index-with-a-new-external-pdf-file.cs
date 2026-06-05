using System.IO;
using System;
using Aspose.Diagram;

class ReplaceOleObject
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the shape that contains the OLE object to replace (0‑based)
            int oleShapeIndex = 2; // example index, replace with actual index

            // Retrieve the shape from the first page (adjust page index if needed)
            Shape oleShape = diagram.Pages[0].Shapes[oleShapeIndex];

            // Ensure the shape has foreign data (OLE object)
            if (oleShape.ForeignData != null)
            {
                // Set the source file path of the linked PDF file
                oleShape.ForeignData.ObjectSourceFullName = @"C:\Path\To\NewFile.pdf";

                // Optionally specify that the object is a linked object
                oleShape.ForeignData.ObjectType = ObjectType.LinkedObject;
            }
            else
            {
                Console.WriteLine("The specified shape does not contain an OLE object.");
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
