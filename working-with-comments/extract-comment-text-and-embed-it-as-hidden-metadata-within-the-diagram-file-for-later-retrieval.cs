using System.IO;
using System;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Gather all comment texts from every page
            StringBuilder commentBuilder = new StringBuilder();

            foreach (Page page in diagram.Pages)
            {
                // Annotations collection holds comments for the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Append the comment text; use .Value as required
                    commentBuilder.AppendLine(annotation.Comment.Value);
                }
            }

            // Create a custom document property to store the concatenated comments
            CustomProp hiddenProp = new CustomProp();
            hiddenProp.Name = "EmbeddedComments";
            hiddenProp.PropType = PropType.String;
            hiddenProp.CustomValue = new CustomValue();
            hiddenProp.CustomValue.ValueString = commentBuilder.ToString();

            // Add the custom property to the diagram's custom properties collection
            diagram.DocumentProps.CustomProps.Add(hiddenProp);

            // Save the diagram with the new hidden metadata
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
