using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a local file
            Diagram diagram = new Diagram(@"C:\InputDiagram.vsdx");

            // Access a shape to be converted (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Configure HTML save options (optional settings)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true // save as a single HTML file
            };

            // Create a file stream that points to a network share (UNC path)
            using (FileStream networkStream = new FileStream(@"\\ServerName\ShareFolder\ShapeOutput.html",
                                                             FileMode.Create,
                                                             FileAccess.Write))
            {
                // Export the shape to HTML and write directly to the UNC stream
                shape.ToHTML(networkStream, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
