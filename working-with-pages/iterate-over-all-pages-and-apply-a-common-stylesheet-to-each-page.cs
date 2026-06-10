using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ApplyCommonStyleToPages
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the IDs of the common styles to apply.
            // These IDs should correspond to styles already present in the diagram's StyleSheets collection.
            // For example, assume Text style ID = 1, Line style ID = 2, Fill style ID = 3.
            int commonTextStyleId = 1;
            int commonLineStyleId = 2;
            int commonFillStyleId = 3;

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Apply the common stylesheet to the current page.
                // ApplyStyle uses -1 for any style you wish to leave unchanged.
                page.ApplyStyle(commonTextStyleId, commonLineStyleId, commonFillStyleId);
            }

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
