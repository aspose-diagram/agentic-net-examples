using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Ensure at least one page exists
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Lambda expression: set width based on page index parity
            diagram.Pages
                .Cast<Page>()
                .Select((page, index) =>
                {
                    // Even index -> 8.5 inches, odd index -> 11 inches
                    page.PageSheet.PageProps.PageWidth.Value = (index % 2 == 0) ? 8.5 : 11.0;
                    return page;
                })
                .ToList(); // Force execution

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
