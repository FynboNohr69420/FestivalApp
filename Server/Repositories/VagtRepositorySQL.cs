using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Repositories
{
    public class VagterRepositorySQL : IVagt
    {
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg";


        public VagterRepositorySQL()
        {
        }

        public Vagt[] getAll()
        {
            var result = new List<Vagt>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Navn\"";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        var Navn = reader.GetString(1);
                        var Kategori = reader.GetString(2);
                        var Point = reader.GetInt32(3);
                        var Start = reader.GetString(4);
                        var Slut = reader.GetString(5);
                        var Antal = reader.GetInt32(6);
                        var Beskrivelse = reader.GetString(7);

                        Vagt b = new Vagt
                        {
                            ID = Id,
                            Navn = Navn,
                            Kategori = Kategori,
                            Point = Point,
                            Start = Start,
                            Slut = Slut,
                            Antal = Antal,
                            Beskrivelse = Beskrivelse,
                        };
                        result.Add(b);
                    }
                }
            }
            return result.ToArray();
        }

        public void Add(Vagt vagt)
        {

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Id\" (\"Navn\", \"Kategori\", \"Point\", \"Start\", \"Slut\", \"Antal\", \"Beskrivelse\",) VALUES (\'@Navn\', \'@Kategori\', @Point, \'@Start\', \'@Slut\', \'@Antal\', \'@Beskrivelse\')";
                command.Parameters.AddWithValue("@Navn", vagt);
                command.Parameters.AddWithValue("@Kategori", vagt);
                command.Parameters.AddWithValue("@Point", vagt);
                command.Parameters.AddWithValue("@Start", vagt);
                command.Parameters.AddWithValue("@Slut", vagt);
                command.Parameters.AddWithValue("@Antal", vagt);
                command.Parameters.AddWithValue("@Beskrivelse", vagt);
                command.ExecuteNonQuery();
            }
        }

        //public int GetNextId()
        //{
        //    int id = 0;

        //    using (var connection = new NpgsqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        var command = connection.CreateCommand();
        //        command.CommandText = @"SELECT MAX(Id) FROM public.Bruger";

        //        using (var reader = command.ExecuteReader())
        //        {
        //            {
        //                while (reader.Read())
        //                {
        //                    id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
        //                }
        //            }
        //        }
        //    }
        //    return id;
        //}

        public void DeleteVagt(int Id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM \"NAVN\" WHERE \"ID\" = @ID";
                command.Parameters.AddWithValue("@ID", Id); ;
                command.ExecuteNonQuery();
            }
        }

        public void UpdateVagt(Vagt vagt)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();


            }
        }
    }
}