using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (ensure at least one page exists)
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                Page page = diagram.Pages[0];

                // Retrieve current page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a comment position within the page bounds
                double commentX = 2.0; // PinX coordinate (in inches)
                double commentY = 2.0; // PinY coordinate (in inches)

                // Add a comment to the page at the specified coordinates
                page.AddComment(commentX, commentY, "Sample comment for validation");

                // Validate that the comment is initially within page boundaries
                if (commentX > pageWidth || commentY > pageHeight)
                    throw new Exception("Comment position is outside page boundaries before scaling.");

                // Apply scaling factors (e.g., 50% of original size)
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                page.PageSheet.PrintProps.ScaleY.Value = 0.5;

                // Compute scaled page dimensions
                double scaledWidth = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
                double scaledHeight = pageHeight * page.PageSheet.PrintProps.ScaleY.Value;

                // Validate that the comment still resides within the scaled page area
                if (commentX > scaledWidth || commentY > scaledHeight)
                    throw new Exception("Comment position is outside page boundaries after scaling.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }