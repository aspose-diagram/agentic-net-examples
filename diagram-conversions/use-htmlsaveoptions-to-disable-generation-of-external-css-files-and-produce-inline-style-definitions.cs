using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the path to your source file.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options.
                // Setting SaveAsSingleFile to true embeds all resources (including CSS) into the HTML,
                // preventing the generation of external CSS files and using inline style definitions.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    SaveAsSingleFile = true
                };

                // Export the diagram to HTML with the configured options.
                // The output will be a single HTML file with inline styles.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }