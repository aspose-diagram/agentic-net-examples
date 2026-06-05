using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (or change index as required)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center coordinates of the page
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is not deleted and has a master
                    if (shape.Del == BOOL.False && shape.Master != null)
                    {
                        // Identify the pentagon shape by its master name
                        if (shape.Master.Name == "Pentagon")
                        {
                            // Align the pentagon to the page center
                            shape.XForm.PinX.Value = centerX;
                            shape.XForm.PinY.Value = centerY;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }