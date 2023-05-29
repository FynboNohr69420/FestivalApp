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
        // Connection string til databasen. Pooling slåes fra for at sikre at Elephant ikke overvældes af simultane forbindelser. (Der er et limit på 10 simultane forbindelser i vores plan)
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
                command.CommandText = "SELECT * FROM \"FullViewAdmin\"";


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
                        var isLocked = reader.GetBoolean(8);
                        var Pladser_Tilbage = reader.GetInt32(10);

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
                            isLocked = isLocked,
                            Pladser_Tilbage = Pladser_Tilbage
                        };
                        result.Add(b);
                    }
                }
                connection.Close();
            }
            return result.ToArray();
        }

        public Vagt[] getAvailable(int brugerid)
        {
            var result = new List<Vagt>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"BrugerView\"";


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
                        var Antal_Pladser = reader.GetInt32(6);
                        var Pladser_Tilbage = 0;
                        if (reader.GetInt32(7) != null)
                        {
                            Pladser_Tilbage = reader.GetInt32(7);
                        }
                        
                        Vagt b = new Vagt
                        {
                            ID = Id,
                            Navn = Navn,
                            Point = Point,
                            Start = Start,
                            Slut = Slut,
                            Antal = Antal_Pladser,
                            Beskrivelse = Beskrivelse,
                            Pladser_Tilbage = Pladser_Tilbage
                        };
                        result.Add(b);
                    }
                }
                connection.Close();
            }
            return result.ToArray();
        }

        public Kategori[] getAllKategori()
        {
            var result = new List<Kategori>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Kategori\"";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        var Navn = reader.GetString(1);

                        Kategori b = new Kategori
                        {
                            ID = Id,
                            Navn = Navn
                        };
                        result.Add(b);
                    }
                }
                connection.Close();
            }
            return result.ToArray();
        }

        public Vagt[] getAllMine(int b_id)
        {
            var result = new List<Vagt>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"BrugersVagter\" WHERE \"Bruger_ID\" =" + b_id;


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
                        var isLocked = reader.GetBoolean(8);
                        var Pladser_Tilbage = reader.GetInt32(10);

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
                            isLocked = isLocked,
                            Pladser_Tilbage = Pladser_Tilbage
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

        public void TagVagt(Vagt vagt, int bruger)
        {
            var result = 0;
            int vagtID;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT \"ID\" FROM \"Bemanding\" WHERE \"Vagt_ID\" =" + vagt.ID + "LIMIT 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                    vagtID = result;
                }
                connection.Close();
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE \"Bemanding\" SET \"Vagt_ID\" = @ID, \"Bruger_ID\" = @Bruger_ID WHERE \"ID\" =" + vagtID;
                command.Parameters.AddWithValue("@ID", vagt.ID);
                command.Parameters.AddWithValue("@Bruger_ID", bruger);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AfmeldVagt(Vagt vagt, int bruger)
        {
            
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE \"Bemanding\" SET \"Bruger_ID\" = NULL WHERE \"Vagt_ID\" =" + vagt.ID + "AND \"Bruger_ID\" =" + bruger;
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void DeleteVagt(int Id) 
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

        public async Task ToggleLockStatus(Vagt vagt, bool currentlockstatus)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                if (currentlockstatus == true)
                {
                    command.CommandText = "UPDATE \"Vagt\" SET \"isLocked\" = false WHERE \"ID\" =" + vagt.ID;
                }
                else
                {
                    command.CommandText = "UPDATE \"Vagt\" SET \"isLocked\" = true WHERE \"ID\" =" + vagt.ID;
                }

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }

}
