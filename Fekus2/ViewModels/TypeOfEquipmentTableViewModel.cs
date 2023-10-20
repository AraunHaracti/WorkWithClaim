﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Fekus2.Models;
using Fekus2.Views.TypeOfEquipmentViews;
using MySql.Data.MySqlClient;
using ReactiveUI;

namespace Fekus2.ViewModels;

public class TypeOfEquipmentTableViewModel : ViewModelBase
{
    Type _type = typeof(TypeOfEquipment);
    
    private readonly Window _parentWindow;
    
    private List<TypeOfEquipment> _itemsFromDatabase;

    private List<TypeOfEquipment> _itemsFilter;
    
    private ObservableCollection<TypeOfEquipment> _itemsOnDataGrid;
    
    private string _searchQuery = "";
    
    private int _indexTake = 0;
    
    public TypeOfEquipment CurrentItem { get; set; }
    
    private string _sql = $"select * from {typeof(TypeOfEquipment).Name}";

    public ObservableCollection<TypeOfEquipment> ItemsOnDataGrid
    {
        get => _itemsOnDataGrid;
        set
        {
            _itemsOnDataGrid = value;
            this.RaisePropertyChanged();
        }
    }
    
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            UpdateItems();
        }
    }
    
    private int IndexTake
    {
        get => _indexTake;
        set
        {
            _indexTake = value;
            
            if (_indexTake > _itemsFilter.Count - 10)
            {
                _indexTake = _itemsFilter.Count - 10;
            }
            
            if (_indexTake < 0)
            {
                _indexTake = 0;
            } 
        }
    }
    
    public TypeOfEquipmentTableViewModel(Window window)
    {
        _parentWindow = window;
        
        UpdateItems();
        
        PropertyChanged += OnSearchQueryChanged;
    }
    
    
    private void OnSearchQueryChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(SearchQuery)) return;
        Search();
    }

    private void Search()
    {
        if (SearchQuery == "")
        {
            _itemsFilter = new(_itemsFromDatabase);
        }

        _itemsFilter = new(_itemsFromDatabase.Where(it =>
        {
            PropertyInfo[] propertyInfos = _type.GetProperties();
            foreach (PropertyInfo f in propertyInfos)
            {
                if (f.GetValue(it).ToString().ToLower().Contains(SearchQuery.ToLower()))
                    return true;
            }

            return false;
        }));
    }

    public void UpdateItems()
    {
        GetDataFromDatabase();
        Search();
        TakeItems(TakeItemsEnum.FirstItems);
    }
    
    private void GetDataFromDatabase()
    {

        _itemsFromDatabase = new List<TypeOfEquipment>();
        
        Database.Open();
    
        MySqlDataReader reader = Database.GetData(_sql);
    
        while (reader.Read() && reader.HasRows)
        {
            var currentItem = new TypeOfEquipment();

            PropertyInfo[] propertyInfos = _type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
            }

            _itemsFromDatabase.Add(currentItem);
        }
        
        Database.Exit();
    }
    
    public void AddItem()
    {
        var itemAddWindowView = new ChangeItemView(UpdateItems);
        itemAddWindowView.ShowDialog(_parentWindow);
        UpdateItems();
    }

    public void DeleteItem()
    {
        if (CurrentItem == null) return;

        var propertyInfos = new List<PropertyInfo>(typeof(TypeOfEquipment).GetProperties().Where(it => it.Name.ToLower().Contains("id")));
        
        string sql = $"delete from {typeof(TypeOfEquipment).Name} where {typeof(TypeOfEquipment).Name}." +
                     $"{propertyInfos[0].Name} = {propertyInfos[0].GetValue(CurrentItem)}";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        UpdateItems();
    }

    public void EditItem()
    {
        if (CurrentItem == null) return;
        var itemEditWindowView = new ChangeItemView(UpdateItems, CurrentItem);
        itemEditWindowView.ShowDialog(_parentWindow);
    }

    public void TakeItems(TakeItemsEnum takeItems)
    {
        switch (takeItems)
        {
            case TakeItemsEnum.FirstItems:
                IndexTake = 0;
                break;
            case TakeItemsEnum.LastItems:
                IndexTake = _itemsFilter.Count - 10;
                break;
            case TakeItemsEnum.NextItems:
                IndexTake += 10;
                break;
            case TakeItemsEnum.PreviousItems:
                IndexTake -= 10;
                break;
        }

        ItemsOnDataGrid = new ObservableCollection<TypeOfEquipment>(_itemsFilter.GetRange(IndexTake, _itemsFilter.Count > 10 ? 10 : _itemsFilter.Count));
    }
}