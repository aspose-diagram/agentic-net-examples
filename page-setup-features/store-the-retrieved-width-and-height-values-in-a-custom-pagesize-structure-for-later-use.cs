using System;
using Aspose.Diagram;

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

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve page width and height (values are in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Store the dimensions in the custom PageSize struct
                PageSize size = new PageSize(pageWidth, pageHeight);

                // Output the stored size for verification
                Console.WriteLine($"Stored page size: {size}");

                // Save the diagram (optional, demonstrates lifecycle usage)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }