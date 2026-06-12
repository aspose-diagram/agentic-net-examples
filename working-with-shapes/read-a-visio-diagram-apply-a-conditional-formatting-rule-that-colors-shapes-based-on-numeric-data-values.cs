using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the Data1 field as the numeric value source
                    if (!string.IsNullOrWhiteSpace(shape.Data1))
                    {
                        double numericValue;
                        bool parsed = double.TryParse(
                            shape.Data1,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out numericValue);

                        if (parsed)
                        {
                            // Ensure the shape uses a solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // Solid fill

                            // Apply fill color based on the numeric value
                            if (numericValue > 100)
                            {
                                // High values – red
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else if (numericValue > 50)
                            {
                                // Medium values – orange
                                shape.Fill.FillForegnd.Value = "#FFA500";
                            }
                            else
                            {
                                // Low values – green
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }
                        }
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
