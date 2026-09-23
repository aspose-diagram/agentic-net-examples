using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Collect all comment texts from every page.
        List<string> commentTexts = new List<string>();
        foreach (Page page in diagram.Pages)
        {
            // Annotations (comments) are stored in the page's PageSheet.
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // The comment text is in the Comment cell.
                string text = annotation.Comment.Value;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    commentTexts.Add(text);
                }
            }
        }

        // Combine comments into a single string (you can change the delimiter as needed).
        string combinedComments = string.Join(Environment.NewLine, commentTexts);

        // Embed the combined comment text as a custom document property.
        // This property acts as hidden metadata that can be retrieved later.
        CustomProp customProp = new CustomProp();
        customProp.Name = "Comments";
        customProp.PropType = PropType.String;
        customProp.CustomValue = new CustomValue();
        customProp.CustomValue.ValueString = combinedComments;

        // Add or replace the custom property in the document.
        // If a property with the same name already exists, remove it first.
        bool exists = false;
        foreach (CustomProp existing in diagram.DocumentProps.CustomProps)
        {
            if (existing.Name == customProp.Name)
            {
                exists = true;
                diagram.DocumentProps.CustomProps.Remove(existing);
                break;
            }
        }
        diagram.DocumentProps.CustomProps.Add(customProp);

        // Save the diagram with the embedded metadata.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine("Comments extracted and stored as hidden metadata successfully.");
    }
}
