using System;
using System.Data;
using System.Data.SqlClient; 
using System.Collections.Generic;
using GameVault.Models; 

namespace GameVault.DAL
{
    internal class GameDAL
    {
        // how i installed ssms with the Trust serverCertification i must set it as true
        private string connectionString = "Server=localhost;Database=GameVault;User Id=sa; Password=Dylan072912.;TrustServerCertificate=True;";

        // read methd 
        public List<Game> GetAllGames()
        {
            // Create a empty List where we're going to store all games that arrive from bd
            List<Game> list = new List<Game>();

            // the using block allow us close and destroy automatically when it finish 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                using (SqlCommand command = new SqlCommand("sp_GetAllGames", connection))
                {
                    // specificate that is a stored procedure 
                    command.CommandType = CommandType.StoredProcedure;

                    // open the connection
                    connection.Open();

                    // execute the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // while the reader find the bd rows 
                        while (reader.Read())
                        {
                            // We init a new empty mold
                            Game game = new Game();

                            // we fill the mold with current date rows
                            game.Id = Convert.ToInt32(reader["Id"]);
                            game.Title = reader["Title"].ToString();
                            game.Genre = reader["Genre"].ToString();
                            game.Price = Convert.ToDecimal(reader["Price"]);

                            // add the game to our list
                            list.Add(game);
                        }
                    }
                }
            }
            // return the list
            return list;
        }

        // Create method 
        public void InsertGame(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_InsertGame", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    // send data to Stored Procedure
                    command.Parameters.AddWithValue("@Title", game.Title);
                    command.Parameters.AddWithValue("@Genre", game.Genre);
                    command.Parameters.AddWithValue("@price", game.Price);

                    connection.Open();

                    // we use ExecuteNonQuery to commands which don't return tables (Insert, Update, Delete)
                    command.ExecuteNonQuery();
                    // 
                }
            }
        }

        //Update method 
        public void UpdateGame(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_UodateGame", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // We need the id to know which game we're going to modify
                    command.Parameters.AddWithValue("@Id", game.Id);
                    command.Parameters.AddWithValue("@Title", game.Title);
                    command.Parameters.AddWithValue("@Genre", game.Genre);
                    command.Parameters.AddWithValue("@Price", game.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                    
                }
            }


        }

        // Delete method
        public void DeleteGame(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand("sp_DeleteGame", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }





    }
}
