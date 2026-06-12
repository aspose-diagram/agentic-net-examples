using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output SWF file path
            string outputPath = "output.swf";

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Verify that the diagram contains at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram does not contain any pages.");
                }

                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("The first page does not contain any shapes.");
                }

                // Retrieve the first shape (as an example)
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);
                if (shape == null)
                {
                    throw new Exception("Failed to retrieve the shape.");
                }

                // Prepare SWF save options
                SWFSaveOptions swfOptions = new SWFSaveOptions
                {
                    // Include the integrated viewer in the SWF file
                    ViewerIncluded = true,
                    // Render all pages (default behavior)
                    PageIndex = 0,
                    PageCount = int.MaxValue
                };

                // Save the diagram (which includes the shape) to SWF format
                diagram.Save(outputPath, swfOptions);

                Console.WriteLine("Shape successfully converted to SWF format.");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during loading, processing, or saving
                Console.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }