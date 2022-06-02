Imports DevExpress.Xpf.Core
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media

Namespace How_to_Drag_and_Drop_Between_GridControl_and_Other_Controls

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.gridControl.ItemsSource = Staff.GetStaff()
        End Sub

        Private Overloads Sub OnDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
            If e.Data.GetDataPresent(GetType(RecordDragDropData)) Then
                Dim textBlock As TextBlock = CType(sender, TextBlock)
                textBlock.Background = Brushes.SkyBlue
                e.Handled = True
            End If
        End Sub

        Private Overloads Sub OnDragLeave(ByVal sender As Object, ByVal e As DragEventArgs)
            Dim textBlock As TextBlock = CType(sender, TextBlock)
            textBlock.Background = Nothing
            textBlock.DataContext = Nothing
            e.Handled = True
        End Sub

        Private Overloads Sub OnDrop(ByVal sender As Object, ByVal e As DragEventArgs)
            If e.Data.GetDataPresent(GetType(RecordDragDropData)) Then
                Dim textBlock As TextBlock = CType(sender, TextBlock)
                Dim data As Object = e.Data.GetData(GetType(RecordDragDropData))
                Dim employees As Object() = CType(data, RecordDragDropData).Records
                textBlock.DataContext = CType(CType(employees, IList)(0), Employee)
            End If
        End Sub

        Private Overloads Sub OnPreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim textBlock As TextBlock = CType(sender, TextBlock)
            If textBlock.DataContext IsNot Nothing Then
                DragDrop.DoDragDrop(textBlock, textBlock.DataContext, DragDropEffects.Move)
            End If
        End Sub

        Private Sub OnDragRecordOver(ByVal sender As Object, ByVal e As DragRecordOverEventArgs)
            If e.IsFromOutside AndAlso e.Data.GetDataPresent(GetType(Employee)) Then
                e.Effects = DragDropEffects.Move
                e.Handled = True
            End If
        End Sub

        Private Sub OnDropRecord(ByVal sender As Object, ByVal e As DropRecordEventArgs)
            If e.IsFromOutside AndAlso e.Data.GetDataPresent(GetType(Employee)) Then
                Dim data As Object = e.Data.GetData(GetType(Employee))
                Dim employee As Employee = CType(data, Employee)
                e.Data.SetData(New RecordDragDropData(New List(Of Employee)() From {employee}.ToArray()))
            End If
        End Sub
    End Class
End Namespace
