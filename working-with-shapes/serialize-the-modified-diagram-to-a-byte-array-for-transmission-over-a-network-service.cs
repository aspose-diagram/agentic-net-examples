using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram from a file.
            // This uses the Diagram(string) constructor (create/load rule).
            Diagram diagram = new Diagram("input.vsdx");

            // TODO: Apply any modifications to the diagram here.
            // e.g., diagram.Pages[0].Shapes.Add(...);

            // Serialize the diagram to a byte array.
            // A MemoryStream is used as the target stream.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the diagram into the stream in VDX format.
                // This follows the Save(Stream, SaveFileFormat) rule.
                diagram.Save(memoryStream, SaveFileFormat.Vdx);

                // Convert the stream contents to a byte array for transmission.
                byte[] diagramBytes = memoryStream.ToArray();

                // diagramBytes now contains the serialized diagram data.
                // It can be sent over a network service as needed.
            }

            // Release resources held by the diagram.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
