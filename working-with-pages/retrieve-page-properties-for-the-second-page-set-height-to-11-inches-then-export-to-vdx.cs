using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file
                // Replace "input.vsdx" with the actual path to your diagram file
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Ensure there are at least two pages
                    if (diagram.Pages.Count < 2)
                    {
                        Console.WriteLine("The diagram does not contain a second page.");
                        return;
                    }

                    // Retrieve the second page (index 1)
                    Page secondPage = diagram.Pages[1];

                    // Read current page dimensions
                    double currentWidth = secondPage.PageSheet.PageProps.PageWidth.Value;
                    double currentHeight = secondPage.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"Original size - Width: {currentWidth} inches, Height: {currentHeight} inches");

                    // Set the page height to 11 inches (width remains unchanged)
                    secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;
                    Console.WriteLine($"Updated height to 11 inches. New size - Width: {currentWidth} inches, Height: {secondPage.PageSheet.PageProps.PageHeight.Value} inches");

                    // Save the modified diagram as VDX format
                    // Replace "output.vdx" with the desired output path
                    diagram.Save("output.vdx", SaveFileFormat.Vdx);
                    Console.WriteLine("Diagram saved as VDX successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }