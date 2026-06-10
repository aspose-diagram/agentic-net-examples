using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input file path and output file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPageDuplicator <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram using the built‑in constructor (lifecycle rule)
            Diagram diagram = new Diagram(inputPath);

            // Collect original pages to avoid modifying the collection while iterating
            List<Page> originalPages = new List<Page>();
            foreach (Page page in diagram.Pages)
            {
                originalPages.Add(page);
            }

            // Determine the current maximum page ID
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Duplicate each original page
            foreach (Page srcPage in originalPages)
            {
                // Create a new blank page and assign a new unique ID
                Page newPage = new Page(maxPageId + 1);
                maxPageId++;

                // Copy the contents of the source page into the new page
                newPage.Copy(srcPage);

                // Rename the copied page
                newPage.Name = srcPage.Name + "_Copy";

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);
            }

            // Save the modified diagram using the correct overload (save rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }