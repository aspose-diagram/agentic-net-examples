using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source stencil (VSSX) file
                string sourceStencilPath = @"C:\Stencils\MyStencil.vssx";

                // Path where the reordered stencil will be saved
                string reorderedStencilPath = @"C:\Stencils\MyStencil_Reordered.vssx";

                // Define the master names in the order you want them to appear.
                // These should be the universal names (NameU) of the masters.
                string[] prioritizedMasterNames = new string[]
                {
                    "Process",      // most frequently used
                    "Decision",
                    "Data",
                    "Start/End"
                    // add more names as needed
                };

                // Load the original stencil
                Diagram sourceStencil = new Diagram(sourceStencilPath, LoadFileFormat.Vssx);

                // Create a new empty diagram that will hold the reordered masters
                Diagram reorderedStencil = new Diagram();

                // Keep track of which masters have already been added
                var addedMasterNames = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // First, add the prioritized masters in the specified order
                foreach (string masterName in prioritizedMasterNames)
                {
                    if (sourceStencil.Masters.IsExist(masterName))
                    {
                        // Add the master from the source stencil to the new stencil
                        reorderedStencil.AddMaster(sourceStencil, masterName);
                        addedMasterNames.Add(masterName);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Master \"{masterName}\" not found in the source stencil.");
                    }
                }

                // Then, add the remaining masters preserving their original order
                foreach (Master master in sourceStencil.Masters)
                {
                    // Use the universal name for comparison
                    string nameU = master.NameU;
                    if (!addedMasterNames.Contains(nameU))
                    {
                        reorderedStencil.AddMaster(sourceStencil, nameU);
                        addedMasterNames.Add(nameU);
                    }
                }

                // Save the reordered stencil
                reorderedStencil.Save(reorderedStencilPath, SaveFileFormat.Vssx);

                // Clean up
                sourceStencil.Dispose();
                reorderedStencil.Dispose();

                Console.WriteLine("Stencil reordering completed successfully.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }