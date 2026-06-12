using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesWithCustomDpi
{
    static void Main()
    {
        try
        {

            // Input Visio file
            string inputFile = "input.vsdx";

            // Folder where PNG images will be saved
            string outputFolder = "ExportedShapes";
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputFile);

            // Prepare image save options for PNG with 600 DPI
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 600; // Set custom DPI

            // Export each shape on each page to a PNG file
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string imagePath = Path.Combine(
                        outputFolder,
                        $"Page{page.ID}_Shape{shape.ID}.png");

                    shape.ToImage(imagePath, imgOptions);
                }
            }

            // Optionally save the diagram (unchanged) to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
