using System;
using Common.Model;
using Dapper;
using Npgsql;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Http;

namespace Server.Repositories
{
    public class BrugerRepositorySQL : IBruger
    {
        private const string connectionString = "UserID=eehvkyxg;Password=DpGHcrCDBfK_RrcdKdwSNiUR3t_PWx-1;Host=balarama.db.elephantsql.com;Port=5432;Database=eehvkyxg;Pooling=false";


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
                        var Fornavn = reader.GetString(1);
                        var Efternavn = reader.GetString(2);
                        var Telefonnummer = reader.GetInt32(3);
                        var Adresse = reader.GetString(4);
                        var Email = reader.GetString(5);
                        var Password = reader.GetString(6);
                        var Iskoordinator = reader.GetBoolean(7);
                        var Fødselsdag = DateTime.Parse(reader.GetString(8).Replace(".", "/").Replace("-", "/").Remove(10, 9)); // Fjerner tidspunkt og erstatter . med / så strengen kan konverteres til en dato

                        Bruger b = new Bruger
                        {
                            ID = Id,
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
                connection.Close();
            }
            return result.ToArray();
        }

        public Bruger getSpecific(string email)
        {
            var result = new Bruger();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Bruger\" WHERE \"Email\" =" + "'" + email + "'";


                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        var Fornavn = reader.GetString(1);
                        var Efternavn = reader.GetString(2);
                        var Telefonnummer = reader.GetInt32(3);
                        var Adresse = reader.GetString(4);
                        var Email = reader.GetString(5);
                        var Password = reader.GetString(6);
                        var Iskoordinator = reader.GetBoolean(7);
                        var Fødselsdag = DateTime.Parse(reader.GetString(8).Replace(".", "/").Remove(10, 9)); // Fjerner tidspunkt og erstatter . med / så strengen kan konverteres til en dato

                        Bruger b = new Bruger
                        {
                            ID = Id,
                            Fornavn = Fornavn,
                            Efternavn = Efternavn,
                            Telefonnummer = Telefonnummer,
                            Adresse = Adresse,
                            Email = Email,
                            Fødselsdag = Fødselsdag,
                            Password = Password,
                            IsKoordinator = Iskoordinator
                        };
                        result = b;
                    }
                }
                connection.Close();
            }
            return result;
        }

        public void Add(Bruger bruger)
        {

            int? resID = null;

            using (var connection = new NpgsqlConnection(connectionString))
            {

                string datoString = bruger.Fødselsdag.Date.ToString();

                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Bruger\" (\"Fornavn\", \"Efternavn\", \"Telefonnummer\", \"Adresse\", \"Email\", \"Fødselsdag\", \"Password\", \"isKoordinator\") VALUES (@Fornavn, @Efternavn, @Telefonnummer, @Adresse, @Email, @Fødselsdag, @Password, @isKoordinator) RETURNING \"ID\"";
                command.Parameters.AddWithValue("@Fornavn", bruger.Fornavn);
                command.Parameters.AddWithValue("@Efternavn", bruger.Efternavn);
                command.Parameters.AddWithValue("@Telefonnummer", bruger.Telefonnummer);
                command.Parameters.AddWithValue("@Adresse", bruger.Adresse);
                command.Parameters.AddWithValue("@Email", bruger.Email);
                command.Parameters.AddWithValue("@Fødselsdag", datoString);
                command.Parameters.AddWithValue("@Password", bruger.Password);
                command.Parameters.AddWithValue("@isKoordinator", false);
                resID = (Int32)command.ExecuteScalar();
                connection.Close();
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO \"Bruger_rolle\" (\"Bruger_id\", \"Rolle_id\") VALUES (@ID, 2)";
                command.Parameters.AddWithValue("@ID", resID);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeleteBruger(int Id)
        {

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM \"Bruger_rolle\" WHERE \"Bruger_id\" =@ID";
                command.Parameters.AddWithValue("@ID", Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM \"Bruger\" WHERE \"ID\" = @ID";
                command.Parameters.AddWithValue("@ID", Id); ;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateBruger(Bruger bruger)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                string datoString = bruger.Fødselsdag.Date.ToString();

                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE \"Bruger\" SET \"Fornavn\"=@Fornavn, \"Efternavn\"=@Efternavn, \"Telefonnummer\"=@Telefonnummer, \"Adresse\"=@Adresse, \"Email\"=@Email, \"Password\"=@Password, \"isKoordinator\"=@isKoordinator, \"Fødselsdag\"=@Fødselsdag WHERE \"ID\" = @ID"; 
                command.Parameters.AddWithValue("@Fornavn", bruger.Fornavn);
                command.Parameters.AddWithValue("@Efternavn", bruger.Efternavn);
                command.Parameters.AddWithValue("@Telefonnummer", bruger.Telefonnummer);
                command.Parameters.AddWithValue("@Adresse", bruger.Adresse);
                command.Parameters.AddWithValue("@Email", bruger.Email);
                command.Parameters.AddWithValue("@Fødselsdag", datoString);
                command.Parameters.AddWithValue("@Password", bruger.Password);
                command.Parameters.AddWithValue("@isKoordinator", bruger.IsKoordinator);
                command.Parameters.AddWithValue("@ID", bruger.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Bruger GetBruger(int brugerID)
        {
            var result = new Bruger();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM \"Bruger\" WHERE \"ID\" =" + "'" + brugerID + "'";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Id = reader.GetInt32(0);
                        var Fornavn = reader.GetString(1);
                        var Efternavn = reader.GetString(2);
                        var Telefonnummer = reader.GetInt32(3);
                        var Adresse = reader.GetString(4);
                        var Email = reader.GetString(5);
                        var Password = reader.GetString(6);
                        var Iskoordinator = reader.GetBoolean(7);
                        var Fødselsdag = DateTime.Parse(reader.GetString(8).Replace(".", "/").Remove(10, 9)); // Fjerner tidspunkt og erstatter . med / så strengen kan konverteres til en dato

                        Bruger b = new Bruger
                        {
                            ID = Id,
                            Fornavn = Fornavn,
                            Efternavn = Efternavn,
                            Telefonnummer = Telefonnummer,
                            Adresse = Adresse,
                            Email = Email,
                            Fødselsdag = Fødselsdag,
                            Password = Password,
                            IsKoordinator = Iskoordinator
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