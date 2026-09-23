using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Define custom page size in inches
        double customWidth = 8.5;   // example width
        double customHeight = 11.0; // example height

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page; add if none exist
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Access the first page and set its custom size
        Page page = diagram.Pages[0];
        page.PageSheet.PageProps.PageWidth.Value = customWidth;
        page.PageSheet.PageProps.PageHeight.Value = customHeight;

        // Save the diagram to a VSDX file
        string filePath = "customPageSize.vsdx";
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // Load the saved diagram
        Diagram loadedDiagram = new Diagram(filePath);

        // Retrieve the page size from the loaded diagram
        Page loadedPage = loadedDiagram.Pages[0];
        double loadedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
        double loadedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

        // Validate that the saved and loaded sizes match (tolerance for floating‑point)
        double tolerance = 0.001;
        bool widthMatches = Math.Abs(customWidth - loadedWidth) < tolerance;
        bool heightMatches = Math.Abs(customHeight - loadedHeight) < tolerance;

        if (widthMatches && heightMatches)
        {
            Console.WriteLine("Success: Custom page size retained after saving and loading.");
        }
        else
        {
            throw new Exception($"Page size mismatch. Expected ({customWidth} x {customHeight}), " +
                                $"but loaded ({loadedWidth} x {loadedHeight}).");
        }
    }
}
