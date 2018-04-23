using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace How_to_Drag_and_Drop_Between_GridControl_and_Other_Controls {
    public partial class MainWindow : Window {
        public class Employee {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Department { get; set; }
            public override string ToString() {
                return Name;
            }
        }

        public static class Stuff {
            public static ObservableCollection<Employee> GetStuff() {
                ObservableCollection<Employee> stuff = new ObservableCollection<Employee>();
                stuff.Add(new Employee() { ID = 1, Name = "Gregory S. Price", Department = "", Position = "President" });
                stuff.Add(new Employee() { ID = 2, Name = "Irma R. Marshall", Department = "Marketing", Position = "Vice President" });
                stuff.Add(new Employee() { ID = 3, Name = "John C. Powell", Department = "Operations", Position = "Vice President" });
                stuff.Add(new Employee() { ID = 4, Name = "Christian P. Laclair", Department = "Production", Position = "Vice President" });
                stuff.Add(new Employee() { ID = 5, Name = "Karen J. Kelly", Department = "Finance", Position = "Vice President" });

                stuff.Add(new Employee() { ID = 6, Name = "Brian C. Cowling", Department = "Marketing", Position = "Manager" });
                stuff.Add(new Employee() { ID = 7, Name = "Thomas C. Dawson", Department = "Marketing", Position = "Manager" });
                stuff.Add(new Employee() { ID = 8, Name = "Angel M. Wilson", Department = "Marketing", Position = "Manager" });
                stuff.Add(new Employee() { ID = 9, Name = "Bryan R. Henderson", Department = "Marketing", Position = "Manager" });

                stuff.Add(new Employee() { ID = 10, Name = "Harold S. Brandes", Department = "Operations", Position = "Manager" });
                stuff.Add(new Employee() { ID = 11, Name = "Michael S. Blevins", Department = "Operations", Position = "Manager" });
                stuff.Add(new Employee() { ID = 12, Name = "Jan K. Sisk", Department = "Operations", Position = "Manager" });
                stuff.Add(new Employee() { ID = 13, Name = "Sidney L. Holder", Department = "Operations", Position = "Manager" });

                stuff.Add(new Employee() { ID = 14, Name = "James L. Kelsey", Department = "Production", Position = "Manager" });
                stuff.Add(new Employee() { ID = 15, Name = "Howard M. Carpenter", Department = "Production", Position = "Manager" });
                stuff.Add(new Employee() { ID = 16, Name = "Jennifer T. Tapia", Department = "Production", Position = "Manager" });

                stuff.Add(new Employee() { ID = 17, Name = "Judith P. Underhill", Department = "Finance", Position = "Manager" });
                stuff.Add(new Employee() { ID = 18, Name = "Russell E. Belton", Department = "Finance", Position = "Manager" });
                return stuff;
            }
        } 

        public MainWindow() {
            InitializeComponent();
            gridControl.ItemsSource = Stuff.GetStuff();
        }                      

        void OnDragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(RecordDragDropData))) {
                TextBlock textBlock = (TextBlock)sender;                
                textBlock.Background = Brushes.SkyBlue;
                e.Handled = true;
            }
        }

        void OnDragLeave(object sender, DragEventArgs e) {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.Background = null;
            textBlock.DataContext = null;
            e.Handled = true;
        }

        void OnDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(RecordDragDropData))) {
                TextBlock textBlock = (TextBlock)sender;
                object data = e.Data.GetData(typeof(RecordDragDropData));
                object[] employees = ((RecordDragDropData)data).Records;
                textBlock.DataContext = (Employee)((IList)employees)[0];
            }
        }

        void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock.DataContext != null) {
                DragDrop.DoDragDrop(textBlock, textBlock.DataContext, DragDropEffects.Move);
            }
        }

        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            if (e.IsFromOutside && e.Data.GetDataPresent(typeof(Employee))) {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }

        void OnDropRecord(object sender, DropRecordEventArgs e) {
            if (e.IsFromOutside && e.Data.GetDataPresent(typeof(Employee))) {
                object data = e.Data.GetData(typeof(Employee));
                Employee employee = (Employee)data;
                e.Data.SetData(new RecordDragDropData(new List<Employee>() { employee }.ToArray()));
            }
        }
    }
}