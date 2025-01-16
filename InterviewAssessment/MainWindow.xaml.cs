using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DomainModelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EntityStore EntityStore { get; set; }

        private bool _isDragging; //True False value to represent the state of entity in terms of if it is moving or not by the effect of the cursor
        private Point _startPoint; //initial point before the move which will be updated by the current position
        private object _draggedData; //the entity itself which is our object, its name and id stays same but the coordinates are changing frequently while the move is happening and we see this change ( state of moving) on the UI immediately
        public MainWindow()
        {
            InitializeComponent();

            EntityStore = new EntityStore();

            var demoData = new[] { new Tuple<int, string, int, int>(1, "Order", 100, 100), new Tuple<int, string, int, int>(2, "OrderLine", 200, 200) };//This line was calling the initial dummy objects. Now I am able to read from entities.sqlite
            var entitiesFromDatabase = EntityStore.GetEntitiesFromDatabase();  // I am calling the new function I created which reads from entities.sqlite
            EntityStore.Load(entitiesFromDatabase); //I changed Demodata with entitiesFromDatabase which is the variable that contains info of id, name, x and y axis

            var entitiesBinding = new Binding { Source = EntityStore };
            EditorCanvas.SetBinding(ItemsControl.ItemsSourceProperty, entitiesBinding);
        }
        private void Entity_MouseDown(object sender, MouseButtonEventArgs e) //our move is starting by left-clicking the mouse on the entity and keep dragging
        {
            if (sender is FrameworkElement element && element.DataContext is Entity entity)
            {
                _isDragging = true;
                _startPoint = e.GetPosition(EditorCanvas);
                _draggedData = entity;

                // Capture the mouse to ensure we get mouse events even if the cursor leaves the entity.
                Mouse.Capture(element);
            }
        }

        private void Entity_MouseMove(object sender, MouseEventArgs e) //it represents the move by keep clicking the left button of the mouse and dragging the entity. X and Y of the object is changing
        {//start point and the current position is being used to define the most up-to-date (changing while the move is happening of course) location (it is calculated by adding the difference to the X and Y initial coordinates)
            if (_isDragging && _draggedData is Entity entity)
            {
                Point currentPosition = e.GetPosition(EditorCanvas);
                double offsetX = currentPosition.X - _startPoint.X;
                double offsetY = currentPosition.Y - _startPoint.Y;

                // Update entity position
                entity.X += (int)offsetY;
                entity.Y += (int)offsetX;

                _startPoint = currentPosition;

                // Notify the UI of the position change
                EditorCanvas.Items.Refresh();
            }
        }

        private void Entity_MouseUp(object sender, MouseButtonEventArgs e) //It represents the state of stop dragging by taking our hand from the left-click
        {
            if (_isDragging)
            {
                _isDragging = false;
                _draggedData = null;

                // Release the mouse capture
                Mouse.Capture(null);
            }
        }
    
    private void AddEntity_Click(object sender, RoutedEventArgs e)
        {
            var popup = new AddEntityDialog();
            popup.ShowDialog();
            if (!string.IsNullOrEmpty(popup.EntityName))
            {
                var randomNrGenerator = new Random();
                EntityStore.Add(popup.EntityName, randomNrGenerator.Next((int)EditorCanvas.ActualWidth - 80), randomNrGenerator.Next((int)EditorCanvas.ActualHeight - 50));
            }
        }
    }
}
