using System.IO;
using System;
using Aspose.Diagram;

struct PageSize
{
    public double Width;
    public double Height;

    public PageSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"Width: {Width}, Height: {Height}";
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual file path)
            string diagramPath = "input.vsdx";

            // Load the diagram within a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve page width and height (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Store the dimensions in a custom PageSize structure
                PageSize pageSize = new PageSize(width, height);

                // Output the stored values
                Console.WriteLine($"Retrieved page size: {pageSize}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
