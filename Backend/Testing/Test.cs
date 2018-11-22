﻿using System;
using Backend.Shared;
using Backend.Parts;
using System.Threading.Tasks;
using CTG.Database;
using CTG.Database.Models;
using Backend.Systems.CondensedSystem;
using Backend.Systems;
using System.Collections.Generic;

namespace Backend.Testing
{
    class Programs
    {
        static void Main(string[] args)
        {
            var DatabaseManager = Shared.DBconnection.GetManager();
            try
            {
                Console.WriteLine("Connection Successful");

                //Equipment.AddEquipment.addEquipment("Dell Inspiron 7559", "Office B Storage Closet", "has radioactive robotics testing software");
                //Deletion.Delete("parts", 7);

                PartsTable table = PartsTable.GetPartsTable();
                Console.WriteLine(table.Data[2].serializable);

                //int result = AddToSystem.Add(2, 1111);
                //Console.WriteLine(result);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                DatabaseManager.GetConnection().Close();
            }
        }
    }
}