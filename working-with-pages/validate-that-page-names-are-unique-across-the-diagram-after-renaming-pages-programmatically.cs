using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string sourcePath = "input.vsdx";
                string destinationPath = "output.vsdx";

                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(sourcePath);

                    // Example renaming: assign a new name to each page based on its index
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        Page page = diagram.Pages[i];
                        page.Name = $"Page_{i + 1}";
                    }

                    // Validate that all page names are unique after renaming
                    ValidateUniquePageNames(diagram);

                    // Save the updated diagram
                    diagram.Save(destinationPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully with unique page names.");
                }
                catch (Exception ex)
                {
                    // Report any validation or processing errors
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Checks that each page in the diagram has a distinct name.
        /// Throws an exception if a duplicate name is found.
        /// </summary>
        /// <param name="diagram">The diagram to validate.</param>
        private static void ValidateUniquePageNames(Diagram diagram)
        {
            var nameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Page page in diagram.Pages)
            {
                if (!nameSet.Add(page.Name))
                {
                    // Duplicate detected
                    throw new Exception($"Duplicate page name detected: \"{page.Name}\"");
                }
            }
        }
    }