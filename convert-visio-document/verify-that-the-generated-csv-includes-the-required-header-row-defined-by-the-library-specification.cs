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

            // Paths for the source Visio file and the generated CSV
            string visioPath = "input.vsdx";
            string csvPath = "output.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Export the diagram to CSV format
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Verify that the CSV file contains the required header row
            if (!File.Exists(csvPath))
                throw new Exception("CSV file was not created.");

            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
                throw new Exception("CSV file is empty.");

            // Expected header as defined by the library specification
            const string expectedHeader = "Shape ID,Shape Name,Shape Type";

            string actualHeader = lines[0];
            if (!actualHeader.Equals(expectedHeader, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"CSV header mismatch. Expected: \"{expectedHeader}\", Got: \"{actualHeader}\"");

            Console.WriteLine("CSV header verified successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
