using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the exported file (HTML in this example)
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Build a metadata string containing page dimensions
                // Example format: "Page 1: Width=8.5in, Height=11in"
                string metadata = "";
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    metadata += $"Page {page.ID}: Width={width}in, Height={height}in; ";
                }

                // Embed the metadata into the document header/footer.
                // Using the right side of the footer for visibility.
                diagram.HeaderFooter.FooterRight = metadata.Trim();

                // Configure HTML save options (you can choose other formats similarly)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Ensure the default font is set to avoid missing glyphs
                htmlOptions.DefaultFont = "Arial";

                // Save the diagram with the embedded metadata
                diagram.Save(outputPath, htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }