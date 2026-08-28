using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual file path)
            string filePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Enumerate pages with explicit index
                int pageIndex = 0;
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Retrieve orientation from PrintProps
                    PrintPageOrientationValue orientationValue = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                    string orientation = orientationValue.ToString();

                    // Retrieve ScaleX from PrintProps
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                    // Output page information
                    Console.WriteLine($"Page Index: {pageIndex}, Orientation: {orientation}, ScaleX: {scaleX}");

                    pageIndex++;
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
