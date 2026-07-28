using System.IO;
using System;
using Aspose.Diagram;

public class DiagramHelper
{
    /// <summary>
    /// Removes all hyperlinks from every shape on the specified page and saves the diagram.
    /// </summary>
    /// <param name="inputFile">Path to the source Visio file.</param>
    /// <param name="pageName">Name of the page from which hyperlinks should be removed.</param>
    /// <param name="outputFile">Path where the modified Visio file will be saved.</param>
    public void RemoveHyperlinksFromPage(string inputFile, string pageName, string outputFile)
    {
        // Load the diagram (lifecycle rule: load)
        Diagram diagram = new Diagram(inputFile);

        // Find the target page by name
        Page targetPage = null;
        foreach (Page page in diagram.Pages)
        {
            if (string.Equals(page.Name, pageName, StringComparison.OrdinalIgnoreCase))
            {
                targetPage = page;
                break;
            }
        }

        if (targetPage == null)
        {
            throw new ArgumentException($"Page '{pageName}' not found in the diagram.");
        }

        // Iterate through all shapes on the page and clear their hyperlink collections
        foreach (Shape shape in targetPage.Shapes)
        {
            // The Hyperlinks property returns a HyperlinkCollection.
            // Calling Clear removes all hyperlink elements from the shape.
            shape.Hyperlinks.Clear();
        }

        // Save the modified diagram (lifecycle rule: save)
        diagram.Save(outputFile, SaveFileFormat.Vdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramHelper();
            obj.RemoveHyperlinksFromPage("", "", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
