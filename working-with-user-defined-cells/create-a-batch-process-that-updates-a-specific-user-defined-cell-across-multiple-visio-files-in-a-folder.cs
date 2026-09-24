using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files
            string folderPath = @"C:\VisioFiles";

            // Name of the user-defined cell to update
            string targetCellName = "MyUserCell";

            // New value to assign to the cell
            string newValue = "12345";

            // Get all VSDX files in the folder
            string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx");

            foreach (string filePath in visioFiles)
            {
                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Look for the target user-defined cell
                            foreach (User userCell in shape.Users)
                            {
                                if (userCell.Name == targetCellName || userCell.NameU == targetCellName)
                                {
                                    // Update the cell's value
                                    userCell.Value.Val = newValue;
                                    break; // Assuming only one instance per shape
                                }
                            }
                        }
                    }

                    // Save the updated diagram back to the same file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Updated: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch update completed.");
        }
    }