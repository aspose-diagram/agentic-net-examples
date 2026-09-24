using System.IO;
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

            // Load the Visio diagram
            Diagram srcDiagram = new Diagram(inputPath);

            // Counter to generate ordered PDF file names
            int pdfPageIndex = 1;

            // Iterate through each page in the source diagram
            foreach (Page srcPage in srcDiagram.Pages)
            {
                // Iterate layers in the order they appear on the page
                foreach (Layer layer in srcPage.PageSheet.Layers)
                {
                    // Iterate all shapes on the current page
                    foreach (Shape shape in srcPage.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape belongs to the current layer
                        string layerMember = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // LayerMember contains semicolon‑separated layer indexes (as strings)
                        string[] memberIndexes = layerMember.Split(';');
                        foreach (string idxStr in memberIndexes)
                        {
                            if (int.TryParse(idxStr, out int idx) && idx == layer.IX)
                            {
                                // Shape is part of the current layer – export it to a PDF page
                                string outputPdf = $"Shape_{pdfPageIndex:D4}.pdf";
                                shape.ToPdf(outputPdf);
                                pdfPageIndex++;
                                break; // Move to next shape after exporting
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Export completed. Generated " + (pdfPageIndex - 1) + " PDF files.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
