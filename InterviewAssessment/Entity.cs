using System.Collections.Generic;
using System.ComponentModel;
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
            Attributes = new Dictionary<string, object>(); //Dictionary to collect newly added attributes
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
                    OnPropertyChangednew();
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
                    OnPropertyChangednew();
                }
            }
        }
        public Dictionary<string, object> Attributes { get; private set; } //Defining the object which we will use to reflect our attribute-value pair within the entity box
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChangednew([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Height => 50 + (Attributes.Count * 30); // Increasing the height by 30 for per new attribute addition

        public void AddAttribute(string attributeName, string attributeValue)
        {
            if (!Attributes.ContainsKey(attributeName)) // Making sure unique attribute name is present for each attribute
            {
                Attributes.Add(attributeName, attributeValue);
                OnPropertyChangednew(nameof(Attributes));
            }
        }
    }
}