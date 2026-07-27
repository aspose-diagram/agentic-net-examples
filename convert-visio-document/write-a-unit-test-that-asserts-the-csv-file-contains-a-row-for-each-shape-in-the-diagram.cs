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

            // Paths to the input Visio file and the output CSV file.
            // Adjust these paths as needed for your environment.
            string diagramPath = "input.vsdx";
            string csvPath = "output.csv";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Count the total number of shapes across all pages.
            int shapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    shapeCount++;
                }
            }

            // Export the diagram to CSV format.
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Verify that the CSV file was created.
            if (!File.Exists(csvPath))
            {
                throw new Exception($"CSV file was not created at path: {csvPath}");
            }

            // Read all lines from the CSV file.
            string[] lines = File.ReadAllLines(csvPath);
            int lineCount = lines.Length;

            // Determine whether the CSV includes a header row.
            // This simple check looks for a common header keyword; adjust if necessary.
            bool headerPresent = lineCount > 0 && lines[0].Contains("ShapeID");
            int dataRowCount = headerPresent ? lineCount - 1 : lineCount;

            // Validate that each shape has a corresponding CSV row.
            if (dataRowCount != shapeCount)
            {
                throw new Exception($"CSV row count ({dataRowCount}) does not match shape count ({shapeCount}).");
            }

            Console.WriteLine("CSV validation succeeded: each shape has a corresponding row.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
