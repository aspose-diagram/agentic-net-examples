using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Word document that will be linked/embedded as an OLE object
            string wordFilePath = "Sample.docx";

            // Verify the Word file exists before proceeding
            if (!File.Exists(wordFilePath))
            {
                throw new FileNotFoundException($"Word file not found: {wordFilePath}");
            }

            // Load the Word document bytes (embedding the file content)
            byte[] wordBytes = File.ReadAllBytes(wordFilePath);

            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Retrieve the first (default) page of the diagram
            Page page = diagram.Pages[0];

            // Add a placeholder shape for the OLE object using the built‑in "OLE Object" master
            // Parameters: PinX, PinY, Width, Height, master name
            long oleShapeId = page.AddShape(2.0, 2.0, 3.0, 2.0, "OLE Object");

            // Get the shape instance from the returned ID
            Shape oleShape = page.Shapes.GetShape(oleShapeId);

            // Mark the shape as a foreign (OLE) shape
            oleShape.Type = TypeValue.Foreign;

            // Ensure ForeignData is available
            if (oleShape.ForeignData == null)
            {
                throw new InvalidOperationException("ForeignData is null on the OLE shape.");
            }

            // Assign the Word document bytes to the OLE object's binary data
            oleShape.ForeignData.ObjectData = wordBytes;

            // Set the source name (used by Visio to identify the OLE type)
            oleShape.ForeignData.ObjectSourceFullName = Path.GetFileName(wordFilePath);

            // Specify that this is an embedded OLE object
            oleShape.ForeignData.ForeignType = ForeignType.Object;

            // Optionally display the OLE object as an icon
            oleShape.ForeignData.ShowAsIcon = BOOL.True;

            // Save the diagram containing the OLE object
            diagram.Save("DiagramWithOle.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
