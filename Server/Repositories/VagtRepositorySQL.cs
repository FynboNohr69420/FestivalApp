using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Repositories
{
    public class VagtRepositorySQL : IVagt
    {
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg";


        public VagtRepositorySQL()
        {
        }

        public Vagt[] getAll()
        {
            var result = new List<Vagt>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Vagt\"";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        var Navn = reader.GetString(1);
                        var Point = reader.GetInt32(2);
                        var Start = DateTime.Parse(reader.GetString(3).Replace(".", "/").Remove(10, 9));
                        var Slut = DateTime.Parse(reader.GetString(4).Replace(".", "/").Remove(10, 9));
                        var Beskrivelse = reader.GetString(5);
                        var KategoriID = reader.GetString(6);
                        var Antal_Pladser = reader.GetInt32(7);

                        Vagt b = new Vagt
                        {
                            ID = Id,
                            Navn = Navn,
                            Kategori = KategoriID,
                            Point = Point,
                            Start = Start,
                            Slut = Slut,
                            Antal = Antal_Pladser,
                            Beskrivelse = Beskrivelse,
                        };
                        result.Add(b);
                    }
                }
            }
            return result.ToArray();
        }

        public void AddVagt(Vagt vagt)
        {

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Vagt\"(\"Id\", \"Navn\", \"Point\", \"Start\", \"Slut\", \"Beskrivelse\", \"Kategori_ID\") VALUES (@Navn, @Point, @Start, @Slut, @Beskrivelse, @Kategori, @Antal)";
                command.Parameters.AddWithValue("@Navn", vagt.Navn);
                command.Parameters.AddWithValue("@Kategori", vagt.Kategori);
                command.Parameters.AddWithValue("@Point", vagt.Point);
                command.Parameters.AddWithValue("@Start", vagt.Start);
                command.Parameters.AddWithValue("@Slut", vagt.Slut);
                command.Parameters.AddWithValue("@Antal", vagt.Antal);
                command.Parameters.AddWithValue("@Beskrivelse", vagt.Beskrivelse);
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