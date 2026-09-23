using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // By default Aspose.Diagram embeds CSS inline.
                // No external CSS files will be generated.
                // (If a property existed to control this, it would be set here.)

                // Save the diagram as HTML with inline styles
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML with inline styles: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }