using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Capture the original pages to avoid modifying the collection while iterating
                    int originalPageCount = diagram.Pages.Count;
                    Page[] originalPages = new Page[originalPageCount];
                    int index = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        originalPages[index++] = p;
                    }

                    // Duplicate each original page
                    foreach (Page srcPage in originalPages)
                    {
                        // Determine the next available page ID
                        int maxId = 0;
                        foreach (Page p in diagram.Pages)
                        {
                            if (p.ID > maxId)
                                maxId = p.ID;
                        }

                        // Create a new page and set its ID and name
                        Page newPage = new Page();
                        newPage.ID = maxId + 1;
                        newPage.Name = srcPage.Name + "_Copy";

                        // Copy the contents of the source page into the new page
                        newPage.Copy(srcPage);

                        // Add the new page to the diagram
                        diagram.Pages.Add(newPage);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully to: " + outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }