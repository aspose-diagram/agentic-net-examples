using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // Replace with the actual path to your .vsdx file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a master (i.e., it can inherit formatting)
                    bool inheritsFill = shape.Master != null;

                    // Log the inheritance status
                    Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Inherits Fill from Master: {inheritsFill}");
                }
            }

            // Optionally, save the diagram (if any modifications were made)
            // string outputPath = "output.vsdx";
            // diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
