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

            // Paths for input Visio file and output SVG file
            string inputPath = "sample.vsdx";
            string svgPath = "output.svg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that at least one shape contains a hyperlink
            bool hyperlinkFound = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has hyperlink: {link.Address.Value}");
                            hyperlinkFound = true;
                        }
                    }
                }
            }

            if (!hyperlinkFound)
            {
                throw new Exception("No hyperlinks found in the diagram.");
            }

            // Export the diagram to SVG format
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;
            diagram.Save(svgPath, svgOptions);
            Console.WriteLine($"Diagram saved as SVG to {svgPath}");

            // Simple validation: ensure the SVG file contains the hyperlink URL(s)
            string svgContent = File.ReadAllText(svgPath);
            if (svgContent.Contains("http"))
            {
                Console.WriteLine("Hyperlink URLs appear in the SVG file.");
            }
            else
            {
                throw new Exception("Hyperlink URLs not found in the SVG output.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
