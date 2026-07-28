using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Get the diagram file path from command line or prompt the user
                string filePath;
                if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                {
                    filePath = args[0];
                }
                else
                {
                    Console.Write("Enter the path to the Visio diagram file: ");
                    filePath = Console.ReadLine();
                }

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("No file path provided. Exiting.");
                    return;
                }

                // Load the diagram
                using (Diagram diagram = new Diagram(filePath))
                {
                    double totalAreaCm2 = 0.0;
                    const double inchesToCentimeters = 2.54;

                    // Iterate through each page and calculate dimensions and area
                    foreach (Page page in diagram.Pages)
                    {
                        // Page dimensions are stored in inches
                        double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                        double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                        // Convert to centimeters
                        double widthCm = widthInches * inchesToCentimeters;
                        double heightCm = heightInches * inchesToCentimeters;

                        // Calculate area in square centimeters
                        double areaCm2 = widthCm * heightCm;
                        totalAreaCm2 += areaCm2;

                        // Output page information
                        Console.WriteLine($"Page ID {page.ID}: Width = {widthCm:F2} cm, Height = {heightCm:F2} cm, Area = {areaCm2:F2} cm²");
                    }

                    // Output total diagram area
                    Console.WriteLine($"Total diagram area across all pages: {totalAreaCm2:F2} cm²");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }