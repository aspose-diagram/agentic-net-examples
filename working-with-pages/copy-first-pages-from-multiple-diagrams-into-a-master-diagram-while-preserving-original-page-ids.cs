using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // List of source diagram file paths
                List<string> sourceFiles = new List<string>
                {
                    "Diagram1.vsdx",
                    "Diagram2.vsdx",
                    "Diagram3.vsdx"
                };

                // Create an empty master diagram
                Diagram masterDiagram = new Diagram();

                // Remove the default empty page that is created with a new Diagram
                if (masterDiagram.Pages.Count > 0)
                {
                    Page defaultPage = masterDiagram.Pages[0];
                    masterDiagram.Pages.Remove(defaultPage);
                }

                // Keep track of page IDs already added to avoid duplicates
                HashSet<int> usedPageIds = new HashSet<int>();

                foreach (string filePath in sourceFiles)
                {
                    // Load source diagram
                    Diagram srcDiagram = new Diagram(filePath);

                    // Copy masters from source to master diagram
                    foreach (Master srcMaster in srcDiagram.Masters)
                    {
                        // Add master by name; if already exists, AddMaster will ignore duplication
                        masterDiagram.AddMaster(srcDiagram, srcMaster.Name);
                    }

                    // Ensure the source diagram has at least one page
                    if (srcDiagram.Pages.Count == 0)
                        continue;

                    // Get the first page from the source diagram
                    Page srcPage = srcDiagram.Pages[0];

                    // Add the page to the master diagram
                    masterDiagram.Pages.Add(srcPage);

                    // Retrieve the page that was just added (it will be the last in the collection)
                    Page addedPage = masterDiagram.Pages[masterDiagram.Pages.Count - 1];

                    // Preserve original page ID if it does not conflict
                    int originalId = srcPage.ID;
                    if (usedPageIds.Contains(originalId))
                    {
                        // If conflict occurs, generate a new unique ID
                        int newId = 1;
                        while (usedPageIds.Contains(newId))
                            newId++;
                        addedPage.ID = newId;
                        usedPageIds.Add(newId);
                    }
                    else
                    {
                        addedPage.ID = originalId;
                        usedPageIds.Add(originalId);
                    }

                    // Optionally preserve the original page name
                    addedPage.Name = srcPage.Name;
                }

                // Save the merged master diagram
                masterDiagram.Save("MasterDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }