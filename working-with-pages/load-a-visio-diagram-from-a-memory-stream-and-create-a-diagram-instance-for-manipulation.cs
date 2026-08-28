using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file bytes into a memory stream (replace with your source of bytes)
            byte[] visioBytes = File.ReadAllBytes("input.vsdx");
            using (MemoryStream inputStream = new MemoryStream(visioBytes))
            {
                // Create a Diagram instance from the memory stream
                Diagram diagram = new Diagram(inputStream);

                // Example manipulation: output the number of pages in the diagram
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");

                // (Optional) Save the diagram back to another memory stream in the same format
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    // outputStream now contains the saved Visio diagram
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
