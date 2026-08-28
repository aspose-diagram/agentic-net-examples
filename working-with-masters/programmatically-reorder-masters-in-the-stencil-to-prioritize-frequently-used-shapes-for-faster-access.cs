using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class ReorderMastersInStencil
{
    static void Main()
    {
        try
        {

            // Load the existing stencil (VST/VSDX) file
            string inputStencilPath = "inputStencil.vst";
            Diagram stencil = new Diagram(inputStencilPath);

            // Define the list of master names (or universal names) in the desired priority order
            // These should correspond to the names of masters that are frequently used.
            List<string> priorityMasterNames = new List<string>
            {
                "Flowchart:Process",
                "Flowchart:Decision",
                "Basic:Rectangle"
                // add more master names as needed
            };

            // Create a new diagram that will hold the reordered masters
            Diagram reorderedStencil = new Diagram();

            // Keep track of masters that have already been added to avoid duplicates
            HashSet<string> addedMasters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // First, add the priority masters in the specified order
            foreach (string masterName in priorityMasterNames)
            {
                // Verify that the source stencil actually contains this master
                bool exists = false;
                foreach (Master m in stencil.Masters)
                {
                    if (string.Equals(m.Name, masterName, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(m.NameU, masterName, StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    // Add the master from the original stencil to the new stencil
                    int newMasterId = reorderedStencil.AddMaster(stencil, masterName);
                    // Record that this master has been added
                    addedMasters.Add(masterName);
                }
            }

            // Next, add the remaining masters that were not in the priority list,
            // preserving their original order.
            foreach (Master srcMaster in stencil.Masters)
            {
                string nameKey = srcMaster.Name ?? srcMaster.NameU;
                if (string.IsNullOrEmpty(nameKey) || addedMasters.Contains(nameKey))
                    continue; // Skip already added masters

                // Add the master using its name (NameU is safer for universal identification)
                string masterToAdd = srcMaster.NameU ?? srcMaster.Name;
                reorderedStencil.AddMaster(stencil, masterToAdd);
                addedMasters.Add(masterToAdd);
            }

            // Save the reordered stencil to a new file
            string outputStencilPath = "reorderedStencil.vst";
            reorderedStencil.Save(outputStencilPath, SaveFileFormat.Vst);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
