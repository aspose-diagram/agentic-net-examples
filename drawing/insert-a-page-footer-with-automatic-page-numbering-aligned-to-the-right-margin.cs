using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram (no using block as Diagram is not disposable per rules)
                Diagram diagram = new Diagram(inputPath);

                // Set the right-aligned footer with automatic page numbering.
                // '&p' is the Visio field code for the current page number.
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Optional: adjust the distance of the footer from the page edge (in inches).
                diagram.HeaderFooter.FooterMargin.Value = 0.5; // half‑inch margin

                // Save the updated diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }