using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPreviewPng> [UserCellName] [NewValue]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string userCellName = args.Length >= 3 ? args[2] : "MyUserCell";
        string newValue = args.Length >= 4 ? args[3] : "Modified";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to modify the specified user-defined cell.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Users collection.
                    if (shape.Users == null)
                        continue;

                    bool cellFound = false;

                    // Search for an existing user-defined cell with the given name.
                    foreach (User user in shape.Users)
                    {
                        if (string.Equals(user.Name, userCellName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(user.NameU, userCellName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Update the cell's value.
                            user.Value.Val = newValue;
                            cellFound = true;
                            break;
                        }
                    }

                    // If the cell does not exist, create and add it.
                    if (!cellFound)
                    {
                        User newUser = new User();
                        newUser.Name = userCellName;
                        newUser.Value.Val = newValue;
                        // Prompt is optional; leave empty.
                        newUser.Prompt.Value = string.Empty;
                        shape.Users.Add(newUser);
                    }
                }
            }

            // Save a visual preview of the modified diagram as a PNG image.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Preview image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}