using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your source as needed)
            Diagram diagram = new Diagram("sample.vsdx");

            // Configure HTML save options (customize as required)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example: generate a single HTML file with embedded resources
                SaveAsSingleFile = true,
                // Example: include the toolbar in the output
                SaveToolBar = true
            };

            // Save the diagram to a memory stream in HTML format
            using (MemoryStream htmlStream = new MemoryStream())
            {
                diagram.Save(htmlStream, htmlOptions);

                // Reset the stream position before reading
                htmlStream.Position = 0;

                // Convert the HTML bytes to a string
                string htmlContent = new StreamReader(htmlStream, Encoding.UTF8).ReadToEnd();

                // htmlContent now contains the generated HTML without any file system access
                Console.WriteLine(htmlContent);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
