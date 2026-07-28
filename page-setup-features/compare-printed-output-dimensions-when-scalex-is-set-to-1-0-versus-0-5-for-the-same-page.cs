using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the page's original dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Helper method to set ScaleX, compute printed dimensions, and display them
                void ShowPrintedDimensions(double scaleX)
                {
                    // Apply uniform scaling for both X and Y axes
                    page.PageSheet.PrintProps.ScaleX.Value = scaleX;
                    page.PageSheet.PrintProps.ScaleY.Value = scaleX;

                    // Printed size is the original size multiplied by the scaling factor
                    double printedWidth = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
                    double printedHeight = pageHeight * page.PageSheet.PrintProps.ScaleY.Value;

                    Console.WriteLine($"ScaleX = {scaleX}, Printed Width = {printedWidth:F2} inches, Printed Height = {printedHeight:F2} inches");
                }

                // Compare dimensions for ScaleX = 1.0 and ScaleX = 0.5
                ShowPrintedDimensions(1.0);
                ShowPrintedDimensions(0.5);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
