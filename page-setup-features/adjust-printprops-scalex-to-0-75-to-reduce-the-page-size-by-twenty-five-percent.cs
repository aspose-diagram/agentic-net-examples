using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Adjust ScaleX for each page to 75% (reduce size by 25%)
                foreach (Page page in diagram.Pages)
                {
                    // Access the print properties of the page
                    var printProps = page.PageSheet.PrintProps;

                    // Set the horizontal scaling factor
                    printProps.ScaleX.Value = 0.75;
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
