using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the cleaned Visio file
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Collect user-defined cells that have empty or whitespace values
                            List<User> cellsToRemove = new List<User>();
                            foreach (User userCell in shape.Users)
                            {
                                // userCell.Value may be null; guard against it
                                string cellValue = userCell.Value?.Val;
                                if (string.IsNullOrWhiteSpace(cellValue))
                                {
                                    cellsToRemove.Add(userCell);
                                }
                            }

                            // Remove the identified empty cells from the shape
                            foreach (User userCell in cellsToRemove)
                            {
                                shape.Users.Remove(userCell);
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("All empty user-defined cells have been removed and the diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }