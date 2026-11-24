using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                double totalAreaCm2 = 0.0;

                Console.WriteLine("Page Dimensions Report");
                Console.WriteLine("------------------------");

                // Iterate over each page to retrieve dimensions.
                foreach (Page page in diagram.Pages)
                {
                    // Page size values are in inches.
                    double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert inches to centimeters (1 inch = 2.54 cm).
                    double widthCm = widthInches * 2.54;
                    double heightCm = heightInches * 2.54;

                    // Calculate area for the current page.
                    double areaCm2 = widthCm * heightCm;
                    totalAreaCm2 += areaCm2;

                    // Output page‑specific information.
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                    Console.WriteLine($"  Width: {widthCm:F2} cm, Height: {heightCm:F2} cm, Area: {areaCm2:F2} cm²");
                }

                Console.WriteLine("------------------------");
                Console.WriteLine($"Total Diagram Area: {totalAreaCm2:F2} cm²");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}