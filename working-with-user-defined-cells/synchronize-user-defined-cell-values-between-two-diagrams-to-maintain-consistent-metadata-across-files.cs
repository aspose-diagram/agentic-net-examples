using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and target Visio files
            string sourcePath = "source.vsdx";
            string targetPath = "target.vsdx";
            string outputPath = "merged.vsdx";

            // Load the diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Iterate through each page in the source diagram
            foreach (Page sourcePage in sourceDiagram.Pages)
            {
                // Find the corresponding page in the target diagram by name
                Page targetPage = targetDiagram.Pages.GetPage(sourcePage.Name);
                if (targetPage == null)
                {
                    // If the page does not exist in the target, skip it
                    continue;
                }

                // Iterate through each shape on the source page
                foreach (Shape sourceShape in sourcePage.Shapes)
                {
                    // Find the matching shape in the target page by universal name
                    Shape targetShape = null;
                    foreach (Shape s in targetPage.Shapes)
                    {
                        if (s.NameU == sourceShape.NameU)
                        {
                            targetShape = s;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        // No matching shape found; skip to next shape
                        continue;
                    }

                    // Synchronize user-defined cells from source shape to target shape
                    foreach (User sourceUser in sourceShape.Users)
                    {
                        // Try to find an existing user cell with the same name in the target shape
                        User targetUser = null;
                        foreach (User u in targetShape.Users)
                        {
                            if (u.Name == sourceUser.Name)
                            {
                                targetUser = u;
                                break;
                            }
                        }

                        if (targetUser != null)
                        {
                            // Update the existing user cell value
                            targetUser.Value.Val = sourceUser.Value.Val;
                        }
                        else
                        {
                            // Create a new user cell in the target shape
                            User newUser = new User();
                            newUser.Name = sourceUser.Name;
                            newUser.NameU = sourceUser.NameU;
                            newUser.Value.Val = sourceUser.Value.Val;
                            newUser.Prompt.Value = sourceUser.Prompt.Value;
                            targetShape.Users.Add(newUser);
                        }
                    }
                }
            }

            // Save the updated target diagram to a new file
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
