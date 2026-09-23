using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Define margin size: 20 points = 20/72 inches
            double marginInches = 20.0 / 72.0;

            // Apply the margin to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                printProps.PageTopMargin.Value = marginInches;
                printProps.PageBottomMargin.Value = marginInches;
                printProps.PageLeftMargin.Value = marginInches;
                printProps.PageRightMargin.Value = marginInches;
            }

            // Example shape drawing after margins are set
            // Draw a simple rectangle on the first page
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                // Parameters: pinX, pinY, width, height (all in inches)
                firstPage.DrawRectangle(1.0, 1.0, 2.0, 1.0);
            }

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
