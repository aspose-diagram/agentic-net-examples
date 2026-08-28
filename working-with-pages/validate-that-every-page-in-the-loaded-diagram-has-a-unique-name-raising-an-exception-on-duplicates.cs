using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the diagram (replace with your actual file path)
                var diagram = new Diagram("input.vsdx");

                // Validate that each page has a unique universal name (NameU)
                ValidateUniquePageNames(diagram);

                // If validation passes, you can continue processing or save the diagram
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Checks that all pages in the diagram have distinct NameU values.
        /// Throws an InvalidOperationException if a duplicate is found.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram Diagram instance to validate.</param>
        static void ValidateUniquePageNames(Diagram diagram)
        {
            // Use a HashSet to track encountered page names efficiently
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Iterate through the PageCollection
            foreach (Page page in diagram.Pages)
            {
                string pageName = page.NameU ?? string.Empty;

                // If the name already exists, raise an exception
                if (!seenNames.Add(pageName))
                {
                    throw new InvalidOperationException(
                        $"Duplicate page name detected: \"{pageName}\". Each page must have a unique NameU.");
                }
            }
        }
    }