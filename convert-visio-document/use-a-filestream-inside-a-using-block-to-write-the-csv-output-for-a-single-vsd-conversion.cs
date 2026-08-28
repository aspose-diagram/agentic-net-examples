using System;
using System.IO;
using Aspose.Diagram;

class VsdToCsvConverter
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputVsdPath = "input.vsd";

            // Path to the destination CSV file
            string outputCsvPath = "output.csv";

            // Load the VSD diagram
            Diagram diagram = new Diagram(inputVsdPath);

            // Write the CSV output using a FileStream inside a using block
            using (FileStream csvStream = new FileStream(outputCsvPath, FileMode.Create, FileAccess.Write))
            {
                // Save the diagram to the CSV stream
                diagram.Save(csvStream, SaveFileFormat.Csv);
            }

            Console.WriteLine("Conversion completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
