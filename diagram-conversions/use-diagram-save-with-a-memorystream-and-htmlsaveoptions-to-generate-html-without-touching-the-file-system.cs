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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("sample.vsd");

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveAsSingleFile = true; // optional: generate a single HTML file

            // Create a memory stream to hold the HTML output
            using (MemoryStream htmlStream = new MemoryStream())
            {
                // Save the diagram as HTML into the memory stream
                diagram.Save(htmlStream, htmlOptions);

                // Reset the stream position to the beginning for reading
                htmlStream.Position = 0;

                // Read the generated HTML content from the stream
                using (StreamReader reader = new StreamReader(htmlStream))
                {
                    string htmlContent = reader.ReadToEnd();

                    // Example usage: output the HTML to console
                    Console.WriteLine(htmlContent);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
