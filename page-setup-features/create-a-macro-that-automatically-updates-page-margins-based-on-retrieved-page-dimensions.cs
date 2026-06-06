using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Prompt user for input and output file paths
        Console.Write("Enter the path of the Visio file to process: ");
        string inputPath = Console.ReadLine();

        Console.Write("Enter the path where the updated Visio file will be saved: ");
        string outputPath = Console.ReadLine();

        // Load the diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve current page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate new margins as 5% of the respective dimensions
                double horizontalMargin = pageWidth * 0.05; // left and right
                double verticalMargin = pageHeight * 0.05;  // top and bottom

                // Update the page's print margins
                page.PageSheet.PrintProps.PageLeftMargin.Value = horizontalMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = horizontalMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = verticalMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = verticalMargin;

                Console.WriteLine($"Page '{page.Name}' margins updated: " +
                                  $"Left/Right = {horizontalMargin:F3} in, " +
                                  $"Top/Bottom = {verticalMargin:F3} in.");
            }

            // Save the modified diagram back to a Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
    }
}
