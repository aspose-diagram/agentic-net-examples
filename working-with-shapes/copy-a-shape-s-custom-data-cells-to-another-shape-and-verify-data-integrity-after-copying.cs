using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, source shape ID, target shape ID, output diagram path.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <sourceShapeId> <targetShapeId> <output.vsdx>");
            return;
        }

        // Assign input arguments to variables.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string sourceIdStr = args[1];
        string targetIdStr = args[2];
        string outputPath = args[3];

        // Parse shape IDs; if parsing fails, report error and exit.
        if (!long.TryParse(sourceIdStr, out long sourceShapeId))
        {
            Console.Error.WriteLine($"Invalid source shape ID: {sourceIdStr}");
            return;
        }
        if (!long.TryParse(targetIdStr, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid target shape ID: {targetIdStr}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Use the first page (index 0) for shape operations.
            Page page = diagram.Pages[0];

            // Retrieve the source shape by ID; cast to int as required by GetShape.
            Shape sourceShape = page.Shapes.GetShape((int)sourceShapeId);
            // Retrieve the target shape by ID.
            Shape targetShape = page.Shapes.GetShape((int)targetShapeId);

            // Guard: ensure both shapes were found.
            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Source shape with ID {sourceShapeId} not found.");
                return;
            }
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Target shape with ID {targetShapeId} not found.");
                return;
            }

            // Clear any existing user-defined cells on the target shape to avoid duplicates.
            targetShape.Users.Clear();

            // Copy each user-defined cell from source to target.
            foreach (User srcUser in sourceShape.Users)
            {
                // Create a new User instance for the target shape.
                User tgtUser = new User();

                // Copy the universal name (NameU) and local name (Name).
                tgtUser.NameU = srcUser.NameU;
                tgtUser.Name = srcUser.Name;

                // Copy the cell value.
                tgtUser.Value.Val = srcUser.Value.Val;

                // Copy the prompt/description if present.
                tgtUser.Prompt.Value = srcUser.Prompt.Value;

                // Add the new user-defined cell to the target shape.
                targetShape.Users.Add(tgtUser);
            }

            // Verify data integrity: ensure each source user cell matches the corresponding target cell.
            bool integrityOk = true;
            foreach (User srcUser in sourceShape.Users)
            {
                // Find the matching user in the target shape by NameU.
                User matchingTgt = null;
                foreach (User tgtUser in targetShape.Users)
                {
                    if (tgtUser.NameU == srcUser.NameU)
                    {
                        matchingTgt = tgtUser;
                        break;
                    }
                }

                // If no matching cell is found, integrity fails.
                if (matchingTgt == null)
                {
                    Console.Error.WriteLine($"Missing user cell '{srcUser.NameU}' in target shape.");
                    integrityOk = false;
                    continue;
                }

                // Compare value and prompt; report any mismatches.
                if (matchingTgt.Value.Val != srcUser.Value.Val)
                {
                    Console.Error.WriteLine($"Value mismatch for '{srcUser.NameU}': source='{srcUser.Value.Val}' target='{matchingTgt.Value.Val}'");
                    integrityOk = false;
                }
                if (matchingTgt.Prompt.Value != srcUser.Prompt.Value)
                {
                    Console.Error.WriteLine($"Prompt mismatch for '{srcUser.NameU}': source='{srcUser.Prompt.Value}' target='{matchingTgt.Prompt.Value}'");
                    integrityOk = false;
                }
            }

            // If integrity check failed, abort saving.
            if (!integrityOk)
            {
                Console.Error.WriteLine("Data integrity verification failed. Diagram not saved.");
                return;
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Indicate successful completion.
            Console.WriteLine("Custom data cells copied and verified successfully.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}