using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioFilePath = "input.vsdx";

                // Path to the CSV file that will contain the page dimensions
                string csvOutputPath = "PageDimensions.csv";

                // Load the diagram from the specified file
                Diagram diagram = new Diagram(visioFilePath);

                // Open a StreamWriter to create the CSV file
                using (StreamWriter writer = new StreamWriter(csvOutputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("PageID,PageName,WidthInches,HeightInches");

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page width and height (values are in inches)
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        // Log the dimensions to the CSV file
                        writer.WriteLine($"{page.ID},{page.Name},{width},{height}");
                    }
                }

                Console.WriteLine($"Page dimensions have been written to '{csvOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }