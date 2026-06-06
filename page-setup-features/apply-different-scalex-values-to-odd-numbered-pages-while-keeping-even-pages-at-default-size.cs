using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set ScaleX
            int pageNumber = 0;
            foreach (Page page in diagram.Pages)
            {
                pageNumber++;

                // Odd-numbered pages get a custom scale (e.g., 75%)
                if (pageNumber % 2 == 1)
                {
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75; // 75% of original size
                }
                else // Even-numbered pages keep default scale (100%)
                {
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0; // 100% size
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
