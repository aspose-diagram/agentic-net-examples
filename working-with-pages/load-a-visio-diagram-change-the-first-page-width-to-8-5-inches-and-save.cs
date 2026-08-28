using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            try
            {
                // Load the Visio diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count > 0)
                {
                    // Access the first page (index 0)
                    Page firstPage = diagram.Pages[0];

                    // Set the page width to 8.5 inches
                    firstPage.PageSheet.PageProps.PageWidth.Value = 8.5;
                }
                else
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Save the modified diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }