using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the existing Visio diagram file
                string inputPath = "input.vsdx";

                // Path where the modified diagram will be saved
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Count the number of shapes on the current page
                        int shapeCount = page.Shapes.Count;

                        // Determine new page height based on shape count
                        // Example: each shape adds 0.5 inches to the page height
                        double newHeight = shapeCount * 0.5;

                        // Set the page height (values are in inches)
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;
                    }

                    // Save the updated diagram back to a Visio file
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }