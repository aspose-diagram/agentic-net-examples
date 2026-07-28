using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Example: load Visio file bytes into a memory stream.
            // Replace the path with your actual file or obtain the byte array from another source.
            byte[] visioBytes = File.ReadAllBytes("input.vsdx");

            using (MemoryStream inputStream = new MemoryStream(visioBytes))
            {
                // Load the diagram from the memory stream using the Diagram(Stream) constructor.
                Diagram diagram = new Diagram(inputStream);

                // Example manipulation: output the number of pages in the diagram.
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");

                // (Optional) Save the diagram back to another memory stream in the same format.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    // The outputStream now contains the saved Visio diagram.
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
