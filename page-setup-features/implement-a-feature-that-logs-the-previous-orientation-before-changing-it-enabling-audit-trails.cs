using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input path, output path, desired orientation.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <orientation>");
            Console.Error.WriteLine("Orientation values: Landscape, Portrait, SameAsPrinter");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string orientationArg = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the desired orientation string into the corresponding enum value.
        PrintPageOrientationValue newOrientation;
        if (!Enum.TryParse(orientationArg, ignoreCase: true, out newOrientation) ||
            (newOrientation != PrintPageOrientationValue.Landscape &&
             newOrientation != PrintPageOrientationValue.Portrait &&
             newOrientation != PrintPageOrientationValue.SameAsPrinter))
        {
            Console.Error.WriteLine($"Invalid orientation value: {orientationArg}");
            Console.Error.WriteLine("Valid values are: Landscape, Portrait, SameAsPrinter");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram from the specified file.
            diagram = new Diagram(inputPath);

            // Iterate through all pages to log and change orientation.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the current orientation.
                PrintPageOrientationValue currentOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                // Log the previous orientation for audit purposes.
                Console.WriteLine($"Page ID {page.ID} ('{page.Name}') current orientation: {currentOrientation}");

                // Apply the new orientation.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = newOrientation;

                // Log the change.
                Console.WriteLine($"Page ID {page.ID} orientation changed to: {newOrientation}");
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
        finally
        {
            // Ensure resources are released.
            if (diagram != null)
            {
                diagram.Dispose();
            }
        }
    }
}