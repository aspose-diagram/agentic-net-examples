using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (can be .vsdx, .vsd, etc.)
                string sourcePath = "input.vsdx";

                // Load the source diagram
                using (Diagram sourceDiagram = new Diagram(sourcePath))
                {
                    int pageCount = sourceDiagram.Pages.Count;

                    // Iterate through each page in the source diagram
                    for (int i = 0; i < pageCount; i++)
                    {
                        // Retrieve the current page
                        Page srcPage = sourceDiagram.Pages[i];

                        // Create a new empty diagram for the single-page export
                        using (Diagram singlePageDiagram = new Diagram())
                        {
                            // Remove the automatically created blank page
                            if (singlePageDiagram.Pages.Count > 0)
                            {
                                Page blankPage = singlePageDiagram.Pages[0];
                                singlePageDiagram.Pages.Remove(blankPage);
                            }

                            // Add the source page to the new diagram
                            singlePageDiagram.Pages.Add(srcPage);

                            // Define the output CSV file name (e.g., Page_1.csv, Page_2.csv, ...)
                            string outputCsv = $"Page_{i + 1}.csv";

                            // Save the new diagram as CSV
                            singlePageDiagram.Save(outputCsv, SaveFileFormat.Csv);
                        }
                    }
                }

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }