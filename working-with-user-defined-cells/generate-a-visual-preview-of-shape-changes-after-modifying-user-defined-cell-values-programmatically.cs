using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the preview image will be saved
            string outputPath = "preview.png";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through every page and shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Define the name of the user‑defined cell to modify or add
                    const string userCellName = "MyCustomValue";

                    // Search for an existing user‑defined cell with the given name
                    User targetUser = null;
                    foreach (User user in shape.Users)
                    {
                        if (user.Name == userCellName)
                        {
                            targetUser = user;
                            break;
                        }
                    }

                    // If the cell does not exist, create and add it to the shape
                    if (targetUser == null)
                    {
                        targetUser = new User();
                        targetUser.Name = userCellName;
                        targetUser.Prompt.Value = "Custom value for preview";
                        shape.Users.Add(targetUser);
                    }

                    // Set the cell's value (as a string)
                    targetUser.Value.Val = "123";

                    // Refresh the shape so that any dependent geometry or layout updates
                    shape.RefreshData();
                }
            }

            // Export the entire diagram as a PNG image to provide a visual preview
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine("Preview image saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
