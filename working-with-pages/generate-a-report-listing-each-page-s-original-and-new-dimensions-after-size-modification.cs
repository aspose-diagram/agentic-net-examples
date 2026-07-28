using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve original page dimensions (in inches)
                    double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define new dimensions (e.g., increase each side by 1 inch)
                    double newWidth = originalWidth + 1.0;
                    double newHeight = originalHeight + 1.0;

                    // Apply the new dimensions to the page
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Output a report line for this page
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                    Console.WriteLine($"  Original Size: {originalWidth}in x {originalHeight}in");
                    Console.WriteLine($"  New Size:      {newWidth}in x {newHeight}in");
                }

                // Save the modified diagram to a new file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
