using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt for the Visio file to load
            Console.Write("Enter the path to the Visio file (e.g., diagram.vsdx): ");
            string inputPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Console.WriteLine("Invalid file path. Exiting.");
                return;
            }

            Diagram diagram;
            try
            {
                // Load the diagram using the constructor that accepts a file path
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1) List pages and shapes");
                Console.WriteLine("2) Edit a user-defined cell");
                Console.WriteLine("3) Save and exit");
                Console.Write("Select an option (1-3): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ListPagesAndShapes(diagram);
                }
                else if (choice == "2")
                {
                    EditUserDefinedCell(diagram);
                }
                else if (choice == "3")
                {
                    SaveDiagram(diagram);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
                }
            }
        }

        // Lists all pages and the shapes they contain
        private static void ListPagesAndShapes(Diagram diagram)
        {
            Console.WriteLine("\nPages and Shapes:");
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                Console.WriteLine($"Page [{p}] - ID: {page.ID}, Name: {page.Name}");
                for (int s = 0; s < page.Shapes.Count; s++)
                {
                    Shape shape = page.Shapes[s];
                    Console.WriteLine($"  Shape [{s}] - ID: {shape.ID}, Name: {shape.Name}");
                }
            }
        }

        // Allows the user to edit a specific user-defined cell
        private static void EditUserDefinedCell(Diagram diagram)
        {
            // Select page
            Console.Write("\nEnter page index: ");
            if (!int.TryParse(Console.ReadLine(), out int pageIndex) ||
                pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            Page page = diagram.Pages[pageIndex];

            // Select shape
            Console.Write("Enter shape index within the page: ");
            if (!int.TryParse(Console.ReadLine(), out int shapeIndex) ||
                shapeIndex < 0 || shapeIndex >= page.Shapes.Count)
            {
                Console.WriteLine("Invalid shape index.");
                return;
            }

            Shape shape = page.Shapes[shapeIndex];

            // List user-defined cells for the selected shape
            if (shape.Users.Count == 0)
            {
                Console.WriteLine("No user-defined cells found for this shape.");
                return;
            }

            Console.WriteLine("\nUser-defined cells:");
            for (int u = 0; u < shape.Users.Count; u++)
            {
                User user = shape.Users[u];
                Console.WriteLine($"  [{u}] Name: {user.Name}, Value: {user.Value.Val}");
            }

            // Choose cell to edit
            Console.Write("Enter the index of the cell to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int userIndex) ||
                userIndex < 0 || userIndex >= shape.Users.Count)
            {
                Console.WriteLine("Invalid cell index.");
                return;
            }

            User targetUser = shape.Users[userIndex];
            Console.Write($"Current value of '{targetUser.Name}' is '{targetUser.Value.Val}'. Enter new value: ");
            string newValue = Console.ReadLine();

            if (newValue == null)
            {
                Console.WriteLine("No value entered. Edit cancelled.");
                return;
            }

            // Update the cell value
            targetUser.Value.Val = newValue;
            Console.WriteLine("Cell value updated successfully.");
        }

        // Saves the diagram to a new file
        private static void SaveDiagram(Diagram diagram)
        {
            Console.Write("\nEnter the output file path (e.g., updated.vsdx): ");
            string outputPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                Console.WriteLine("Invalid output path. Save aborted.");
                return;
            }

            try
            {
                // Save using the SaveFileFormat enum (PascalCase)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }