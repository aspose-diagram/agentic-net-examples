using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file on a network share
                string inputPath = @"\\ServerName\ShareFolder\SampleDiagram.vsd";

                // Path where the CSV file will be saved
                string outputPath = @"C:\ExportedDiagram.csv";

                // Load the Visio diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Save the diagram as CSV
                diagram.Save(outputPath, SaveFileFormat.Csv);

                Console.WriteLine("Diagram successfully exported to CSV at: " + outputPath);

            }
            catch (System.IO.IOException ex)
            {
                Console.Error.WriteLine($"[IOException] {ex.Message}");
            }
    }
    }