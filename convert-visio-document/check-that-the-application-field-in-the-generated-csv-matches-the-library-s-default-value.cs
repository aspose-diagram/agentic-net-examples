using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string csvPath = "output.csv";

        try
        {
            // Create a new diagram with a default page
            Diagram diagram = new Diagram();

            // Add a simple shape to ensure the CSV contains data
            // Page index 0, master shape 1 (basic rectangle), shape name "Rectangle", master shape index 0
            diagram.AddShape(0, 1, "Rectangle", 0);

            // Save the diagram as CSV using the library's default settings
            diagram.Save(csvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram creation or saving: {ex.Message}");
            return;
        }

        // Verify the CSV file was created
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file was not created at path: {csvPath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0)
        {
            Console.Error.WriteLine("CSV file is empty.");
            return;
        }

        // Parse header to find "Application" column
        string headerLine = lines[0];
        string[] headers = headerLine.Split(',');

        int appIndex = Array.IndexOf(headers, "Application");
        if (appIndex == -1)
        {
            Console.Error.WriteLine("The CSV does not contain an 'Application' column.");
            return;
        }

        // Ensure there is at least one data row
        if (lines.Length < 2)
        {
            Console.Error.WriteLine("CSV file does not contain data rows.");
            return;
        }

        string dataLine = lines[1];
        string[] values = dataLine.Split(',');

        if (appIndex >= values.Length)
        {
            Console.Error.WriteLine("The 'Application' column index exceeds the number of values in the data row.");
            return;
        }

        string applicationValue = values[appIndex].Trim();

        const string expectedDefault = "Aspose.Diagram";

        if (!string.Equals(applicationValue, expectedDefault, StringComparison.Ordinal))
        {
            Console.Error.WriteLine($"Application field mismatch. Expected: '{expectedDefault}', Actual: '{applicationValue}'.");
            return;
        }

        Console.WriteLine("Application field matches the library's default value.");
    }
}