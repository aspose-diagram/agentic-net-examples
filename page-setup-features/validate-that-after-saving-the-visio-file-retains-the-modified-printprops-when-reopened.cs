using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Create a new diagram and add a page
        Diagram diagram = new Diagram();
        diagram.Pages.Add(new Page());

        // Access the first page
        Page page = diagram.Pages[0];

        // Modify PrintProps
        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
        page.PageSheet.PrintProps.ScaleY.Value = 0.75;
        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
        page.PageSheet.PrintProps.PagesX.Value = 1;
        page.PageSheet.PrintProps.PagesY.Value = 1;
        page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;    // inches
        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
        page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

        // Save the diagram to a file
        string filePath = "modified.vsdx";
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // Reload the diagram
        Diagram loadedDiagram = new Diagram(filePath);
        Page loadedPage = loadedDiagram.Pages[0];

        // Validate that the PrintProps were retained
        if (loadedPage.PageSheet.PrintProps.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape)
            throw new Exception("PrintPageOrientation was not retained.");

        if (Math.Abs(loadedPage.PageSheet.PrintProps.ScaleX.Value - 0.75) > 0.0001)
            throw new Exception("ScaleX was not retained.");

        if (Math.Abs(loadedPage.PageSheet.PrintProps.ScaleY.Value - 0.75) > 0.0001)
            throw new Exception("ScaleY was not retained.");

        if (loadedPage.PageSheet.PrintProps.OnPage.Value != BOOL.True)
            throw new Exception("OnPage flag was not retained.");

        if (loadedPage.PageSheet.PrintProps.PagesX.Value != 1 || loadedPage.PageSheet.PrintProps.PagesY.Value != 1)
            throw new Exception("PagesX/PagesY were not retained.");

        if (Math.Abs(loadedPage.PageSheet.PrintProps.PageTopMargin.Value - 0.5) > 0.0001 ||
            Math.Abs(loadedPage.PageSheet.PrintProps.PageBottomMargin.Value - 0.5) > 0.0001 ||
            Math.Abs(loadedPage.PageSheet.PrintProps.PageLeftMargin.Value - 0.5) > 0.0001 ||
            Math.Abs(loadedPage.PageSheet.PrintProps.PageRightMargin.Value - 0.5) > 0.0001)
            throw new Exception("Page margins were not retained.");

        Console.WriteLine("PrintProps retained after save and reload successfully.");
    }
}
