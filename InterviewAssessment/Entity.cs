﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DomainModelEditor
{
    public class Entity : INotifyPropertyChanged
    {
        public Entity(int id, string name, int x, int y)
        {
            Id = id;
            Name = name;
            _x = x;
            _y = y;
        }

        public int Id { get; }

        public string Name { get; }

        private int _x;
        public int X //Making X ready to be changed when the cursor is dragging the elemenet
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _y;
        public int Y //Making Y ready to be changed when the cursor is dragging the elemenet
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}