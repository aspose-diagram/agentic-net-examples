using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio stencil file path
                string inputPath = "stencil.vssx";
                // Output cleaned stencil file path
                string outputPath = "stencil_cleaned.vssx";

                // Load the stencil diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Collect IDs of masters that are actually used by shapes on any page
                    HashSet<int> usedMasterIds = new HashSet<int>();

                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Master != null)
                            {
                                usedMasterIds.Add(shape.Master.ID);
                            }
                        }
                    }

                    // Create a list of masters to remove (cannot modify collection while iterating)
                    List<Master> mastersToRemove = new List<Master>();

                    foreach (Master master in diagram.Masters)
                    {
                        if (!usedMasterIds.Contains(master.ID))
                        {
                            mastersToRemove.Add(master);
                        }
                    }

                    // Remove the unused masters
                    foreach (Master master in mastersToRemove)
                    {
                        diagram.Masters.Remove(master);
                    }

                    // Save the cleaned stencil
                    diagram.Save(outputPath, SaveFileFormat.Vssx);
                }

                Console.WriteLine("Unused masters have been removed and stencil saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }