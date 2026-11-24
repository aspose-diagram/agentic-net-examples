using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page to add a footer shape.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define footer shape size.
                    double footerWidth = pageWidth;      // full width
                    double footerHeight = 0.3;           // modest height

                    // Position the footer at the bottom center of the page.
                    double pinX = pageWidth / 2.0;       // center horizontally
                    double pinY = footerHeight / 2.0;    // a little above the bottom edge

                    // Add a text shape (initially empty) to the page.
                    Shape footerShape = page.AddText(pinX, pinY, footerWidth, footerHeight, string.Empty);

                    // Clear any existing text runs.
                    footerShape.Text.Value.Clear();

                    // Create a field that inserts the current page number (&p).
                    Field pageNumberField = new Field();
                    pageNumberField.Value.Val = "&p";

                    // Add the field to the shape's Fields collection.
                    footerShape.Fields.Add(pageNumberField);

                    // Optional: center the text horizontally within the shape.
                    // Visio aligns text by default; additional formatting can be applied here if required.
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}