using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get diagram file path from command line or use a default name
        string filePath = args.Length > 0 ? args[0] : "input.vsdx";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Load the diagram inside a using block to ensure resources are released
        using (Diagram diagram = new Diagram(filePath))
        {
            double totalAreaCm2 = 0;
            int pageNumber = 0;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                pageNumber++;

                // Page dimensions are stored in inches
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert inches to centimeters (1 inch = 2.54 cm)
                double widthCm = widthInches * 2.54;
                double heightCm = heightInches * 2.54;

                // Calculate area for the current page
                double areaCm2 = widthCm * heightCm;
                totalAreaCm2 += areaCm2;

                Console.WriteLine($"Page {pageNumber}: Width = {widthCm:F2} cm, Height = {heightCm:F2} cm, Area = {areaCm2:F2} cm²");
            }

            // Output total area of all pages
            Console.WriteLine($"Total diagram area: {totalAreaCm2:F2} cm²");
        }
    }
}
