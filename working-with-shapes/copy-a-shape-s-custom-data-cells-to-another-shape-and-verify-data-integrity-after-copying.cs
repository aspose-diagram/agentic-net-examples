using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Retrieve source and target shapes by their IDs (adjust IDs as needed)
                Shape sourceShape = page.Shapes.GetShape(1);
                Shape targetShape = page.Shapes.GetShape(2);

                if (sourceShape == null || targetShape == null)
                {
                    throw new Exception("Source or target shape not found.");
                }

                // Clear any existing custom data cells on the target shape
                targetShape.Users.Clear();

                // Copy custom data cells (User-defined cells) from source to target
                foreach (User srcUser in sourceShape.Users)
                {
                    User newUser = new User
                    {
                        Name = srcUser.Name,
                        NameU = srcUser.NameU
                    };
                    newUser.Value.Val = srcUser.Value.Val;
                    newUser.Prompt.Value = srcUser.Prompt.Value;

                    targetShape.Users.Add(newUser);
                }

                // Verify that the copied data matches the source data
                bool allMatch = true;
                foreach (User srcUser in sourceShape.Users)
                {
                    User matchingUser = null;
                    foreach (User tgtUser in targetShape.Users)
                    {
                        if (tgtUser.Name == srcUser.Name)
                        {
                            matchingUser = tgtUser;
                            break;
                        }
                    }

                    if (matchingUser == null || matchingUser.Value.Val != srcUser.Value.Val)
                    {
                        allMatch = false;
                        Console.WriteLine($"Data mismatch for user cell '{srcUser.Name}'.");
                        break;
                    }
                }

                if (allMatch)
                {
                    Console.WriteLine("Custom data cells copied successfully and verified.");
                }
                else
                {
                    throw new Exception("Verification of copied custom data cells failed.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }