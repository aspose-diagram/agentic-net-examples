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

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Preserve the original auto‑expand setting
                var originalAutoExpand = page.PageSheet.PageProps.DrawingResizeType.Value;

                // Temporarily disable page auto‑expand
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                // Modify page dimensions (example: 11 inches width, 8.5 inches height)
                page.PageSheet.PageProps.PageWidth.Value = 11.0;
                page.PageSheet.PageProps.PageHeight.Value = 8.5;

                // Re‑enable auto‑expand by restoring the original setting
                page.PageSheet.PageProps.DrawingResizeType.Value = originalAutoExpand;

                // Save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }