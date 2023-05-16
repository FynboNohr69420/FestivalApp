﻿using System;
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
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg;Pooling=false";


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
                        var Start = reader.GetDateTime(3);
                        var Slut = reader.GetDateTime(4);
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
                connection.Close();
            }
            return result.ToArray();
        }

        public void AddVagt(Vagt vagt)
        {
            int? resID = null; // Placeholder variabel til ID'et af indsat data
            
            // Indsætter stamdata om vagten i vagt tabellen
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Vagt\"(\"Navn\", \"Point\", \"Start\", \"Slut\", \"Beskrivelse\", \"Kategori_ID\", \"Antal_Pladser\", \"isLocked\") VALUES (@Navn, @Point, @Start, @Slut, @Beskrivelse, @Kategori, @Antal, @isLocked) RETURNING \"ID\"";
                command.Parameters.AddWithValue("@Navn", vagt.Navn);
                command.Parameters.AddWithValue("@Kategori", vagt.Kategori);
                command.Parameters.AddWithValue("@Point", vagt.Point);
                command.Parameters.AddWithValue("@Start", vagt.Start);
                command.Parameters.AddWithValue("@Slut", vagt.Slut);
                command.Parameters.AddWithValue("@Antal", vagt.Antal);
                command.Parameters.AddWithValue("@Beskrivelse", vagt.Beskrivelse);
                command.Parameters.AddWithValue("@isLocked", vagt.isLocked);
                resID = (Int32)command.ExecuteScalar(); // Kører query'en, men returnerer 'RETURNING' værdien. (Se CommandText herover)
                connection.Close();
            }
            // Indsætter linjer i bemandings-tabellen
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Bemanding\"(\"Vagt_ID\") VALUES (@ID)";
                command.Parameters.AddWithValue("@ID", resID);

                for (int i = 1; i <= vagt.Antal; i++) // Loop der indsætter den mængde bemandings-linger som er defineret i vagten
                    {
                    command.ExecuteNonQuery();
                    }
                connection.Close();
            }
        }

        public void DeleteVagt(int Id) //KFN: Opdater så bemanding fjernes først
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM \"Bemanding\" WHERE \"Vagt_ID\" = @ID";
                command.Parameters.AddWithValue("@ID", Id);
                command.ExecuteNonQuery();
                connection.Close();
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM \"Vagt\" WHERE \"ID\" = @ID";
                command.Parameters.AddWithValue("@ID", Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateVagt(Vagt vagt)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                

                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE \"Vagt\" SET \"Navn\"=@Navn, \"Point\"=@Point, \"Start\"=@Start, \"Slut\"=@Slut, \"Beskrivelse\"=@Beskrivelse, \"Kategori_ID\"=@Kategori_ID, \"Antal_Pladser\"=@Antal_Pladser WHERE \"ID\" = @ID";
                command.Parameters.AddWithValue("@Navn", vagt.Navn);
                command.Parameters.AddWithValue("@Point", vagt.Point);
                command.Parameters.AddWithValue("@Start", vagt.Start);
                command.Parameters.AddWithValue("@Slut", vagt.Slut);
                command.Parameters.AddWithValue("@Beskrivelse", vagt.Beskrivelse);
                command.Parameters.AddWithValue("@Kategori_ID", vagt.Kategori );
                command.Parameters.AddWithValue("@Antal_Pladser", vagt.Antal);
                command.Parameters.AddWithValue("@ID", vagt.ID);
                command.ExecuteNonQuery();
                connection.Close();
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
                        var Start = reader.GetDateTime(3);
                        var Slut = reader.GetDateTime(4);
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
                connection.Close();
            }
            return result;
        }

        
         
       
    }

}
