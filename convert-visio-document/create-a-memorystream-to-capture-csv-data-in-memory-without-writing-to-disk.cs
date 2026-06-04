using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (replace with your source file or stream)
            Diagram diagram = new Diagram("input.vsdx");

            // Create a memory stream to hold the CSV output
            using (MemoryStream csvStream = new MemoryStream())
            {
                // Save the diagram as CSV directly into the memory stream
                diagram.Save(csvStream, SaveFileFormat.Csv);

                // Reset the stream position to the beginning for reading
                csvStream.Position = 0;

                // Example: read the CSV data as a string (optional)
                string csvContent = new StreamReader(csvStream).ReadToEnd();
                Console.WriteLine(csvContent);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
