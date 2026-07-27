using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the document title from built‑in properties
                string title = diagram.DocumentProps.Title;

                // Set the header text (centered) to the document title
                diagram.HeaderFooter.HeaderCenter = title;

                // Configure the header/footer font: Arial, 12 pt
                // Height uses a negative mapping: Height = -(PointSize * 1.333) ≈ -16 for 12 pt
                diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
                diagram.HeaderFooter.HeaderFooterFont.Height = -16; // 12 pt
                diagram.HeaderFooter.HeaderFooterFont.Weight = 400; // Normal weight

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }