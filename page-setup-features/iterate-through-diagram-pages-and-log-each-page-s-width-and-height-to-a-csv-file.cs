using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string diagramPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "pages_dimensions.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Write page dimensions to CSV
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // CSV header
                    writer.WriteLine("PageName,Width,Height");

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;
                        string pageName = page.Name ?? string.Empty;

                        // Write a line for the current page
                        writer.WriteLine($"{pageName},{width},{height}");
                    }
                }

                Console.WriteLine($"Page dimensions have been exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }