using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;

namespace Server.Repositories
{
    public class FrivilligeRepositorySQL : IFrivillig
    {
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg";

        public FrivilligeRepositorySQL()
        {
        }

        public Frivillig[] getAll()
        {
            var result = new List<Frivillig>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Vagt";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        Console.WriteLine("Id=" + Id);
                        var Navn = reader.GetString(1);
                        var Email = reader.GetString(2);
                        var Fødselsdagsdato = reader.GetDateTime(3);
                        var Adresse = reader.GetString(4);
                        var Telefonnummer = reader.GetInt32(5);

                        Frivillig b = new Frivillig { Id = Id, Navn = Navn, Email = Email, Fødselsdagsdato = Fødselsdagsdato, Adresse = Adresse, Telefonnummer = Telefonnummer};
                        result.Add(b);
                    }
                }
            }
            return result.ToArray();
        }

        public void Add(Frivillig frivillig)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO ShoppingList (Id, Navn, Email, Fødselsdagsdato, Adresse, Telefonnummer) VALUES ($Id ,$Navn, $Email, $Fødselsdagsdato, $Adresse, $Telefonnummer)";
                command.Parameters.AddWithValue("$Id", GetNextId() + 1);
                command.Parameters.AddWithValue("$Navn", frivillig.Navn);
                command.Parameters.AddWithValue("$Email", frivillig.Email);
                command.Parameters.AddWithValue("$Fødselsdagsdato", frivillig.Fødselsdagsdato);
                command.Parameters.AddWithValue("$Adresse", frivillig.Adresse);
                command.Parameters.AddWithValue("$Telefonnummer", frivillig.Telefonnummer);
                command.ExecuteNonQuery();
            }
        }

        public int GetNextId()
        {
            int id = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT MAX(Id) FROM ShoppingList";

                using (var reader = command.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                        }
                    }
                }
            }
            return id;
        }

    }

}