using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace SharePointDiagramIntegration
{
    // Simple DTO representing a SharePoint list item
    public class SharePointItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    class Program
    {
        // Placeholder method that simulates retrieving items from a SharePoint list.
        // In a real scenario, replace this with actual SharePoint client code.
        private static List<SharePointItem> GetSharePointListItems()
        {
            // Mock data for demonstration purposes
            return new List<SharePointItem>
            {
                new SharePointItem { Title = "Item 1", Description = "First item description" },
                new SharePointItem { Title = "Item 2", Description = "Second item description" },
                new SharePointItem { Title = "Item 3", Description = "Third item description" }
            };
        }

        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve the first page (or any specific page you need)
                Page page = diagram.Pages[0];

                // Get SharePoint data
                List<SharePointItem> items = GetSharePointListItems();

                // Simple mapping: each SharePoint item updates a shape whose NameU matches "ItemShape{index}"
                for (int i = 0; i < items.Count; i++)
                {
                    string targetShapeName = $"ItemShape{i + 1}"; // Expected shape name in the diagram
                    Shape targetShape = null;

                    // Locate the shape by its universal name
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape '{targetShapeName}' not found. Skipping item {i + 1}.");
                        continue;
                    }

                    // Update the shape's text with the SharePoint item's title
                    targetShape.Text.Value.Clear();
                    targetShape.Text.Value.Add(new Txt(items[i].Title));

                    // Optionally, store the description in a custom user-defined cell
                    // First, check if the cell already exists
                    User descriptionCell = null;
                    foreach (User userCell in targetShape.Users)
                    {
                        if (userCell.Name.Equals("Description", StringComparison.OrdinalIgnoreCase))
                        {
                            descriptionCell = userCell;
                            break;
                        }
                    }

                    if (descriptionCell == null)
                    {
                        // Create a new user-defined cell
                        descriptionCell = new User();
                        descriptionCell.Name = "Description";
                        targetShape.Users.Add(descriptionCell);
                    }

                    descriptionCell.Value.Val = items[i].Description;
                }

                // Refresh data connections (if any) to ensure external data is synchronized
                diagram.Refresh();

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram updated and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}