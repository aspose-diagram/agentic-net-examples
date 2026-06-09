using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page to inspect its size (values are in inches)
                Page page = diagram.Pages[0];
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Standard page dimensions
                const double a4Width = 8.27;
                const double a4Height = 11.69;
                const double letterWidth = 8.5;
                const double letterHeight = 11.0;
                const double tolerance = 0.1; // Allow minor rounding differences

                // Determine appropriate header margin
                double headerMargin;
                if (Math.Abs(width - a4Width) < tolerance && Math.Abs(height - a4Height) < tolerance)
                {
                    // A4 format
                    headerMargin = 0.2;
                }
                else if (Math.Abs(width - letterWidth) < tolerance && Math.Abs(height - letterHeight) < tolerance)
                {
                    // Letter format
                    headerMargin = 0.3;
                }
                else
                {
                    // Fallback for other sizes
                    headerMargin = 0.2;
                }

                // Apply the margin globally
                diagram.HeaderFooter.HeaderMargin.Value = headerMargin;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
