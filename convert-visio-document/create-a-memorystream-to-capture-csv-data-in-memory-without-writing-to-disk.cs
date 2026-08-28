using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create a memory stream to capture CSV data
            using (MemoryStream csvStream = new MemoryStream())
            {
                // Save the diagram as CSV into the memory stream
                diagram.Save(csvStream, SaveFileFormat.Csv);

                // Rewind the stream to read its contents
                csvStream.Position = 0;

                // Example: read the CSV text from the memory stream
                using (StreamReader reader = new StreamReader(csvStream))
                {
                    string csvData = reader.ReadToEnd();
                    Console.WriteLine(csvData);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
