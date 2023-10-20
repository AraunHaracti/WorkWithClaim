using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Fekus2.Models;
using MySql.Data.MySqlClient;

namespace Fekus2.ViewModels;

public class ClaimStatusTableViewModel : ViewModelBase
{
    public ObservableCollection<ClaimStatus> ItemsOnDataGrid { get; set; }

    public ClaimStatusTableViewModel()
    {
        string sql = $"select * from {nameof(ClaimStatus)}";
        
        List<ClaimStatus> itemsFromDatabase = new List<ClaimStatus>();
        
        Database.Open();
    
        MySqlDataReader reader = Database.GetData(sql);
    
        while (reader.Read() && reader.HasRows)
        {
            var currentItem = new ClaimStatus();

            PropertyInfo[] propertyInfos = typeof(ClaimStatus).GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
            }

            itemsFromDatabase.Add(currentItem);
        }
        
        Database.Exit();

        ItemsOnDataGrid = new ObservableCollection<ClaimStatus>(itemsFromDatabase);
    }
}