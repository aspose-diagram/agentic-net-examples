using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the path to your source file.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options.
                // HTMLSaveOptions does not expose a property to change the image format.
                // PNG is the default image format used for all images embedded in the HTML output.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Example of setting additional options (optional).
                htmlOptions.ExportHiddenPage = false; // Do not export hidden pages.
                htmlOptions.IsExportComments = false; // Do not include comments in the HTML.

                // Save the diagram as HTML using the configured options.
                // The exported images will be in PNG format by default.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }