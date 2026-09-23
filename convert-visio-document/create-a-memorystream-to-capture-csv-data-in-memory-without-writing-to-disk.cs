using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Create a MemoryStream to hold CSV data in memory
        using (var memoryStream = new MemoryStream())
        {
            // Write CSV content to the MemoryStream using a StreamWriter
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, leaveOpen: true))
            {
                writer.WriteLine("Name,Age,Country");
                writer.WriteLine("Alice,30,USA");
                writer.WriteLine("Bob,25,Canada");
                writer.WriteLine("Charlie,28,UK");
                writer.Flush(); // Ensure all data is flushed to the stream
            }

            // Reset the stream position to the beginning for reading or further processing
            memoryStream.Position = 0;

            // Example: read the CSV data back as a string (optional)
            using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
            {
                string csvContent = reader.ReadToEnd();
                Console.WriteLine(csvContent);
            }

            // At this point, memoryStream contains the CSV data and can be passed to other APIs without touching the file system
        }
    }
}
