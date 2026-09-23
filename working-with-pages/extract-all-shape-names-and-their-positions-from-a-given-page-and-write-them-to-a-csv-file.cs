using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string visioPath = "input.vsdx";
            // Output CSV file path
            string csvPath = "shapes.csv";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Open a StreamWriter for the CSV file
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("Name,PinX,PinY");

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the universal name of the shape
                        string name = shape.NameU ?? string.Empty;

                        // Retrieve shape position (PinX, PinY)
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Write a CSV line
                        writer.WriteLine($"{name},{pinX},{pinY}");
                    }
                }
            }

            Console.WriteLine("Shape data exported to CSV successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
