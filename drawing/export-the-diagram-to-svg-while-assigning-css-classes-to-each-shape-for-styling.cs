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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assign a CSS class name to each shape.
            // Here we use the NameU property to store the class name.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.NameU = $"cls_{shape.ID}";
                }
            }

            // Prepare SVG save options.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Ensure the whole page is rendered.
                PageIndex = 0,
                // Optional: fit the SVG to the viewport.
                SVGFitToViewPort = true
            };

            // Save the diagram as an SVG file using the provided Save method.
            diagram.Save("output.svg", svgOptions);

            // Post‑process the generated SVG to replace the default id attribute
            // with a class attribute that contains the CSS class we assigned.
            string svgContent = File.ReadAllText("output.svg", Encoding.UTF8);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string idAttribute = $"id=\"{shape.ID}\"";
                    string classAttribute = $"class=\"{shape.NameU}\"";
                    svgContent = svgContent.Replace(idAttribute, classAttribute);
                }
            }

            // Write the modified SVG back to disk.
            File.WriteAllText("output.svg", svgContent, Encoding.UTF8);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
