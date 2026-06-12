using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (replace with actual path)
                string inputPath = "input.vsdx";

                // Output files
                string pdfPath = "output.pdf";
                string svgPath = "output.svg";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Enable KeepTextFlat for all shapes in all pages
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;
                    }
                }

                // Export to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                diagram.Save(pdfPath, pdfOptions);

                // Export to SVG
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                diagram.Save(svgPath, svgOptions);

                // Read the generated files
                byte[] pdfBytes = File.ReadAllBytes(pdfPath);
                byte[] svgBytes = File.ReadAllBytes(svgPath);

                // Simple comparison: size and content
                bool areEqual = pdfBytes.Length == svgBytes.Length && pdfBytes.SequenceEqual(svgBytes);

                if (areEqual)
                {
                    Console.WriteLine("PDF and SVG outputs are identical.");
                }
                else
                {
                    Console.WriteLine("PDF and SVG outputs differ.");
                    throw new Exception("Visual output mismatch between PDF and SVG.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }