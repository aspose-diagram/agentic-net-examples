using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page height (in inches)
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Define a factor to calculate rotation based on page height
                        // This example uses 5 degrees per inch of page height
                        double degreesPerInch = 5.0;
                        double angleDeg = pageHeight * degreesPerInch;

                        // Convert degrees to radians because the Angle cell expects radians
                        double angleRad = Math.PI * angleDeg / 180.0;

                        // Adjust rotation for each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Set the rotation angle (in radians)
                            shape.XForm.Angle.Value = angleRad;
                        }
                    }

                    // Save the modified diagram back to Visio format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Shape rotation angles have been adjusted based on page height.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }