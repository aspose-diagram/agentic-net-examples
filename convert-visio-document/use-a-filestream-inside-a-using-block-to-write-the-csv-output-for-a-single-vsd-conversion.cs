using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD file into a Diagram object
            Diagram diagram = new Diagram("input.vsd");

            // Open a FileStream for the CSV output inside a using block
            using (FileStream csvStream = new FileStream("output.csv", FileMode.Create, FileAccess.Write))
            {
                // Save the diagram data as CSV to the stream
                diagram.Save(csvStream, SaveFileFormat.Csv);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
