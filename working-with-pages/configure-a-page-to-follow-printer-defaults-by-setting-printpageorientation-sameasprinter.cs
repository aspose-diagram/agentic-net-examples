using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths (replace with actual paths as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set each page to use printer defaults for orientation
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Print orientation set to SameAsPrinter and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
