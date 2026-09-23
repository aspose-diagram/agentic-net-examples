using System.IO;
using System;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Simulate an external dataset with some records
        DataTable table = new DataTable();
        table.Columns.Add("Category", typeof(string));
        table.Columns.Add("Value", typeof(int));

        table.Rows.Add("Alpha", 10);
        table.Rows.Add("Beta", 20);
        table.Rows.Add("Alpha", 15);
        table.Rows.Add("Gamma", 5);

        // Determine distinct categories (each will become a page)
        HashSet<string> distinctCategories = new HashSet<string>();
        foreach (DataRow row in table.Rows)
        {
            distinctCategories.Add(row["Category"].ToString());
        }

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Find the current maximum page ID to assign unique IDs to new pages
        int maxPageId = 0;
        foreach (Page existingPage in diagram.Pages)
        {
            if (existingPage.ID > maxPageId)
                maxPageId = existingPage.ID;
        }

        // Add a new page for each distinct category
        foreach (string category in distinctCategories)
        {
            maxPageId++;
            Page newPage = new Page(maxPageId);
            newPage.Name = category;
            diagram.Pages.Add(newPage);

            // Add a simple rectangle shape to the page and label it with the category name
            double pinX = 2.0;   // X coordinate of the shape's center
            double pinY = 2.0;   // Y coordinate of the shape's center
            double width = 4.0;  // Width of the rectangle
            double height = 2.0; // Height of the rectangle

            long shapeId = newPage.DrawRectangle(pinX, pinY, width, height);
            Shape shape = newPage.Shapes.GetShape((int)shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(category));
        }

        // Save the diagram with all generated pages
        diagram.Save("DynamicPages.vsdx", SaveFileFormat.Vsdx);
    }
}
