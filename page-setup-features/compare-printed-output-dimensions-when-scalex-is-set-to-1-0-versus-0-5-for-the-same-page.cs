using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Set page size (e.g., standard Letter size 8.5 x 11 inches)
                page.PageSheet.PageProps.PageWidth.Value = 8.5;
                page.PageSheet.PageProps.PageHeight.Value = 11.0;

                // Access the print properties of the page
                var printProps = page.PageSheet.PrintProps;

                // Function to compute and display printed dimensions for a given ScaleX
                void ShowPrintedDimensions(double scaleX)
                {
                    // Set scaling factors
                    printProps.ScaleX.Value = scaleX;
                    printProps.ScaleY.Value = scaleX; // Assuming uniform scaling

                    // Original page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Printed dimensions after scaling
                    double printedWidth = pageWidth * printProps.ScaleX.Value;
                    double printedHeight = pageHeight * printProps.ScaleY.Value;

                    Console.WriteLine($"ScaleX = {scaleX}");
                    Console.WriteLine($"Original Size: {pageWidth}in x {pageHeight}in");
                    Console.WriteLine($"Printed Size : {printedWidth}in x {printedHeight}in");
                    Console.WriteLine(new string('-', 40));
                }

                // Compare dimensions for ScaleX = 1.0 and ScaleX = 0.5
                ShowPrintedDimensions(1.0);
                ShowPrintedDimensions(0.5);
            }
        }
    }