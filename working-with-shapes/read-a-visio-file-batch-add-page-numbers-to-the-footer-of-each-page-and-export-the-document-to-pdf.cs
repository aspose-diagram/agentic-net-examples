using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for input Visio file path
        Console.Write("Enter the path to the Visio file: ");
        string inputPath = Console.ReadLine();

        // Prompt user for output PDF file path
        Console.Write("Enter the desired output PDF path: ");
        string outputPath = Console.ReadLine();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set the footer to display the page number on each page
            // '&p' is the Visio field code for the current page number
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Optional: adjust footer margin if needed (in inches)
            diagram.HeaderFooter.FooterMargin.Value = 0.2;

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("PDF exported successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
