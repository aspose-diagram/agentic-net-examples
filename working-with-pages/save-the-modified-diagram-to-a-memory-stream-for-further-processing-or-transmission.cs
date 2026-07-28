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

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // -----------------------------------------------------------------
            // Perform any required modifications to the diagram here.
            // For example, you could add shapes, change properties, etc.
            // -----------------------------------------------------------------

            // Save the modified diagram to a memory stream in VDX format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, SaveFileFormat.Vdx);

                // Reset the stream position if you need to read from it later
                memoryStream.Position = 0;

                // The memoryStream now contains the diagram data and can be
                // transmitted, stored, or processed further as needed.
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
