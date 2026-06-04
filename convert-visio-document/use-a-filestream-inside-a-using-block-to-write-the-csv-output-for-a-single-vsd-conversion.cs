using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD diagram from file
            Diagram diagram = new Diagram("input.vsd");

            // Open a FileStream in a using block for the CSV output
            using (FileStream csvStream = new FileStream("output.csv", FileMode.Create, FileAccess.Write))
            {
                // Save the diagram to the stream in CSV format
                diagram.Save(csvStream, SaveFileFormat.Csv);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
