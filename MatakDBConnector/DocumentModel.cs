using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class DocumentModel : Document
    {
        public int AddNewDocument(Document newDocument, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;                                     
                
                    command.CommandText =
                        "INSERT INTO document (route_id, filename, created, updated, created_by_user_id, updated_by_user_id, is_landmark) VALUES (@route_id, @filename, @created, @updated, @created_by_user_id, @updated_by_user_id, @is_landmark) RETURNING document_id";
                    newDocumentCommandHelper(newDocument, command, true);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public int UpdateDocumentId(Document document, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;                                     
                
                    command.CommandText =
                        "UPDATE document SET route_id = (@route_id), filename = (@filename), created = (@created), updated = (@updated), created_by_user_id = (@created_by_user_id), updated_by_user_id = (@updated_by_user_id), is_landmark = (@is_landmark) WHERE document_id = (@document_id) RETURNING document_id";
                    newDocumentCommandHelper(document, command, false);
                    command.Parameters.AddWithValue("document_id", document.DocumentId);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public string GetDocumentHandleByDocId(int documentId, out string errorMessage)
        {
            errorMessage = null;
            string documentHandle = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_id, filename FROM document WHERE document_id = (@documentId)";
                    command.Parameters.AddWithValue("documentId", documentId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        documentHandle = reader.GetInt32(0) + "_" + documentId + "_" +reader.GetString(1);
                    }

                    return documentHandle;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public Document GetDocumentById(int documentId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT document_id, route_id, filename, created, updated, created_by_user_id, updated_by_user_id, is_landmark FROM document WHERE document_id = (@documentId)";
                    command.Parameters.AddWithValue("documentId", documentId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return DocumentMaker(reader);
                    }

                    errorMessage = "Specified document ID was not found in the database - " + documentId;
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public List<Document> GetAllDocumentsByRouteLanmdmarkId(int routeId, Boolean isLandmark, out string errorMessage)
        {
            errorMessage = null;
            List<Document> documents = new List<Document>();
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT document_id, route_id, filename, created, updated, created_by_user_id, updated_by_user_id, is_landmark FROM document WHERE route_id = (@routeId) AND is_landmark = (@isLandmark)";
                    command.Parameters.AddWithValue("routeId", routeId);
                    command.Parameters.AddWithValue("isLandmark", isLandmark);
                    
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Document document = new Document();
                        documents.Add(document.DocumentMaker(reader));
                    }

                    return documents;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
    }
}