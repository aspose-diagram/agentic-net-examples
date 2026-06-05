using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null)
                    {
                        var foreign = shape.ForeignData;
                        bool isEmpty = false;

                        // Embedded OLE object: ObjectData should contain bytes
                        if (foreign.ObjectData != null && foreign.ObjectData.Length > 0)
                        {
                            // Data present – nothing to do
                        }
                        // Linked OLE object: ObjectSourceFullName should be non‑empty
                        else if (!string.IsNullOrEmpty(foreign.ObjectSourceFullName))
                        {
                            // Source name present – assume data is valid
                        }
                        else
                        {
                            // Neither embedded data nor linked source – treat as empty
                            isEmpty = true;
                        }

                        if (isEmpty)
                        {
                            Console.WriteLine(
                                $"Warning: OLE object in shape ID {shape.ID} on page ID {page.ID} is empty.");
                        }
                    }
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
