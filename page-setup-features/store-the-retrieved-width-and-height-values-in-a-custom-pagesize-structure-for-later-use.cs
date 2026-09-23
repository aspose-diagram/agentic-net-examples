using System;
using Aspose.Diagram;

// Custom structure to hold page dimensions
    public struct PageSize
    {
        public double Width;   // Width in inches
        public double Height;  // Height in inches

        public PageSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"Width: {Width} in, Height: {Height} in";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "sample.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Access page width and height via the PageProps cells
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Store dimensions in the custom PageSize structure
                    PageSize size = new PageSize(pageWidth, pageHeight);

                    // Output the stored values for verification
                    Console.WriteLine("Retrieved page size:");
                    Console.WriteLine(size);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }