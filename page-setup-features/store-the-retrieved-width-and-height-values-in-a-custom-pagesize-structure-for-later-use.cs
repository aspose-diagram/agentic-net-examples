using System;
using Aspose.Diagram;

// Simple structure to hold page dimensions.
    public struct PageSize
    {
        public double Width;   // Page width in inches.
        public double Height;  // Page height in inches.

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

                // Path to the Visio file. Adjust as needed.
                string diagramPath = "sample.vsdx";

                // Load the diagram using Aspose.Diagram.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Ensure there is at least one page.
                    if (diagram.Pages.Count == 0)
                    {
                        Console.WriteLine("The diagram contains no pages.");
                        return;
                    }

                    // Access the first page (index 0).
                    Page page = diagram.Pages[0];

                    // Retrieve page width and height (values are in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Store the dimensions in the custom PageSize struct.
                    PageSize size = new PageSize(pageWidth, pageHeight);

                    // Use the stored size as needed; here we simply display it.
                    Console.WriteLine("Page dimensions stored in PageSize struct:");
                    Console.WriteLine(size);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }