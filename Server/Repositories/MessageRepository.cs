using System.Collections.Generic;
using System.Linq;//z pętli albo z tego korzystaj - to te Any() Where()
using System;
using ConsoleWeb.Database;
using System.Data.SqlClient;
using System.Data;

public class MessageRepository
{
    //private readonly static List<MessageModel> Messages = new List<MessageModel>();

    public MessageModel SendMessage(MessageModel input)
    {
        using (var dbContext = new DbContext())
        {
            string sql = "INSERT INTO messages (senderid, recipientid, text, date) VALUES (@Recipientid, @Senderid, @Tekst, @Date)";
            dbContext.Connection.Open();

           var message = new MessageModel()
            {
                Id = input.Id,
                Senderid = input.Senderid,
                Recipientid = input.Recipientid,
                Text = input.Text,
                Sendate = input.Sendate
            };

            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@Recipientid", input.Recipientid);
                command.Parameters.AddWithValue("@Senderid", input.Senderid);
                command.Parameters.AddWithValue("@Tekst", input.Text);
                command.Parameters.AddWithValue("@Date", input.Sendate);
                command.ExecuteNonQuery();
            }
            return message;
        }
    }

   public List<MessageModel> GetMessages()
    {
        using (var dbContext = new DbContext())
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sql = $"select * from messages";
            dbContext.Connection.Open();
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var obecnyId = (int)reader["id"];
                        var obecnySenderId = (int)reader["senderid"];
                        var obecnyRecipientId = (int)reader["recipientid"];
                        var obecnyText = (String)reader["text"];
                        var obecnaData = (DateTime)reader["date"];

                        MessageModel message = new MessageModel
                        {
                            Id = obecnyId,
                            Recipientid = obecnyRecipientId,
                            Senderid = obecnySenderId,
                            Text = obecnyText,
                            Sendate = obecnaData,
                        };

                        messages.Add(message);
                    }
                }
                reader.Close();            
            }
            dbContext.Connection.Close();
            return messages;            
        }
    }

    public MessageModel GetMessage(int id)
    {
        using (var dbContext = new DbContext())
        {
            MessageModel message = new MessageModel();
            string sql = $"select * from messages where id = @id";
            dbContext.Connection.Open();
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        message.Id = (int)reader["id"];
                        message.Senderid = (int)reader["senderid"];
                        message.Recipientid = (int)reader["recipientid"];
                        message.Text = (String)reader["text"];
                        message.Sendate = (DateTime)reader["date"];
                    }
                }
                reader.Close();
            }
            dbContext.Connection.Close();
            return message;
        }
    }

    public MessageModel DeleteMessage(int id)
    {
        using (var dbContext = new DbContext())
        {
            string sql = "DELETE FROM messages WHERE id = @id";
            dbContext.Connection.Open();
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            dbContext.Connection.Close();
        }
            return GetMessage(id);
    }

    public string DeleteMessages()
    {
        using (var dbContext = new DbContext())
        {
            string sql = "DELETE FROM messages";
            dbContext.Connection.Open();
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.ExecuteNonQuery();
            }
            dbContext.Connection.Close();
            return "Wiadomości usunięte!";
        }
    }
}