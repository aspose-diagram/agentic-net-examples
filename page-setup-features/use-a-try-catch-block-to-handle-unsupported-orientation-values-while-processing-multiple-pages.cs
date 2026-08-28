using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file.
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define desired orientations for each page (by index).
                var orientations = new Dictionary<int, string>
                {
                    { 0, "Landscape" },
                    { 1, "Portrait" },
                    { 2, "InvalidOrientation" } // This will trigger the catch block.
                };

                // Iterate through pages using an index to match the dictionary.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    string orientationStr;

                    // Use the specified orientation if present; otherwise default.
                    if (!orientations.TryGetValue(i, out orientationStr))
                    {
                        orientationStr = "SameAsPrinter";
                    }

                    try
                    {
                        // Apply orientation based on the string value.
                        switch (orientationStr)
                        {
                            case "Landscape":
                                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                                break;
                            case "Portrait":
                                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                                break;
                            case "SameAsPrinter":
                                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;
                                break;
                            default:
                                // Unsupported value – raise an exception to be caught.
                                throw new ArgumentException($"Unsupported orientation: {orientationStr}");
                        }

                        Console.WriteLine($"Page {i} orientation set to {orientationStr}.");
                    }
                    catch (ArgumentException ex)
                    {
                        // Handle unsupported orientation values gracefully.
                        Console.WriteLine($"Error processing page {i}: {ex.Message}");
                        // Fallback to a safe default orientation.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;
                    }
                }

                // Save the modified diagram.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
