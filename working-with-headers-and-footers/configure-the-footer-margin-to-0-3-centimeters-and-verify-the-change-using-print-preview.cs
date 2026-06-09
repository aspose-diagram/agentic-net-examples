using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Convert 0.3 centimeters to inches (1 cm = 0.393701 inches)
            double centimeters = 0.3;
            double inches = centimeters * 0.393701;

            // Set the global footer margin (value is in inches)
            diagram.HeaderFooter.FooterMargin.Value = inches;

            // Verify that the margin was applied correctly
            double actualMargin = diagram.HeaderFooter.FooterMargin.Value;
            if (Math.Abs(actualMargin - inches) > 0.0001)
            {
                throw new Exception($"Footer margin verification failed. Expected {inches} inches, but got {actualMargin} inches.");
            }
            else
            {
                Console.WriteLine($"Footer margin successfully set to {actualMargin} inches ({centimeters} cm).");
            }

            // Invoke print preview (Print method with PrintSaveOptions)
            PrintSaveOptions previewOptions = new PrintSaveOptions();
            diagram.Print(previewOptions);

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
