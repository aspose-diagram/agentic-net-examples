using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is output file, remaining are input diagram files
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <output.vsdx> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];

        // Verify each input file exists
        for (int i = 1; i < args.Length; i++)
        {
            if (!File.Exists(args[i]))
            {
                Console.Error.WriteLine($"Input file not found: {args[i]}");
                return;
            }
        }

        try
        {
            // Load the first diagram to serve as the master diagram
            using (Diagram masterDiagram = new Diagram(args[1]))
            {
                // Keep only the first page of the master diagram
                while (masterDiagram.Pages.Count > 1)
                {
                    // Remove pages from the end to avoid index shifting
                    masterDiagram.Pages.Remove(masterDiagram.Pages[masterDiagram.Pages.Count - 1]);
                }

                // Process remaining source diagrams (if any)
                for (int i = 2; i < args.Length; i++)
                {
                    string srcPath = args[i];
                    using (Diagram srcDiagram = new Diagram(srcPath))
                    {
                        // Copy masters from source diagram to master diagram (avoid duplicates)
                        foreach (Master srcMaster in srcDiagram.Masters)
                        {
                            // Check if a master with the same name already exists
                            if (masterDiagram.Masters.GetMasterByName(srcMaster.Name) == null)
                            {
                                masterDiagram.Masters.Add(srcMaster);
                            }
                        }

                        // Ensure the source diagram has at least one page
                        if (srcDiagram.Pages.Count == 0)
                        {
                            Console.Error.WriteLine($"Source diagram has no pages: {srcPath}");
                            continue;
                        }

                        // Get the first page from the source diagram
                        Page srcPage = srcDiagram.Pages[0];

                        // Create a new page in the master diagram and copy content
                        Page newPage = new Page();
                        newPage.Copy(srcPage);

                        // Preserve original page ID and name
                        newPage.ID = srcPage.ID;
                        newPage.Name = srcPage.Name;

                        // Add the copied page to the master diagram
                        masterDiagram.Pages.Add(newPage);
                    }
                }

                // Save the combined master diagram to the specified output path
                masterDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Master diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}