using System;
using System.IO;
using Aspose.Diagram;

class ExportShapeText
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape
                    string shapeText = shape.GetPureText();

                    // If the shape contains any text, export it to a .txt file
                    if (!string.IsNullOrEmpty(shapeText))
                    {
                        // Build a file name that identifies the shape (using its ID)
                        string fileName = $"Shape_{shape.ID}_Text.txt";

                        // Write the text to the file (overwrites if the file already exists)
                        File.WriteAllText(fileName, shapeText);
                    }
                }
            }

            // Optionally, save the diagram if any modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
