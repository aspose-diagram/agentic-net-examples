using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Custom structure to hold page dimensions
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
            return $"Width: {Width} inches, Height: {Height} inches";
        }
    }

    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the Visio file
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page in the diagram
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Read page width and height (values are in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Store the dimensions in the custom PageSize structure
            PageSize size = new PageSize(pageWidth, pageHeight);

            // Output the stored dimensions for verification
            Console.WriteLine("Retrieved page size:");
            Console.WriteLine(size);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}