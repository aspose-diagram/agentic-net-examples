using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the source diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page
                Page page = diagram.Pages[0];

                // Retrieve the source shape (assumes shape with ID 1 exists)
                Shape sourceShape = page.Shapes.GetShape(1);

                // Determine master name for the new shape
                string masterName = sourceShape.Master != null ? sourceShape.Master.Name : "Rectangle";

                // Add a new shape on the same page, offset vertically to avoid overlap
                long newShapeId = page.AddShape(
                    sourceShape.XForm.PinX.Value,
                    sourceShape.XForm.PinY.Value + 2.0,
                    masterName);

                // Retrieve the newly added shape instance
                Shape targetShape = page.Shapes.GetShape((int)newShapeId);

                // ----- Copy simple custom data cells (Data1, Data2, Data3) -----
                targetShape.Data1 = sourceShape.Data1;
                targetShape.Data2 = sourceShape.Data2;
                targetShape.Data3 = sourceShape.Data3;

                // ----- Copy user‑defined cells (Users collection) -----
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

                // ----- Verify that the data was copied correctly -----
                bool integrityOk = true;

                // Verify Data1‑Data3
                if (targetShape.Data1 != sourceShape.Data1) integrityOk = false;
                if (targetShape.Data2 != sourceShape.Data2) integrityOk = false;
                if (targetShape.Data3 != sourceShape.Data3) integrityOk = false;

                // Verify Users collection
                if (targetShape.Users.Count != sourceShape.Users.Count)
                {
                    integrityOk = false;
                }
                else
                {
                    for (int i = 0; i < sourceShape.Users.Count; i++)
                    {
                        User src = sourceShape.Users[i];
                        User tgt = targetShape.Users[i];

                        if (src.Name != tgt.Name ||
                            src.NameU != tgt.NameU ||
                            src.Value.Val != tgt.Value.Val ||
                            src.Prompt.Value != tgt.Prompt.Value)
                        {
                            integrityOk = false;
                            break;
                        }
                    }
                }

                // Report result
                if (!integrityOk)
                {
                    throw new Exception("Data integrity verification failed after copying custom cells.");
                }
                else
                {
                    Console.WriteLine("Custom data cells copied successfully and verified.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }