using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output diagram path
        string outputPath = "GeneratedDiagram.vsdx";

        // Simulate external dataset using an in‑memory DataTable
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Category", typeof(string));
        dataTable.Columns.Add("Value", typeof(int));

        // Sample data with duplicate categories
        dataTable.Rows.Add("Finance", 100);
        dataTable.Rows.Add("HR", 50);
        dataTable.Rows.Add("Finance", 150);
        dataTable.Rows.Add("IT", 200);
        dataTable.Rows.Add("HR", 75);

        // Extract distinct categories
        List<string> distinctCategories = dataTable.AsEnumerable()
                                                   .Select(row => row.Field<string>("Category"))
                                                   .Distinct()
                                                   .ToList();

        // Create a new diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram())
            {
                // Add a window to avoid errors when the diagram has no windows
                Window window = new Window();
                window.WindowType = WindowTypeValue.Drawing;
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1100;
                window.WindowHeight = 700;
                diagram.Windows.Add(window);

                // Determine the current maximum page ID to assign unique IDs
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new page for each distinct category
                foreach (string category in distinctCategories)
                {
                    // Increment page ID
                    maxPageId++;

                    // Initialize a new page and set its properties
                    Page newPage = new Page();
                    newPage.ID = maxPageId;
                    newPage.Name = category;

                    // Add the page to the diagram
                    diagram.Pages.Add(newPage);

                    // Add a simple text shape to the page to identify it
                    // Parameters: pinX, pinY, width, height, text
                    Shape textShape = newPage.AddText(1.0, 1.0, 5.0, 1.0, $"Category: {category}");
                }

                // Save the resulting diagram to the specified file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}