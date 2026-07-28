using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one stylesheet
                if (diagram.Pages.Count > 0 && diagram.StyleSheets.Count > 0)
                {
                    // Get the first page (page index 0)
                    Page page = diagram.Pages[0];

                    // Get the first stylesheet in the collection
                    StyleSheet style = diagram.StyleSheets[0];

                    // Apply the stylesheet to the page.
                    // ApplyStyle(lineStyleId, fillStyleId, textStyleId)
                    page.ApplyStyle(style.ID, style.ID, style.ID);
                }
                else
                {
                    Console.WriteLine("Diagram does not contain pages or stylesheets.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Style applied and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }