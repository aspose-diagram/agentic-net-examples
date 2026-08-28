using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                double totalAreaCm2 = 0.0;
                int pageNumber = 1;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page width and height in inches
                    double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert dimensions to centimeters (1 inch = 2.54 cm)
                    double widthCm = widthInches * 2.54;
                    double heightCm = heightInches * 2.54;

                    // Calculate area in square centimeters
                    double areaCm2 = widthCm * heightCm;
                    totalAreaCm2 += areaCm2;

                    // Output dimensions and area for the current page
                    Console.WriteLine($"Page {pageNumber}: Width = {widthCm:F2} cm, Height = {heightCm:F2} cm, Area = {areaCm2:F2} cm²");
                    pageNumber++;
                }

                // Output total diagram area
                Console.WriteLine($"Total Diagram Area: {totalAreaCm2:F2} cm²");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
