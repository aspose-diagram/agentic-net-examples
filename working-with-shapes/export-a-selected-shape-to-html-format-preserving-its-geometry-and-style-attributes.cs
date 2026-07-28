using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path where the HTML file will be saved
                string outputHtmlPath = "shape.html";

                // Identifier of the shape to export (adjust as needed)
                long shapeId = 1;

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram does not contain any pages.");

                // Use the first page (or select a specific page by name/index)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID (cast to int as required by GetShape)
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape == null)
                    throw new Exception($"Shape with ID {shapeId} was not found on page '{page.NameU}'.");

                // Configure HTML export options (default settings preserve geometry and style)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Export the selected shape to HTML
                shape.ToHTML(outputHtmlPath, htmlOptions);

                Console.WriteLine($"Shape {shapeId} successfully exported to HTML at '{outputHtmlPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }