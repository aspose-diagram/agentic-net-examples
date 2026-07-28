using System;
using System.IO;
using Aspose.Diagram;

class OleDataValidator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    ForeignData foreignData = shape.ForeignData;
                    if (foreignData != null)
                    {
                        bool hasData = false;

                        // For embedded OLE objects, ObjectData holds the binary content
                        if (foreignData.ObjectData != null && foreignData.ObjectData.Length > 0)
                        {
                            hasData = true;
                        }

                        // For linked OLE objects, ObjectSourceFullName holds the source file path
                        // Consider it non‑empty if the path string is not null or whitespace
                        if (!hasData && !string.IsNullOrWhiteSpace(foreignData.ObjectSourceFullName))
                        {
                            hasData = true;
                        }

                        // If no data was found, log a warning with shape identification details
                        if (!hasData)
                        {
                            Console.WriteLine(
                                $"Warning: OLE object in shape ID {shape.ID} on page \"{page.Name}\" is empty.");
                        }
                    }
                }
            }

            // Save the diagram (optional – here we just save a copy)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
