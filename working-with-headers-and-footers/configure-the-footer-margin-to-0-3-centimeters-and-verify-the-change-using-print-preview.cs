using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for ImageSaveOptions

class Program
{
    static void Main(string[] args)
    {
        // Paths for input, output and preview image
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "output.vsdx";
        string previewPath = "preview.png";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Convert 0.3 centimeters to inches (1 cm = 0.393701 inches)
            double cmToInch = 0.393701;
            double footerMarginInInches = 0.3 * cmToInch;

            // Set the global footer margin (value is in inches)
            diagram.HeaderFooter.FooterMargin.Value = footerMarginInInches;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Verify that the margin was set correctly
            double actualMargin = diagram.HeaderFooter.FooterMargin.Value;
            if (Math.Abs(actualMargin - footerMarginInInches) > 0.0001)
            {
                throw new Exception($"Footer margin verification failed. Expected {footerMarginInInches}, but got {actualMargin}.");
            }
            else
            {
                Console.WriteLine($"Footer margin successfully set to {actualMargin} inches.");
            }

            // Generate a preview image (first page) to visually confirm the change
            ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Optionally, set the page index if needed (default is first page)
            previewOptions.PageIndex = 0;
            diagram.Save(previewPath, previewOptions);
            Console.WriteLine($"Print preview saved to '{previewPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}