using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Repositories
{
    public class BrugerRepositorySQL : IBruger
    {
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg";


        public BrugerRepositorySQL()
        {
        }

        public Bruger[] getAll()
        {
            var result = new List<Bruger>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Bruger\"";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        Console.WriteLine("Id=" + Id);
                        var Fornavn = reader.GetString(1);
                        var Efternavn = reader.GetString(2);
                        var Telefonnummer = reader.GetInt32(3);
                        var Adresse = reader.GetString(4);
                        var Email = reader.GetString(5);
                        var Fødselsdag = reader.GetDateTime(6);
                        var Password = reader.GetString(7);
                        var Iskoordinator = reader.GetBoolean(8);

                        Bruger b = new Bruger
                        {
                            Id = Id,
                            Fornavn = Fornavn,
                            Efternavn = Efternavn,
                            Telefonnummer = Telefonnummer,
                            Adresse = Adresse,
                            Email = Email,
                            Fødselsdag = Fødselsdag,
                            Password = Password,
                            IsKoordinator = Iskoordinator
                        };
                        result.Add(b);
                    }
                }
            }
            return result.ToArray();
        }

        public void Add(Bruger bruger)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO public.Bruger (Id, Fornavn, Efternavn, Telefonnummer, Adresse, Email, Fødselsdag, Password, IsKoordinator)
                                                    VALUES ($Id ,$Fornavn, $Efternavn, $Telefonnummer, $Adresse, $Email, $Fødselsdag, $Password, $IsKoordinator)";
                command.Parameters.AddWithValue("$Id", GetNextId() + 1);
                command.Parameters.AddWithValue("$Fornavn", bruger.Fornavn);
                command.Parameters.AddWithValue("$Efternavn", bruger.Efternavn);
                command.Parameters.AddWithValue("$Telefonnummer", bruger.Telefonnummer);
                command.Parameters.AddWithValue("$Adresse", bruger.Adresse);
                command.Parameters.AddWithValue("$Email", bruger.Email);
                command.Parameters.AddWithValue("$Fødselsdag", bruger.Fødselsdag);
                command.Parameters.AddWithValue("$Password", bruger.Password);
                command.Parameters.AddWithValue("$IsKoordinator", bruger.IsKoordinator);
                command.ExecuteNonQuery();
            }
        }

        public int GetNextId()
        {
            int id = 0;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT MAX(Id) FROM public.Bruger";

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

        public void DeleteBruger(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"DELETE FROM public.Bruger WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateBruger(Bruger bruger)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

            
            }
        }



    }
    
}