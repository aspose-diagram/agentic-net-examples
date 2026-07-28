using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "target_synced.vsdx";

                // Load the diagrams
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Iterate through each page in the source diagram
                foreach (Page sourcePage in sourceDiagram.Pages)
                {
                    // Try to find a page with the same name in the target diagram
                    Page targetPage = targetDiagram.Pages.GetPage(sourcePage.Name);
                    if (targetPage == null)
                    {
                        // If the page does not exist in the target, skip synchronization for this page
                        continue;
                    }

                    // Iterate through each shape on the source page
                    foreach (Shape sourceShape in sourcePage.Shapes)
                    {
                        // Find the corresponding shape in the target page by universal name
                        Shape targetShape = null;
                        foreach (Shape shp in targetPage.Shapes)
                        {
                            if (shp.NameU == sourceShape.NameU)
                            {
                                targetShape = shp;
                                break;
                            }
                        }

                        if (targetShape == null)
                        {
                            // No matching shape found; skip this shape
                            continue;
                        }

                        // Synchronize each user‑defined cell from the source shape to the target shape
                        foreach (User sourceUser in sourceShape.Users)
                        {
                            // Look for an existing user cell with the same name in the target shape
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
                                // Update the existing cell value
                                targetUser.Value.Val = sourceUser.Value.Val;
                            }
                            else
                            {
                                // Create a new user‑defined cell and add it to the target shape
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

                // Save the updated target diagram
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }