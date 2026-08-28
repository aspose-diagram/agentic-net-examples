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

            // Load a Visio file into a byte array (could be from any source, e.g., database, network)
            byte[] diagramBytes = File.ReadAllBytes("input.vsdx");

            // Create a memory stream from the byte array
            using (MemoryStream inputStream = new MemoryStream(diagramBytes))
            {
                // Use the Diagram constructor that accepts a Stream to load the diagram from memory
                Diagram diagram = new Diagram(inputStream);

                // Perform any required operations on the diagram here
                // ...

                // Save the diagram back to a memory stream in the same format (VSDX)
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);

                    // Optionally write the result to a file
                    File.WriteAllBytes("output.vsdx", outputStream.ToArray());
                }

                // Clean up the Diagram object
                diagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
