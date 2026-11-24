using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, desired orientation
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <Orientation: Landscape|Portrait|SameAsPrinter>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string orientationArg = args[2];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse desired orientation
        PrintPageOrientationValue newOrientation;
        if (!Enum.TryParse(orientationArg, true, out newOrientation) ||
            (newOrientation != PrintPageOrientationValue.Landscape &&
             newOrientation != PrintPageOrientationValue.Portrait &&
             newOrientation != PrintPageOrientationValue.SameAsPrinter))
        {
            Console.Error.WriteLine("Invalid orientation. Allowed values: Landscape, Portrait, SameAsPrinter");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page, log previous orientation, then set new orientation
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve current orientation
                    PrintPageOrientationValue previousOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                    // Log audit information
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, Previous Orientation: {previousOrientation}");

                    // Set the new orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = newOrientation;
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}