using System.Windows;

namespace DomainModelEditor
{
    public partial class AddAttributeDialog : Window
    {
        // Our new variables for the attribute name and value and the home entity which will have the attribute as a new feature. They are not read-only but also able to be changed
        public string AttributeNamenew { get; set; }
        public string AttributeValuenew { get; set; }
        public Entity SelectedEntity { get; set; }

        public AddAttributeDialog()
        {
            InitializeComponent();
        }

        public void SetEntities(System.Collections.ObjectModel.ObservableCollection<Entity> entities)
        {
            // Populate the ComboBox with the entities
            EntityComboBox.ItemsSource = entities;  // binding the list within the dialogue with the entities itself to make sure showing them dynamically even after "add entity" button is used
            //EntityComboBox.DisplayMemberPath = "Name"; // Set the displayed name of the entities //Later I made it shown via mentioning name within xaml from AddAttributeDialogue.xaml
        }
        private void SelectButton_Click(object sender, RoutedEventArgs e) // To confirm for sure an entity is selected, I decided to add a discrete button to use after an item is chosen from the list of entities
        {
            // Get the currently selected entity from the ComboBox
            SelectedEntity = (Entity)EntityComboBox.SelectedItem;

            if (SelectedEntity == null)
            {
                MessageBox.Show("Please select a valid entity.");
            }
            else
            {
                MessageBox.Show($"Entity '{SelectedEntity.Name}' selected.");
            }
        }
        private void AddAttribute_Click(object sender, RoutedEventArgs e)
        {
            // If the user hasn't selected an entity or entered an attribute, show an error
            if (SelectedEntity == null)
            {
                MessageBox.Show("Please select an entity.");
                return;
            }
            // Get values from the dialog fields
            AttributeNamenew = AttributeName.Text;
            AttributeValuenew = AttributeValue.Text;
            SelectedEntity.AddAttribute(AttributeNamenew, AttributeValuenew);
            // Close the dialog when "Add Attribute" is clicked
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog without any action
            this.DialogResult = false;
            this.Close();
        }

        private void EntityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Get the selected entity from the ComboBox
            SelectedEntity = (Entity)EntityComboBox.SelectedItem;
        }
    }
}
