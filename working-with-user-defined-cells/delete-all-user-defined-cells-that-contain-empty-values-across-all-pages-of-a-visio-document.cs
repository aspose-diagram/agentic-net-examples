using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path. If not supplied, create a new file name with suffix "_cleaned".
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, filenameWithoutExt + "_cleaned.vsdx");
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Collect user‑defined cells that have empty values.
                        List<User> usersToRemove = new List<User>();
                        foreach (User user in shape.Users)
                        {
                            // Guard against null Value and treat whitespace as empty.
                            string cellValue = user.Value?.Val;
                            if (string.IsNullOrWhiteSpace(cellValue))
                            {
                                usersToRemove.Add(user);
                            }
                        }

                        // Remove the collected empty user‑defined cells.
                        foreach (User userToRemove in usersToRemove)
                        {
                            shape.Users.Remove(userToRemove);
                        }
                    }
                }

                // Save the modified diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully saved cleaned diagram to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}