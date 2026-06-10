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

            // Load an existing diagram (replace the path with your actual file)
            Diagram diagram = new Diagram("input.vsdx");

            // ----- Perform any modifications to the diagram here -----
            // Example: (placeholder) diagram.ActivePage.Name = "ModifiedPage";

            // Save the diagram into a memory stream in VDX format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, SaveFileFormat.Vdx);
                // Reset the stream position if it will be read later
                memoryStream.Position = 0;

                // Example usage of the memory stream (e.g., display its size)
                Console.WriteLine($"Diagram saved to memory stream. Size = {memoryStream.Length} bytes");
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
