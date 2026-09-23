using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all pages (1‑based page numbers)
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    int pageNumber = i + 1; // Visio page numbers start at 1

                    // Apply a custom ScaleX to odd‑numbered pages
                    if (pageNumber % 2 == 1) // odd page
                    {
                        // Example: scale to 50% width
                        page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                    }
                    else
                    {
                        // Even pages keep the default scale (1.0)
                        page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
