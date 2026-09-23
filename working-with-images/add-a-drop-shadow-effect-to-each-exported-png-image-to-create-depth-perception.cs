using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Apply a simple drop shadow to every shape on every page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Fill section before setting shadow properties
                    if (shape.Fill != null)
                    {
                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // Shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Shadow offset (horizontal and vertical)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }
            }

            // Export each page as a PNG image with the applied shadow effect
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i; // Export the specific page
                string outputPath = $"output_page_{i + 1}.png";
                diagram.Save(outputPath, pngOptions);
            }

            Console.WriteLine("Export completed with drop shadows applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
