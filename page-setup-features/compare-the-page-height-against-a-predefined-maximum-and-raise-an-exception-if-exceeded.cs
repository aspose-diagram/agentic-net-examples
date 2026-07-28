using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define the maximum allowed page height in inches.
                double maxPageHeight = 11.0; // Example: 11 inches

                // Path to the Visio diagram file.
                string diagramPath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (in inches).
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compare against the predefined maximum.
                    if (pageHeight > maxPageHeight)
                    {
                        // Raise an exception if the page height exceeds the limit.
                        throw new Exception($"Page \"{page.Name}\" height ({pageHeight} inches) exceeds the maximum allowed height of {maxPageHeight} inches.");
                    }
                }

                // Optional: Inform the user that all pages are within the allowed height.
                Console.WriteLine("All pages are within the allowed height limit.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }