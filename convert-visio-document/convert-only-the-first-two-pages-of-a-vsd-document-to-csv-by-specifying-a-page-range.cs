using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file (VSD format)
                string inputPath = "input.vsd";
                // Output CSV file
                string outputPath = "output.csv";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(inputPath, LoadFileFormat.Vsd);

                // Verify that the source diagram has at least two pages
                if (sourceDiagram.Pages.Count < 2)
                    throw new Exception("The source diagram must contain at least two pages.");

                // Create a new empty diagram
                Diagram newDiagram = new Diagram();

                // The default constructor creates one empty page; remove it
                newDiagram.Pages.Remove(newDiagram.Pages[0]);

                // Add only the first two pages from the source diagram
                newDiagram.Pages.Add(sourceDiagram.Pages[0]);
                newDiagram.Pages.Add(sourceDiagram.Pages[1]);

                // Save the new diagram as CSV (only the added pages will be exported)
                newDiagram.Save(outputPath, SaveFileFormat.Csv);

                // Clean up resources
                sourceDiagram.Dispose();
                newDiagram.Dispose();

                Console.WriteLine($"First two pages exported to CSV successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }