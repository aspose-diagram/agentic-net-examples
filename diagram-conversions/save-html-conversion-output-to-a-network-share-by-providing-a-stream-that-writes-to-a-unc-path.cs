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

            // Path to the source Visio file
            string sourceDiagramPath = @"C:\Diagrams\sample.vsdx";

            // Load the diagram (uses the standard constructor)
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Select a shape to convert – here we take the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            // UNC network share where the HTML output will be written
            string uncHtmlPath = @"\\Server\Share\output.html";

            // Create a file stream that points to the UNC location
            using (FileStream outputStream = new FileStream(uncHtmlPath, FileMode.Create, FileAccess.Write))
            {
                // Configure HTML save options as needed
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Example: save everything into a single HTML file
                    SaveAsSingleFile = true
                };

                // Write the shape's HTML representation directly to the UNC stream
                shape.ToHTML(outputStream, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
