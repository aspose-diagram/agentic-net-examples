using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Path where the CSV will be saved
            string csvPath = "output.csv";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Export the diagram to CSV format
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Verify that the CSV contains the required header row
            if (!File.Exists(csvPath))
            {
                throw new Exception($"CSV file was not created at path: {csvPath}");
            }

            // Read the first line (header) of the CSV file
            string headerLine = File.ReadLines(csvPath).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new Exception("CSV file is empty or header line is missing.");
            }

            // Example of a required header column (adjust as per library specification)
            // Here we check for the presence of "Shape ID" which is a typical column in Aspose.Diagram CSV export
            if (!headerLine.Contains("Shape ID"))
            {
                throw new Exception($"CSV header does not contain required column 'Shape ID'. Header found: {headerLine}");
            }

            // Additional header checks can be added here if needed
            Console.WriteLine("CSV header verification passed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
