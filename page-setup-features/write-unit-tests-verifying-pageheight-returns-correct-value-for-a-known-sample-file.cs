using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the sample Visio file (ensure the file exists at this location)
                const string sampleFilePath = "sample.vsdx";

                // Expected page height in inches for the known sample file
                const double expectedPageHeight = 11.0;

                // Load the diagram using the Aspose.Diagram constructor
                using (Diagram diagram = new Diagram(sampleFilePath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Access the page height value (in inches)
                    double actualPageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Output the retrieved value for diagnostic purposes
                    Console.WriteLine($"Actual Page Height: {actualPageHeight} inches");

                    // Verify the height matches the expected value
                    if (Math.Abs(actualPageHeight - expectedPageHeight) > 0.0001)
                    {
                        throw new Exception($"Page height mismatch. Expected: {expectedPageHeight}, Actual: {actualPageHeight}");
                    }

                    // If the check passes, indicate success
                    Console.WriteLine("Page height verification passed.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }