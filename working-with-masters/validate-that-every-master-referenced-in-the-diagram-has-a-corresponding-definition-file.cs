using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vdx");

            // Build sets of defined master IDs and names for quick lookup
            var definedMasterIds = new HashSet<int>();
            var definedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Master master in diagram.Masters)
            {
                definedMasterIds.Add(master.ID);
                if (!string.IsNullOrEmpty(master.NameU))
                    definedMasterNames.Add(master.NameU);
                else if (!string.IsNullOrEmpty(master.Name))
                    definedMasterNames.Add(master.Name);
            }

            // List to collect any missing master references
            var missingMasters = new List<string>();

            // Iterate through all pages and shapes to verify master references
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may have a Master reference
                    if (shape.Master != null)
                    {
                        int masterId = shape.Master.ID;
                        string masterName = shape.Master.NameU ?? shape.Master.Name;

                        bool exists = definedMasterIds.Contains(masterId) ||
                                      (!string.IsNullOrEmpty(masterName) && definedMasterNames.Contains(masterName));

                        if (!exists)
                        {
                            missingMasters.Add(
                                $"Page '{page.Name}' Shape ID {shape.ID} references missing master '{masterName ?? masterId.ToString()}'");
                        }
                    }
                }
            }

            // Report validation results
            if (missingMasters.Count == 0)
            {
                Console.WriteLine("All masters referenced in the diagram have corresponding definitions.");
            }
            else
            {
                Console.WriteLine("Missing master definitions found:");
                foreach (string msg in missingMasters)
                    Console.WriteLine(msg);
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
