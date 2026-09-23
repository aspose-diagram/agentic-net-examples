using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Paths to the source and destination Visio files
        const string inputPath = "input.vsdx";
        const string outputPath = "output.vsdx";

        try
        {
            // Read desired page dimensions from the console
            double newWidth = ReadPositiveDouble("Enter new page width (in inches): ");
            double newHeight = ReadPositiveDouble("Enter new page height (in inches): ");

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply the new dimensions to each page
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }
        }
        catch (ArgumentException ex)
        {
            // Handle validation errors for page size values
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Reads a positive double value from the console; throws if invalid
    static double ReadPositiveDouble(string prompt)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        if (double.TryParse(input, out double value) && value > 0)
        {
            return value;
        }

        throw new ArgumentException("Page size must be a positive numeric value.");
    }
}
