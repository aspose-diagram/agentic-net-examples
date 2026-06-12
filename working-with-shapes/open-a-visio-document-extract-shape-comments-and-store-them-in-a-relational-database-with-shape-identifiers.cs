using System;
using Aspose.Diagram;
using System.Data;
using System.Data.SqlClient;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Connection string to the relational database
                string connectionString = "Data Source=SERVER;Initial Catalog=DatabaseName;Integrated Security=True";

                // Open the Visio document
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Iterate through all pages in the diagram
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Iterate through all annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Extract shape identifier, comment text, and reviewer identifier
                            long shapeId = annotation.ShapeID;                     // Primitive long
                            string commentText = annotation.Comment.Value ?? string.Empty;
                            int reviewerId = annotation.ReviewerID.Value;          // Primitive int

                            // Insert the comment into the database
                            InsertComment(connectionString, shapeId, commentText, reviewerId);
                        }
                    }
                }

                Console.WriteLine("Comment extraction and database insertion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Inserts a comment record into the Comments table.
        /// Expected table schema: Comments(ShapeId BIGINT, CommentText NVARCHAR(MAX), ReviewerId INT)
        /// </summary>
        static void InsertComment(string connStr, long shapeId, string comment, int reviewerId)
        {
            const string insertSql = @"
                INSERT INTO Comments (ShapeId, CommentText, ReviewerId)
                VALUES (@ShapeId, @CommentText, @ReviewerId);";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(insertSql, conn))
            {
                cmd.Parameters.Add("@ShapeId", SqlDbType.BigInt).Value = shapeId;
                cmd.Parameters.Add("@CommentText", SqlDbType.NVarChar, -1).Value = comment;
                cmd.Parameters.Add("@ReviewerId", SqlDbType.Int).Value = reviewerId;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }