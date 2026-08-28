using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Path to the output CSV file
                string csvPath = "PageDimensions.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Prepare the CSV file with a header
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    writer.WriteLine("PageName,Width,Height");

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page width and height (values are in inches)
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        // Write the page information to the CSV
                        writer.WriteLine($"{page.Name},{width},{height}");
                    }
                }

                Console.WriteLine($"Page dimensions have been written to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }