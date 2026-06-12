using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the pattern to look for in user‑defined cell names
                string pattern = "MyCustomCell";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Users collection is present
                        if (shape.Users == null)
                            continue;

                        // Check if any user‑defined cell name contains the pattern
                        bool matchesPattern = false;
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name != null && userCell.Name.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                            {
                                matchesPattern = true;
                                break;
                            }
                        }

                        // Process only the shapes that matched the pattern
                        if (matchesPattern)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" matches pattern \"{pattern}\".");
                            // Place additional processing logic here
                        }
                    }
                }

                // Optionally save the diagram after processing
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }