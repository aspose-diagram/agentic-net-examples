using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there are at least three pages
                if (diagram.Pages.Count < 3)
                {
                    throw new Exception("The diagram must contain at least three pages.");
                }

                // Remove the third page (zero‑based index 2)
                Page pageToRemove = diagram.Pages[2];
                diagram.Pages.Remove(pageToRemove);

                // Renumber remaining pages sequentially (starting from 1)
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    page.ID = i + 1; // Set new sequential ID

                    // Optional: update the page name to reflect new order
                    page.Name = $"Page-{i + 1}";
                    page.NameU = page.Name;
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