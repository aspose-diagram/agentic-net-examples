using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Read the diagram file into a byte array (could be from any source, e.g., network, database)
            byte[] diagramBytes = File.ReadAllBytes("input.vsdx");

            // Wrap the byte array in a MemoryStream
            using (MemoryStream memoryStream = new MemoryStream(diagramBytes))
            {
                // Load the diagram from the memory stream using the Diagram constructor overload that accepts a Stream
                Diagram diagram = new Diagram(memoryStream);

                // (Optional) Manipulate the diagram here

                // Save the diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
