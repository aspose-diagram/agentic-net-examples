using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the exported CSV file
                string outputPath = "output.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Note: Aspose.Diagram does not expose an Encoding property on SaveOptions.
                // The CSV export uses UTF‑8 encoding internally, so no additional configuration is required.

                // Save the diagram as CSV
                diagram.Save(outputPath, SaveFileFormat.Csv);

                Console.WriteLine($"Diagram successfully exported to CSV at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }