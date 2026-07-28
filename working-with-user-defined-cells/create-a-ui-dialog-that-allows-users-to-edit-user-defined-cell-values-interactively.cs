using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt for the Visio file to load
            Console.Write("Enter the path to the Visio diagram file: ");
            string inputPath = Console.ReadLine();

            // Load the diagram using the Diagram constructor
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load diagram: " + ex.Message);
                return;
            }

            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.WriteLine("\nPages in the diagram:");
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    var page = diagram.Pages[i];
                    Console.WriteLine($"{i}: {page.NameU}");
                }

                Console.Write("Select a page index (or type 'exit' to quit): ");
                string pageInput = Console.ReadLine();
                if (pageInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    exitRequested = true;
                    continue;
                }

                if (!int.TryParse(pageInput, out int pageIndex) || pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.WriteLine("Invalid page index.");
                    continue;
                }

                Page selectedPage = diagram.Pages[pageIndex];

                Console.WriteLine($"\nShapes on page '{selectedPage.NameU}':");
                foreach (Shape shape in selectedPage.Shapes)
                {
                    // Shape ID is a long, NameU is the universal name
                    Console.WriteLine($"ID: {shape.ID}, Name: {shape.NameU}");
                }

                Console.Write("Enter the Shape ID you want to edit (or 'back' to choose another page): ");
                string shapeInput = Console.ReadLine();
                if (shapeInput.Equals("back", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!long.TryParse(shapeInput, out long shapeId))
                {
                    Console.WriteLine("Invalid shape ID.");
                    continue;
                }

                Shape targetShape = selectedPage.Shapes.GetShape(shapeId);
                if (targetShape == null)
                {
                    Console.WriteLine("Shape not found.");
                    continue;
                }

                // List user-defined cells for the selected shape
                Console.WriteLine($"\nUser-defined cells for shape ID {targetShape.ID}:");
                foreach (User userCell in targetShape.Users)
                {
                    Console.WriteLine($"- NameU: {userCell.NameU}, Value: {userCell.Value.Val}, Prompt: {userCell.Prompt.Value}");
                }

                bool editAnother = true;
                while (editAnother)
                {
                    Console.Write("\nEnter the NameU of the user-defined cell to edit (or 'done' to finish editing this shape): ");
                    string cellName = Console.ReadLine();
                    if (cellName.Equals("done", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    // Find the user cell by NameU
                    User cellToEdit = null;
                    foreach (User u in targetShape.Users)
                    {
                        if (u.NameU.Equals(cellName, StringComparison.OrdinalIgnoreCase))
                        {
                            cellToEdit = u;
                            break;
                        }
                    }

                    if (cellToEdit == null)
                    {
                        Console.WriteLine("User-defined cell not found.");
                        continue;
                    }

                    Console.Write($"Current value of '{cellToEdit.NameU}' is '{cellToEdit.Value.Val}'. Enter new value: ");
                    string newValue = Console.ReadLine();

                    // Update the cell value
                    cellToEdit.Value.Val = newValue;
                    Console.WriteLine($"Cell '{cellToEdit.NameU}' updated to '{cellToEdit.Value.Val}'.");
                }

                Console.Write("\nDo you want to edit another shape on this page? (y/n): ");
                string anotherShape = Console.ReadLine();
                if (!anotherShape.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    // Ask if the user wants to save changes
                    Console.Write("\nDo you want to save the diagram? (y/n): ");
                    string saveChoice = Console.ReadLine();
                    if (saveChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter output file path (e.g., output.vsdx): ");
                        string outputPath = Console.ReadLine();

                        try
                        {
                            diagram.Save(outputPath, SaveFileFormat.Vsdx);
                            Console.WriteLine("Diagram saved successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to save diagram: " + ex.Message);
                        }
                    }

                    // Return to page selection or exit
                    Console.Write("\nDo you want to edit another page? (y/n): ");
                    string anotherPage = Console.ReadLine();
                    if (!anotherPage.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        exitRequested = true;
                    }
                }
            }

            // Clean up
            diagram.Dispose();
            Console.WriteLine("Application terminated.");
        }
    }