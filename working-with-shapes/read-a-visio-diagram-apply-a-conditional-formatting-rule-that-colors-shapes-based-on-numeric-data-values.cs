using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and the output file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Process each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Assume the numeric value to evaluate is stored in Data1 (string)
                    string dataValue = shape.Data1;

                    // Try to parse the string as a double
                    if (double.TryParse(dataValue, out double numericValue))
                    {
                        // Apply fill color based on the numeric range
                        if (numericValue < 10)
                        {
                            // Light blue for low values
                            shape.Fill.FillForegnd.Value = "#ADD8E6";
                        }
                        else if (numericValue < 20)
                        {
                            // Light green for medium values
                            shape.Fill.FillForegnd.Value = "#90EE90";
                        }
                        else
                        {
                            // Light pink for high values
                            shape.Fill.FillForegnd.Value = "#FFB6C1";
                        }

                        // Ensure the fill pattern is solid (pattern index 1)
                        shape.Fill.FillPattern.Value = 1;
                    }
                }
            }

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
