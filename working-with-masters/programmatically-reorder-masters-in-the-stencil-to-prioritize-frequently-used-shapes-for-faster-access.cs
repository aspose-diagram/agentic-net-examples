using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class ReorderStencilMasters
{
    static void Main()
    {
        try
        {

            // Load the existing stencil file (VSS)
            Diagram stencil = new Diagram("input.vss");

            // Define the order of frequently used masters by their names (or IDs)
            // Adjust this list according to actual usage statistics.
            List<string> priorityMasterNames = new List<string>
            {
                "Rectangle",
                "Ellipse",
                "Star"
            };

            // Build a new ordered collection of masters
            List<Master> orderedMasters = new List<Master>();

            // First, add the priority masters in the desired order
            foreach (string name in priorityMasterNames)
            {
                Master priorityMaster = stencil.Masters.FirstOrDefault(m => m.Name == name);
                if (priorityMaster != null)
                {
                    orderedMasters.Add(priorityMaster);
                }
            }

            // Then, add the remaining masters that were not already added
            foreach (Master master in stencil.Masters)
            {
                if (!orderedMasters.Contains(master))
                {
                    orderedMasters.Add(master);
                }
            }

            // Clear the original masters collection and repopulate it with the new order
            stencil.Masters.Clear();
            foreach (Master master in orderedMasters)
            {
                stencil.Masters.Add(master);
            }

            // Save the reordered stencil to a new file
            stencil.Save("output.vss", SaveFileFormat.Vss);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
