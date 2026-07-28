using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Define the pattern to match user-defined cell names (e.g., names starting with "MyCell")
                string pattern = @"^MyCell";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        bool matchesPattern = false;

                        // Check each user-defined cell (User) in the shape
                        if (shape.Users != null)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                // Match against Name or NameU
                                if (Regex.IsMatch(userCell.Name ?? string.Empty, pattern) ||
                                    Regex.IsMatch(userCell.NameU ?? string.Empty, pattern))
                                {
                                    matchesPattern = true;
                                    break;
                                }
                            }
                        }

                        // Process only shapes that contain a matching user-defined cell
                        if (matchesPattern)
                        {
                            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, Page: {page.Name}");
                            // Additional processing logic for the matched shape can be placed here
                        }
                    }
                }

                // Optional: Save the diagram if modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }