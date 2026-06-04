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
            Diagram diagram = new Diagram(@"C:\Docs\sample.vsdx");

            // Select the shape to be converted (e.g., first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Save the HTML as a single file for easier handling
                SaveAsSingleFile = true
            };

            // UNC path on the network share where the HTML will be written
            string uncPath = @"\\Server\Share\Exports\shape.html";

            // Ensure the target directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(uncPath));

            // Create a FileStream that writes directly to the UNC location
            using (FileStream stream = new FileStream(uncPath, FileMode.Create, FileAccess.Write))
            {
                // Export the shape to HTML using the stream
                shape.ToHTML(stream, htmlOptions);
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
