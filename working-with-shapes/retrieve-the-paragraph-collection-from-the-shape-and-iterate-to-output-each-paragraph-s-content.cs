using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    shape = s;
                    break;
                }
            }

            if (shape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Iterate through the paragraph collection of the shape
            for (int i = 0; i < shape.Paras.Count; i++)
            {
                Aspose.Diagram.Para para = shape.Paras[i];
                // Output paragraph index (Para objects contain formatting, not direct text)
                Console.WriteLine($"Paragraph {i + 1}:");

                // As Aspose.Diagram does not expose paragraph text directly,
                // we output the full shape text as a placeholder.
                Console.WriteLine(shape.Text.Value.Text);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
