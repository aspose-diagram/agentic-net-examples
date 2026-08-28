using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Maximum allowed page height in inches
            double maxHeight = 11.0; // adjust as needed

            // Load the Visio diagram (replace with your actual file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Check each page's height
            foreach (Page page in diagram.Pages)
            {
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
                if (pageHeight > maxHeight)
                {
                    // Raise an exception if the height exceeds the limit
                    throw new Exception($"Page \"{page.Name}\" height {pageHeight} exceeds the maximum allowed {maxHeight} inches.");
                }
            }

            Console.WriteLine("All page heights are within the allowed limit.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
