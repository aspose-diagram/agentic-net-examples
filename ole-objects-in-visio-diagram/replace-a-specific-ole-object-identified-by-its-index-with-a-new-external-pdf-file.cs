using System.IO;
using System;
using Aspose.Diagram;

class ReplaceOleObject
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the page and shape (OLE object) index to replace
            int pageIndex = 0;   // first page
            int shapeIndex = 2;  // zero‑based index of the OLE shape on the page

            // Get the target shape
            Page page = diagram.Pages[pageIndex];
            Shape oleShape = page.Shapes[shapeIndex];

            // Ensure the shape contains foreign data (OLE object)
            if (oleShape.ForeignData != null)
            {
                // Set the source file name of the linked PDF
                oleShape.ForeignData.ObjectSourceFullName = @"C:\Path\To\NewFile.pdf";

                // Mark the object as a linked OLE object
                oleShape.ForeignData.ObjectType = ObjectType.LinkedObject;
            }
            else
            {
                Console.WriteLine("The specified shape does not contain OLE foreign data.");
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
