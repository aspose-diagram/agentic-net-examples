using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create a memory stream to hold the CSV output
            using (MemoryStream csvStream = new MemoryStream())
            {
                // Save the diagram as CSV directly into the memory stream
                diagram.Save(csvStream, SaveFileFormat.Csv);

                // Reset the stream position to the beginning for reading
                csvStream.Position = 0;

                // Example: read the CSV data as a string (optional)
                using (StreamReader reader = new StreamReader(csvStream))
                {
                    string csvContent = reader.ReadToEnd();
                    Console.WriteLine(csvContent);
                }

                // At this point, csvStream contains the CSV data in memory
                // It can be returned, sent over a network, etc., without touching the file system
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
