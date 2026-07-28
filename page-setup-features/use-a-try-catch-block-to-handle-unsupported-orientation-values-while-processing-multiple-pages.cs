using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the diagram from a file
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Sample orientation values for each page (the third value is unsupported)
                    string[] orientations = { "Landscape", "Portrait", "Diagonal" };

                    int pageIndex = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        // Determine the orientation string for the current page
                        string orientationStr = pageIndex < orientations.Length ? orientations[pageIndex] : "Portrait";

                        try
                        {
                            // Attempt to parse the string to the enum
                            if (!Enum.TryParse<PrintPageOrientationValue>(orientationStr, true, out var orientation))
                            {
                                throw new ArgumentException($"Unsupported orientation value: {orientationStr}");
                            }

                            // Apply the parsed orientation to the page's print properties
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                            Console.WriteLine($"Page '{page.Name}' orientation set to {orientation}.");
                        }
                        catch (Exception ex)
                        {
                            // Handle any errors (e.g., unsupported orientation)
                            Console.WriteLine($"Error processing page '{page.Name}': {ex.Message}");
                            // Fallback to a default orientation
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }

                        pageIndex++;
                    }

                    // Save the modified diagram
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved to 'output.vsdx'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }