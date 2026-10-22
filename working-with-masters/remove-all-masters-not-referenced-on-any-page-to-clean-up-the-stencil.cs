using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio stencil file path
        string inputPath = "input.vssx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output path for the cleaned stencil
        string outputPath = "cleaned_output.vssx";

        try
        {
            // Load the stencil diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect masters that are not referenced by any shape on any page
                List<Master> mastersToRemove = new List<Master>();

                // Iterate over each master in the stencil
                foreach (Master master in diagram.Masters)
                {
                    bool isUsed = false;

                    // Scan all pages and their shapes to see if the master is referenced
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // If the shape uses this master, mark as used and break out
                            if (shape.Master != null && shape.Master.NameU == master.NameU)
                            {
                                isUsed = true;
                                break;
                            }
                        }
                        if (isUsed) break;
                    }

                    // If the master was never used, schedule it for removal
                    if (!isUsed)
                    {
                        mastersToRemove.Add(master);
                    }
                }

                // Remove the unused masters from the diagram
                foreach (Master m in mastersToRemove)
                {
                    // The MasterCollection supports Remove method
                    diagram.Masters.Remove(m);
                }

                // Save the cleaned stencil to a new file
                diagram.Save(outputPath, SaveFileFormat.Vssx);
                Console.WriteLine($"Cleaned stencil saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}