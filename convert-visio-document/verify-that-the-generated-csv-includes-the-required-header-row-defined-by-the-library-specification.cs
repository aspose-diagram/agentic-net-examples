using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";
                // Output CSV file path
                string csvPath = "output.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Export the diagram to CSV format
                diagram.Save(csvPath, SaveFileFormat.Csv);

                // Expected header row as defined by Aspose.Diagram CSV specification
                // (example header – adjust if the library defines a different set of columns)
                string expectedHeader = "ShapeID,ShapeName,ShapeType";

                // Read the first line of the generated CSV file
                string actualHeader;
                using (var reader = new StreamReader(csvPath))
                {
                    actualHeader = reader.ReadLine();
                }

                // Verify that the header matches the expected value
                if (actualHeader == null)
                {
                    throw new Exception("CSV file is empty. Header row is missing.");
                }

                if (!actualHeader.Equals(expectedHeader, StringComparison.Ordinal))
                {
                    throw new Exception($"CSV header mismatch. Expected: \"{expectedHeader}\", Actual: \"{actualHeader}\"");
                }

                Console.WriteLine("CSV header verification succeeded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }