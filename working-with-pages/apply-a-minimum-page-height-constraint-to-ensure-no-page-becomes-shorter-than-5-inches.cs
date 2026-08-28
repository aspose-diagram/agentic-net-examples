using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Minimum page height in inches
                const double minHeight = 5.0;

                // Iterate through all pages and enforce the minimum height
                foreach (Page page in diagram.Pages)
                {
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
                    if (currentHeight < minHeight)
                    {
                        page.PageSheet.PageProps.PageHeight.Value = minHeight;
                    }
                }

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
