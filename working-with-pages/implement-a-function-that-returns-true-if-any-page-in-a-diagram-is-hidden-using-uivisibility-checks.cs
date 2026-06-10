using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramHelper
{
    // Returns true if any page in the diagram is hidden (UIVisibility set to Hidden)
    public static bool AnyPageHidden(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        foreach (Page page in diagram.Pages)
        {
            if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
            {
                return true;
            }
        }
        return false;
    }
}

public class Program
{
    public static void Main()
    {
        string filePath = "sample.vsdx";
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(filePath);
            bool hasHiddenPage = DiagramHelper.AnyPageHidden(diagram);
            Console.WriteLine(hasHiddenPage
                ? "The diagram contains at least one hidden page."
                : "No hidden pages found in the diagram.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}