using GameVault.DAL;
using GameVault.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameVault.BLL
{
    internal class GameBLL
    {
       
        private GameDAL dataAccess = new GameDAL();

        //Read
        public List<Game> GetAllGames()
        {
            return dataAccess.GetAllGames();
        }

        //Smart save button
        public void SaveGame(Game game)
        {
            // validate
            if (String.IsNullOrWhiteSpace(game.Title))
            {
                throw new Exception("The title cannot be empty.");
            }
            if (String.IsNullOrWhiteSpace(game.Genre))
            {
                throw new Exception("The genre cannot be empty.");
            }
            if (game.Price < 0)
            {
                throw new Exception("The title cannot be negative.");
            }
            // dual logic
            if(game.Id == 0)
            {
                dataAccess.InsertGame(game);
            }
            else
            {
                dataAccess.UpdateGame(game);
            }
        }

        // Delete
        public void DeleteGame(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Please select a valid game to delete.");
            }
            dataAccess.DeleteGame(id);
        }

        



        

    }
}
