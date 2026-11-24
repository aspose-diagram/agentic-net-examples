using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simple class to represent a SharePoint list item (simulated)
    class SharePointItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    static void Main(string[] args)
    {
        // Input Visio diagram path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio diagram path
        string outputPath = "output.vsdx";

        // Simulate retrieval of SharePoint list items
        List<SharePointItem> sharePointItems = GetMockSharePointItems();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master (to avoid null reference)
                        if (shape.Master == null)
                            continue;

                        // Attempt to find a matching SharePoint item by shape name
                        SharePointItem matchingItem = FindItemByTitle(sharePointItems, shape.NameU);

                        if (matchingItem != null)
                        {
                            // Update the shape's text with the SharePoint item's description
                            // Clear existing text runs
                            shape.Text.Value.Clear();

                            // Add new text run
                            shape.Text.Value.Add(new Txt(matchingItem.Description));

                            // Refresh the shape's data to apply changes
                            shape.RefreshData();
                        }
                    }
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to simulate fetching SharePoint list items
    static List<SharePointItem> GetMockSharePointItems()
    {
        // In a real scenario, replace this with actual SharePoint SDK calls.
        // Here we create sample items for demonstration purposes.
        return new List<SharePointItem>
        {
            new SharePointItem { Title = "Shape1", Description = "First item description" },
            new SharePointItem { Title = "Shape2", Description = "Second item description" },
            new SharePointItem { Title = "Shape3", Description = "Third item description" }
        };
    }

    // Helper method to find a SharePoint item by title (case-insensitive)
    static SharePointItem FindItemByTitle(List<SharePointItem> items, string title)
    {
        if (string.IsNullOrEmpty(title))
            return null;

        foreach (SharePointItem item in items)
        {
            if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
                return item;
        }
        return null;
    }
}