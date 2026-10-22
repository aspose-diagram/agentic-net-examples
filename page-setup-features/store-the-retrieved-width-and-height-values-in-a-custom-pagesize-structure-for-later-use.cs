using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Custom structure to hold page dimensions.
    struct PageSize
    {
        public double Width;
        public double Height;

        public PageSize(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }

    static void Main(string[] args)
    {
        // Determine the input diagram file path.
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Retrieve page width and height (values are in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Store the dimensions in the custom PageSize structure.
            PageSize size = new PageSize(pageWidth, pageHeight);

            // Output the stored values for verification.
            Console.WriteLine($"Page Width: {size.Width} inches");
            Console.WriteLine($"Page Height: {size.Height} inches");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during Aspose operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}