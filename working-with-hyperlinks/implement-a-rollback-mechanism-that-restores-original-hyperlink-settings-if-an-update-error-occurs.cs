using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class HyperlinkRollbackExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Example: work with the first page and first shape
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Preserve original hyperlink settings by cloning each hyperlink
            List<Hyperlink> originalLinks = new List<Hyperlink>();
            foreach (Hyperlink hl in shape.Hyperlinks)
            {
                originalLinks.Add((Hyperlink)hl.Clone());
            }

            try
            {
                // Update hyperlink settings (example: change the address)
                foreach (Hyperlink hl in shape.Hyperlinks)
                {
                    hl.Address.Value = "https://new.example.com";
                }

                // Perform operations that might fail
                PerformRiskyOperation(diagram);
            }
            catch (Exception ex)
            {
                // Rollback: restore the original hyperlinks
                shape.Hyperlinks.Clear();
                foreach (Hyperlink hl in originalLinks)
                {
                    shape.Hyperlinks.Add(hl);
                }

                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Original hyperlink settings have been restored.");
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder for any operation that could throw an exception
    static void PerformRiskyOperation(Diagram diagram)
    {
        // Simulate a failure
        throw new InvalidOperationException("Simulated operation failure.");
    }
}
