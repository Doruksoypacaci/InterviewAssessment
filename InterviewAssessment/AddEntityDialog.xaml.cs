using System.Collections.Generic;
using System.Windows;

namespace DomainModelEditor
{
    /// <summary>
    /// Interaction logic for NamePrompt.xaml
    /// </summary>
    public partial class AddEntityDialog : Window
    {
        public string EntityName { get; set; }
        public Dictionary<string, object> Attributes { get; private set; }

        public AddEntityDialog()
        {
            InitializeComponent();
            Attributes = new Dictionary<string, object>();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EntityName = Name.Text;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
