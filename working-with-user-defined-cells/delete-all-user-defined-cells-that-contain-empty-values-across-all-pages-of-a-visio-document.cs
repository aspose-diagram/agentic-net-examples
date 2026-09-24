using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the cleaned Visio file
            string outputPath = "output_cleaned.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Users collection
                    if (shape.Users == null)
                        continue;

                    // Collect user-defined cells with empty values
                    List<User> usersToRemove = new List<User>();
                    foreach (User userCell in shape.Users)
                    {
                        // userCell.Value may be null; guard against it
                        string cellValue = userCell.Value?.Val;
                        if (string.IsNullOrWhiteSpace(cellValue))
                        {
                            usersToRemove.Add(userCell);
                        }
                    }

                    // Remove the identified empty user-defined cells
                    foreach (User userCell in usersToRemove)
                    {
                        shape.Users.Remove(userCell);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
