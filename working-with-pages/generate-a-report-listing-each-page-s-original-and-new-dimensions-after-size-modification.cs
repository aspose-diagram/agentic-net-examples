using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (default if not provided)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            // Output Visio file path (default if not provided)
            string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Capture original dimensions (in inches)
                    double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Example modification: increase both width and height by 1 inch
                    double newWidth = originalWidth + 1.0;
                    double newHeight = originalHeight + 1.0;

                    // Apply new dimensions
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Report original and new dimensions
                    Console.WriteLine($"Page ID {page.ID} - Original: {originalWidth}in x {originalHeight}in, New: {newWidth}in x {newHeight}in");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
