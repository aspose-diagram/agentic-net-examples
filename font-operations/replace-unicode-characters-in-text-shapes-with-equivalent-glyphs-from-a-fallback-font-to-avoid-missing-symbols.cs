using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Fallback font to use for missing Unicode glyphs
        const string fallbackFont = "Arial Unicode MS";

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and shapes to ensure text shapes are visited
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Verify the shape actually contains text
                if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                {
                    // No explicit character replacement is required here.
                    // Setting the DefaultFont in the save options will cause
                    // missing glyphs to be rendered using the fallback font.
                }
            }
        }

        // Configure save options with the fallback font
        DiagramSaveOptions saveOptions = new DiagramSaveOptions();
        saveOptions.DefaultFont = fallbackFont;

        // Save the diagram using the configured options
        diagram.Save(outputPath, saveOptions);
    }
}
