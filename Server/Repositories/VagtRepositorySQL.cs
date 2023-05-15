using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MongoDB.Driver.Core.Configuration;

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
                        var KategoriID = reader.GetInt32(6);
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

                command.CommandText = "INSERT INTO \"Vagt\"(\"Navn\", \"Point\", \"Start\", \"Slut\", \"Beskrivelse\", \"Kategori_ID\", \"Antal_Pladser\") VALUES (@Navn, @Point, @Start, @Slut, @Beskrivelse, @Kategori, @Antal)";
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

                command.CommandText = "DELETE FROM \"Vagt\" WHERE \"ID\" = @ID";
                command.Parameters.AddWithValue("@ID", Id); ;
                command.ExecuteNonQuery();
            }
        }

        public void UpdateVagt(Vagt vagt)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                string datoString = vagt.Start.Date.ToString();
                string datoString2 = vagt.Slut.Date.ToString();

                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE \"Vagt\" SET \"Navn\"=@Navn, \"Point\"=@Point, \"Start\"=@Start, \"Slut\"=@Slut, \"Beskrivelse\"=@Beskrivelse, \"Kategori_ID\"=@Kategori, \"Antal_Pladser\"=Antal WHERE \"ID\" = @Id";
                command.Parameters.AddWithValue("@Navn", vagt.Navn);
                command.Parameters.AddWithValue("@Point", vagt.Point);
                command.Parameters.AddWithValue("@Start", datoString);
                command.Parameters.AddWithValue("@Slut", datoString2);
                command.Parameters.AddWithValue("@Beskrivelse", vagt.Beskrivelse);
                command.Parameters.AddWithValue("@Kategori_ID", vagt.Kategori );
                command.Parameters.AddWithValue("@Antal_Pladser", vagt.Antal);
                command.Parameters.AddWithValue("@ID", vagt.ID);
                command.ExecuteNonQuery();
            }
        }

        public Vagt GetVagt(int VagtID)
        {
            var result = new Vagt();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Vagt\" WHERE \"ID\" =" + "'" + VagtID + "'";
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
                        var KategoriID = reader.GetInt32(6);
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
                        result = b;
                    }
                }
            }
            return result;
        }
    }

}
