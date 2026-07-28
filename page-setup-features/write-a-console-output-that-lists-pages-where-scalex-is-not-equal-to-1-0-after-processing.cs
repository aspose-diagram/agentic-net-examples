using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string filePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages explicitly typing the loop variable
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Retrieve the ScaleX value from the page's PrintProps
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                    // List pages where ScaleX is not exactly 1.0
                    if (scaleX != 1.0)
                    {
                        Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, ScaleX: {scaleX}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
