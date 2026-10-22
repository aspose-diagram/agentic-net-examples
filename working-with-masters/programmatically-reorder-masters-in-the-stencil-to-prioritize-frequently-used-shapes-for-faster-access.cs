using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input stencil file path
        string stencilPath = "inputStencil.vssx";
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Error: Stencil file not found at '{stencilPath}'.");
            return;
        }

        // Output reordered stencil file path
        string outputStencilPath = "reorderedStencil.vssx";

        // List of master names that should appear first (most frequently used)
        List<string> prioritizedMasters = new List<string>
        {
            "Rectangle",
            "Dynamic connector",
            "Star 7"
            // Add more master names as needed
        };

        try
        {
            // Load the existing stencil diagram inside a using block (mandatory)
            using (Diagram sourceStencil = new Diagram(stencilPath))
            {
                // Create a new empty diagram to hold reordered masters
                using (Diagram reorderedStencil = new Diagram())
                {
                    // Keep track of masters already added to avoid duplicates
                    HashSet<string> addedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    // First, add prioritized masters in the specified order
                    foreach (string masterName in prioritizedMasters)
                    {
                        if (sourceStencil.Masters.IsExist(masterName))
                        {
                            // Retrieve the master from the source stencil
                            Master srcMaster = sourceStencil.Masters.GetMasterByName(masterName);
                            // Clone the master to avoid reference issues
                            Master clonedMaster = (Master)srcMaster.Clone();
                            // Add the cloned master to the new stencil
                            reorderedStencil.Masters.Add(clonedMaster);
                            addedMasterNames.Add(masterName);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Warning: Master '{masterName}' not found in source stencil.");
                        }
                    }

                    // Then, add the remaining masters preserving their original order
                    foreach (Master master in sourceStencil.Masters)
                    {
                        string name = master.Name;
                        if (string.IsNullOrEmpty(name))
                            continue;

                        if (!addedMasterNames.Contains(name))
                        {
                            Master clonedMaster = (Master)master.Clone();
                            reorderedStencil.Masters.Add(clonedMaster);
                            addedMasterNames.Add(name);
                        }
                    }

                    // Save the reordered stencil to the specified output path
                    reorderedStencil.Save(outputStencilPath, SaveFileFormat.Vssx);
                    Console.WriteLine($"Reordered stencil saved to '{outputStencilPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}