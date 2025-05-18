using System;
using System.Collections.Generic;
using System.Data.Common;
using Console_test_task.Models;
using MySql.Data.MySqlClient;

namespace Console_test_task
{
    internal class UsingDB // Класс для махинаций с данными из БД 
    {
        public static List<Pallet> pallets = LoadPalletsWithBoxes();
        public static List<Pallet> LoadPalletsWithBoxes()
        {
            List<Pallet> pallets = new();

            using DbConnection conn = dbConnection.Connect();
            conn.Open();

            using var palletCmd = conn.CreateCommand();
            palletCmd.CommandText = "SELECT * FROM pallets";
            using var palletReader = palletCmd.ExecuteReader();

            while (palletReader.Read())
            {
                var pallet = new Pallet
                {
                    Id = Convert.ToInt32(palletReader["id"]),
                    Width = Convert.ToDecimal(palletReader["width"]),
                    Height = Convert.ToDecimal(palletReader["height"]),
                    Depth = Convert.ToDecimal(palletReader["depth"])
                };
                pallets.Add(pallet);
            }
            palletReader.Close();

            // Загрузим коробки
            using var boxCmd = conn.CreateCommand();
            boxCmd.CommandText = "SELECT * FROM boxes";
            using var boxReader = boxCmd.ExecuteReader();

            while (boxReader.Read())
            {
                var box = new Box
                {
                    Id = Convert.ToInt32(boxReader["id"]),
                    Width = Convert.ToDecimal(boxReader["width"]),
                    Height = Convert.ToDecimal(boxReader["height"]),
                    Depth = Convert.ToDecimal(boxReader["depth"]),
                    Weight = Convert.ToDecimal(boxReader["weight"]),
                    ProductionDate = boxReader["production_date"] == DBNull.Value ? null : Convert.ToDateTime(boxReader["production_date"]),
                    ExpirationDate = boxReader["expiration_date"] == DBNull.Value ? null : Convert.ToDateTime(boxReader["expiration_date"]),
                    PalletId = Convert.ToInt32(boxReader["pallet_id"])
                };

                var pallet = pallets.Find(p => p.Id == box.PalletId);
                pallet?.Boxes.Add(box);
            }

            return pallets;
        }




    }
}
