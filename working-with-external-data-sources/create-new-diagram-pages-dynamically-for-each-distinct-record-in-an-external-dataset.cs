using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the template Visio file (must exist)
        string templatePath = "template.vsdx";
        if (!System.IO.File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }
        // Path where the resulting diagram will be saved
        string outputPath = "output.vsdx";

        // Guard against missing template file
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Simulate external dataset using an in‑memory DataTable
        // In a real scenario replace this block with actual data retrieval logic
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Category", typeof(string));
        dataTable.Columns.Add("Value", typeof(int));

        // Sample rows
        dataTable.Rows.Add("Finance", 100);
        dataTable.Rows.Add("HR", 50);
        dataTable.Rows.Add("IT", 200);
        dataTable.Rows.Add("Finance", 150); // Duplicate category to test distinct handling

        // Extract distinct values from the "Category" column
        HashSet<string> distinctCategories = new HashSet<string>();
        foreach (DataRow row in dataTable.Rows)
        {
            if (row["Category"] != DBNull.Value)
                distinctCategories.Add(row["Category"].ToString());
        }

        // Load the template diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(templatePath))
            {
                // Determine the current maximum Page ID to assign unique IDs to new pages
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new page for each distinct category
                foreach (string category in distinctCategories)
                {
                    // Increment the page ID for uniqueness
                    maxPageId++;

                    // Initialize a new Page instance
                    Page newPage = new Page();
                    newPage.ID = maxPageId;
                    newPage.Name = category; // Use the category name as the page name

                    // Add the newly created page to the diagram
                    diagram.Pages.Add(newPage);
                }

                // Save the modified diagram to the output file using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}