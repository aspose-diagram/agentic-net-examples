using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages to adjust the footer.
        foreach (Page page in diagram.Pages)
        {
            // Retrieve the page height (in inches).
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define a vertical offset from the bottom edge.
            // Example: place the footer 0.5 inches above the bottom margin.
            double footerOffset = 0.5;

            // Optionally, you could compute the offset based on page height.
            // For demonstration, we set FooterMargin to the desired offset.
            diagram.HeaderFooter.FooterMargin.Value = footerOffset;

            // Set footer text. Use Visio field code '&p' for the current page number.
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // If you need a different footer per page, you can customize it here.
            // Example: include the page name.
            // diagram.HeaderFooter.FooterLeft = $"Page: {page.Name}";
        }

        // Save the modified diagram back to a Visio file.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
