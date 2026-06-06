using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the sample Visio file (ensure this file exists at the specified location)
                const string sampleFilePath = "sample.vsdx";

                // Expected page width in inches for the known sample file
                const double expectedPageWidth = 8.5; // Example: standard US Letter width

                // Load the diagram
                using (Diagram diagram = new Diagram(sampleFilePath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Get the page width value (in inches)
                    double actualPageWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Verify the page width matches the expected value
                    if (Math.Abs(actualPageWidth - expectedPageWidth) > 0.001)
                    {
                        throw new Exception($"Page width verification failed. Expected: {expectedPageWidth}, Actual: {actualPageWidth}");
                    }

                    Console.WriteLine("Page width verification passed.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }