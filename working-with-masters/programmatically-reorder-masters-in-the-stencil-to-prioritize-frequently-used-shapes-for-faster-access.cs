using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input stencil file path (source)
        string sourceStencilPath = "source.vssx";
        if (!System.IO.File.Exists(sourceStencilPath))
        {
            Console.Error.WriteLine($"File not found: {sourceStencilPath}");
            return;
        }
        // Output stencil file path (reordered)
        string outputStencilPath = "reordered.vssx";

        // Guard: ensure source stencil exists
        if (!File.Exists(sourceStencilPath))
        {
            Console.Error.WriteLine($"Source stencil file not found: {sourceStencilPath}");
            return;
        }

        // Define the priority order of master names (most frequently used first)
        // Adjust this list according to your usage statistics
        List<string> priorityMasters = new List<string>
        {
            "Rectangle",
            "Dynamic connector",
            "Star 7"
        };

        try
        {
            // Load the source stencil diagram
            Diagram sourceDiagram = new Diagram(sourceStencilPath);

            // Create a new empty diagram that will hold the reordered masters
            Diagram reorderedDiagram = new Diagram();

            // Keep track of masters that have already been added to avoid duplicates
            HashSet<string> addedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // First, add masters in the priority order if they exist in the source stencil
            foreach (string masterName in priorityMasters)
            {
                if (sourceDiagram.Masters.IsExist(masterName))
                {
                    // Retrieve the master to obtain its ID
                    Master srcMaster = sourceDiagram.Masters.GetMasterByName(masterName);
                    int masterId = srcMaster.ID;

                    // Import the master into the new diagram using the same stencil file
                    reorderedDiagram.AddMaster(sourceStencilPath, masterId);

                    addedMasterNames.Add(masterName);
                }
                else
                {
                    Console.WriteLine($"Priority master not found in source stencil: {masterName}");
                }
            }

            // Next, add all remaining masters preserving their original order
            foreach (Master srcMaster in sourceDiagram.Masters)
            {
                string name = srcMaster.Name;
                if (!addedMasterNames.Contains(name))
                {
                    int masterId = srcMaster.ID;
                    reorderedDiagram.AddMaster(sourceStencilPath, masterId);
                    addedMasterNames.Add(name);
                }
            }

            // Save the reordered stencil
            reorderedDiagram.Save(outputStencilPath, SaveFileFormat.Vssx);
            Console.WriteLine($"Reordered stencil saved to: {outputStencilPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing stencil: {ex.Message}");
        }
    }
}