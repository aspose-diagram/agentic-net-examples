using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (VSDX, VSD, etc.). Pass as first argument or edit the default value.
                string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioPath);

                // Lists to hold diagnostic information.
                var shapesWithNoUserCells = new List<string>();
                var shapesWithDuplicateUserCells = new List<string>();

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the collection of user-defined cells.
                        var userCells = shape.Users;

                        // If there are no user-defined cells, record the shape.
                        if (userCells == null || userCells.Count == 0)
                        {
                            shapesWithNoUserCells.Add($"{page.NameU} -> {shape.NameU} (ID:{shape.ID})");
                            continue;
                        }

                        // Gather the names of the user cells.
                        var userNames = new List<string>();
                        foreach (User user in userCells)
                        {
                            // The universal name of the user cell.
                            string nameU = user.NameU;
                            if (!string.IsNullOrEmpty(nameU))
                            {
                                userNames.Add(nameU);
                            }
                        }

                        // Detect duplicate names within the same shape.
                        var duplicateNames = userNames
                            .GroupBy(n => n)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .ToList();

                        if (duplicateNames.Count > 0)
                        {
                            string dupList = string.Join(", ", duplicateNames);
                            shapesWithDuplicateUserCells.Add($"{page.NameU} -> {shape.NameU} (ID:{shape.ID}) : {dupList}");
                        }
                    }
                }

                // Output the diagnostic results.
                Console.WriteLine("=== Shapes with NO user-defined cells ===");
                if (shapesWithNoUserCells.Count == 0)
                    Console.WriteLine("None");
                else
                    shapesWithNoUserCells.ForEach(Console.WriteLine);

                Console.WriteLine();
                Console.WriteLine("=== Shapes with DUPLICATE user-defined cell names ===");
                if (shapesWithDuplicateUserCells.Count == 0)
                    Console.WriteLine("None");
                else
                    shapesWithDuplicateUserCells.ForEach(Console.WriteLine);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }