using DevExpress.Xpf.Core;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace How_to_Drag_and_Drop_Between_GridControl_and_Other_Controls {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            gridControl.ItemsSource = Staff.GetStaff();
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