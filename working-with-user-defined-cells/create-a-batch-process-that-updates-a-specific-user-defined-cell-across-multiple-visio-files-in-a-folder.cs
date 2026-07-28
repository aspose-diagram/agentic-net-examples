using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Prompt user for inputs
            Console.Write("Enter the folder path containing Visio files: ");
            string folderPath = Console.ReadLine()?.Trim();

            Console.Write("Enter the name of the user-defined cell to update: ");
            string cellName = Console.ReadLine()?.Trim();

            Console.Write("Enter the new value for the cell: ");
            string newValue = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(cellName) || newValue == null)
            {
                Console.WriteLine("Invalid input. Exiting.");
                return;
            }

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            try
            {
                UpdateUserDefinedCellInFolder(folderPath, cellName, newValue);
                Console.WriteLine("Batch update completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the specified user-defined cell in all Visio files within the given folder.
        /// </summary>
        /// <param name="folderPath">Path to the folder containing Visio files.</param>
        /// <param name="cellName">Name of the user-defined cell to modify.</param>
        /// <param name="newValue">New value to assign to the cell.</param>
        private static void UpdateUserDefinedCellInFolder(string folderPath, string cellName, string newValue)
        {
            // Process .vsdx files; adjust the pattern if other formats are needed
            string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in visioFiles)
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Users collection exists
                        if (shape.Users != null)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                // Match by Name or universal NameU (case‑insensitive)
                                if (string.Equals(userCell.Name, cellName, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(userCell.NameU, cellName, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Update the cell value
                                    userCell.Value.Val = newValue;
                                }
                            }
                        }
                    }
                }

                // Overwrite the original file with the updated diagram
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }
    }