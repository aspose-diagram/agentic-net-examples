using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Indices of source and target pages (0‑based)
                int sourcePageIndex = 0;
                int targetPageIndex = 1;

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure source page exists
                    if (sourcePageIndex >= diagram.Pages.Count)
                    {
                        Console.WriteLine("Source page index out of range.");
                        return;
                    }

                    // Get source page
                    Page sourcePage = diagram.Pages[sourcePageIndex];

                    // Retrieve the first shape on the source page to clone
                    Shape sourceShape = null;
                    foreach (Shape s in sourcePage.Shapes)
                    {
                        sourceShape = s;
                        break;
                    }

                    if (sourceShape == null)
                    {
                        Console.WriteLine("No shape found on the source page.");
                        return;
                    }

                    // Ensure target page exists; create if necessary
                    Page targetPage;
                    if (targetPageIndex < diagram.Pages.Count)
                    {
                        targetPage = diagram.Pages[targetPageIndex];
                    }
                    else
                    {
                        // Add a new blank page
                        targetPage = new Page();
                        targetPage.Name = $"Page{targetPageIndex + 1}";
                        diagram.Pages.Add(targetPage);
                    }

                    // Get master name of the source shape
                    if (sourceShape.Master == null)
                    {
                        Console.WriteLine("Source shape does not have an associated master.");
                        return;
                    }
                    string masterName = sourceShape.Master.Name;

                    // Position for the cloned shape (same as source)
                    double pinX = sourceShape.XForm.PinX.Value;
                    double pinY = sourceShape.XForm.PinY.Value;

                    // Add a new shape on the target page using the same master
                    long newShapeId = targetPage.AddShape(pinX, pinY, masterName);
                    Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                    // Copy user‑defined cells (Users collection) from source to clone
                    foreach (User srcUser in sourceShape.Users)
                    {
                        User newUser = new User
                        {
                            Name = srcUser.Name,
                            NameU = srcUser.NameU,
                            Value = { Val = srcUser.Value.Val },
                            Prompt = { Value = srcUser.Prompt.Value }
                        };
                        clonedShape.Users.Add(newUser);
                    }

                    // Optional: copy other properties (e.g., text) if needed
                    // clonedShape.Text.Value.Clear();
                    // clonedShape.Text.Value.Add(new Txt(sourceShape.Text.Value.Text));

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Shape cloned and saved successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }