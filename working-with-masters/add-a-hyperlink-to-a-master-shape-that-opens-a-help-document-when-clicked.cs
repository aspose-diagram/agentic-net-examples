using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Locate the master by its name (ensure the master exists in the diagram)
            string targetMasterName = "Rectangle"; // example master name
            Master? targetMaster = null;
            foreach (Master master in diagram.Masters)
            {
                if (master.Name == targetMasterName)
                {
                    targetMaster = master;
                    break;
                }
            }

            if (targetMaster == null)
            {
                throw new Exception($"Master \"{targetMasterName}\" not found in the diagram.");
            }

            // Retrieve a shape from the master to which the hyperlink will be added.
            // Here we use the first shape in the master's shape collection.
            if (targetMaster.Shapes.Count == 0)
            {
                throw new Exception($"Master \"{targetMasterName}\" does not contain any shapes.");
            }

            Shape masterShape = targetMaster.Shapes[0];

            // Create a new hyperlink that points to the help document.
            Hyperlink helpLink = new Hyperlink();
            helpLink.Name = "HelpLink";
            // Use a file URI or a web URL as needed.
            helpLink.Address.Value = "file:///C:/HelpDocs/help.pdf";

            // Add the hyperlink to the shape's Hyperlinks collection.
            masterShape.Hyperlinks.Add(helpLink);

            // Save the modified diagram to a new file.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
