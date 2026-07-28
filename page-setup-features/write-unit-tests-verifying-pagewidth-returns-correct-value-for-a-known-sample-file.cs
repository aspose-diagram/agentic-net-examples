using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the sample Visio file (ensure this file exists at the specified location)
                string filePath = "sample.vsdx";

                // Expected page width in inches for the known sample file
                double expectedWidth = 8.5; // Example: standard US Letter width

                // Load the diagram
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Retrieve the first page
                    Page page = diagram.Pages[0];

                    // Get the page width (value is in inches)
                    double actualWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Verify the width matches the expected value
                    if (Math.Abs(actualWidth - expectedWidth) > 0.001)
                    {
                        throw new Exception($"Page width mismatch. Expected {expectedWidth}, but got {actualWidth}.");
                    }

                    Console.WriteLine("Page width test passed.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }