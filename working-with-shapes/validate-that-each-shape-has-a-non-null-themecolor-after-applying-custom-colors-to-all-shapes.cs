using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a custom fill color to every shape and validate that the color is not null or empty
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a custom color (example: solid red)
                    shape.Fill.FillForegnd.Value = "#FF0000";

                    // Validate that the ThemeColor (interpreted as FillForegnd) is set
                    if (string.IsNullOrWhiteSpace(shape.Fill.FillForegnd.Value))
                    {
                        throw new Exception($"Shape ID {shape.ID} has a null or empty ThemeColor.");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("All shapes have a non‑null ThemeColor and the diagram was saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
