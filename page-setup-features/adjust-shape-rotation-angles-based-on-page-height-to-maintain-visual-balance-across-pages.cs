using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from a file stream
                using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page height (in inches)
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Original rotation angle (in degrees)
                            double originalAngle = shape.XForm.Angle.Value;

                            // Example adjustment: add a factor based on page height
                            // This keeps the rotation proportional to the page size
                            double adjustedAngle = (originalAngle + (pageHeight * 5)) % 360;

                            // Apply the new rotation angle
                            shape.XForm.Angle.Value = adjustedAngle;
                        }
                    }

                    // Save the modified diagram back to Visio format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    diagram.Dispose();
                }

                Console.WriteLine("Shape rotation angles have been adjusted and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }