using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
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

                // Synchronize user‑defined cells from source to target
                SyncUserDefinedCells(sourceDiagram, targetDiagram);

                // Save the updated target diagram
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies all user‑defined cell values from the source diagram to the target diagram.
        /// Shapes are matched by their universal name (NameU) and page name.
        /// If a user‑defined cell does not exist in the target shape, it is created.
        /// </summary>
        static void SyncUserDefinedCells(Diagram source, Diagram target)
        {
            // Iterate through each page in the source diagram
            foreach (Page srcPage in source.Pages)
            {
                // Find the corresponding page in the target diagram by name
                Page tgtPage = target.Pages.GetPage(srcPage.Name);
                if (tgtPage == null)
                {
                    // No matching page; skip synchronization for this page
                    continue;
                }

                // Iterate through each shape on the source page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Find the matching shape on the target page by universal name
                    Shape tgtShape = FindShapeByNameU(tgtPage, srcShape.NameU);
                    if (tgtShape == null)
                    {
                        // No matching shape; skip this shape
                        continue;
                    }

                    // Synchronize each user‑defined cell
                    foreach (User srcUser in srcShape.Users)
                    {
                        // Try to locate the same user‑defined cell in the target shape
                        User tgtUser = FindUserByNameU(tgtShape, srcUser.NameU);
                        if (tgtUser != null)
                        {
                            // Update existing cell value
                            tgtUser.Value.Val = srcUser.Value.Val;
                        }
                        else
                        {
                            // Create a new user‑defined cell in the target shape
                            User newUser = new User
                            {
                                Name = srcUser.Name,
                                NameU = srcUser.NameU,
                                Prompt = { Value = srcUser.Prompt.Value },
                                Value = { Val = srcUser.Value.Val }
                            };
                            tgtShape.Users.Add(newUser);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds a shape on the given page whose universal name matches the specified name.
        /// Returns null if no such shape exists.
        /// </summary>
        static Shape FindShapeByNameU(Page page, string nameU)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a user‑defined cell in a shape by its universal name.
        /// Returns null if not found.
        /// </summary>
        static User FindUserByNameU(Shape shape, string nameU)
        {
            foreach (User user in shape.Users)
            {
                if (user.NameU != null && user.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            return null;
        }
    }