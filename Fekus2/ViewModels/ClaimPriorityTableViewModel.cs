using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Fekus2.Models;
using MySql.Data.MySqlClient;

namespace Fekus2.ViewModels;

public class ClaimPriorityTableViewModel
{
    public ObservableCollection<ClaimPriority> ItemsOnDataGrid { get; set; }

    public ClaimPriorityTableViewModel()
    {
        string sql = $"select * from {nameof(ClaimPriority)}";

        List<ClaimPriority> itemsFromDatabase = new List<ClaimPriority>();
        
        Database.Open();
    
        MySqlDataReader reader = Database.GetData(sql);
    
        while (reader.Read() && reader.HasRows)
        {
            var currentItem = new ClaimPriority();

            PropertyInfo[] propertyInfos = typeof(ClaimPriority).GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
            }

            itemsFromDatabase.Add(currentItem);
        }
        
        Database.Exit();
        
        ItemsOnDataGrid = new ObservableCollection<ClaimPriority>(itemsFromDatabase);
    }
}