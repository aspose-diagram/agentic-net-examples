using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Access the first page (index 0)
                Page firstPage = diagram.Pages[0];

                // Set the page width to 8.5 inches (values are in inches)
                firstPage.PageSheet.PageProps.PageWidth.Value = 8.5;

                // Save the modified diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }