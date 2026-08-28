using System;
using System.IO;
using Aspose.Diagram;

class OleDataValidator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    ForeignData foreign = shape.ForeignData;
                    if (foreign == null)
                        continue; // No foreign data, skip

                    // Determine if the foreign data represents an OLE object
                    // Typically, OLE objects have a non‑zero ObjectType when ForeignType is "Object"
                    bool isOleObject = foreign.ObjectType != ObjectType.Undefined && foreign.ObjectType != 0;
                    if (!isOleObject)
                        continue; // Not an OLE object, skip

                    // Evaluate embedded OLE data
                    bool hasEmbeddedData = foreign.ObjectData != null && foreign.ObjectData.Length > 0;

                    // Evaluate linked OLE data (source file name)
                    bool hasLinkedData = !string.IsNullOrWhiteSpace(foreign.ObjectSourceFullName);

                    // If both embedded and linked data are missing/empty, log a warning
                    if (!hasEmbeddedData && !hasLinkedData)
                    {
                        Console.WriteLine(
                            $"Warning: OLE object in shape ID {shape.ID} on page \"{page.Name}\" contains no data.");
                    }
                }
            }

            // Save the diagram (if any modifications were made; here we just preserve the original)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
