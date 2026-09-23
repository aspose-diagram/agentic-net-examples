using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // (Assuming the file path is provided; replace with actual path)
                string diagramPath = @"C:\Diagrams\SampleDiagram.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Example: Rename each page by appending its index
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Access the page
                    Page page = diagram.Pages[i];

                    // New name (you can apply any renaming logic here)
                    string newName = $"Page_{i + 1}";

                    // Assign the new name
                    page.Name = newName;
                }

                // Validate that all page names are unique
                ValidateUniquePageNames(diagram);

                // (Optional) Save the diagram if needed
                // diagram.Save(@"C:\Diagrams\SampleDiagram_Renamed.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Checks that each page in the diagram has a unique name.
        /// Throws an exception if duplicate names are found.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram Diagram instance.</param>
        private static void ValidateUniquePageNames(Diagram diagram)
        {
            // Use a HashSet to track encountered page names
            HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Page page in diagram.Pages)
            {
                // If the name already exists in the set, it's a duplicate
                if (!pageNames.Add(page.Name))
                {
                    throw new InvalidOperationException(
                        $"Duplicate page name detected: \"{page.Name}\". All page names must be unique.");
                }
            }

            // If execution reaches here, all names are unique
            Console.WriteLine("All page names are unique.");
        }
    }