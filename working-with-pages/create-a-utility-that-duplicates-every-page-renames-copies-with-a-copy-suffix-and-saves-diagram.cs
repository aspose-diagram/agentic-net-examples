using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input file path and output file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPageDuplicator <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Capture the original pages to avoid modifying the collection while iterating
                var originalPages = new Page[diagram.Pages.Count];
                int index = 0;
                foreach (Page p in diagram.Pages)
                {
                    originalPages[index++] = p;
                }

                // Determine the current maximum page ID
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Duplicate each original page
                foreach (Page srcPage in originalPages)
                {
                    // Create a new page with a unique ID
                    Page newPage = new Page();
                    newPage.ID = ++maxPageId;

                    // Copy the content from the source page
                    newPage.Copy(srcPage);

                    // Rename the copied page
                    newPage.Name = srcPage.Name + "_Copy";
                    newPage.NameU = srcPage.NameU + "_Copy";

                    // Add the new page to the diagram
                    diagram.Pages.Add(newPage);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    }