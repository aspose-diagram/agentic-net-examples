using System;
using System.IO;
using Aspose.Diagram;

class ExportShapeTexts
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = @"C:\Diagrams\sample.vsdx";

            // Load the Visio diagram (uses Aspose.Diagram's load rule)
            Diagram diagram = new Diagram(visioPath);

            // Output text file path
            string outputPath = @"C:\Diagrams\ShapeTexts.txt";

            // Use a StreamWriter to create/overwrite the output file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string shapeText = shape.GetPureText();

                        // Write shape identifier and its text to the file
                        writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                        writer.WriteLine(shapeText);
                        writer.WriteLine(new string('-', 40));
                    }
                }
            }

            Console.WriteLine($"Shape texts exported to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
