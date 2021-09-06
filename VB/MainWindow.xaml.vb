Imports System.Windows
Imports System.Collections.ObjectModel
Imports System.Windows.Controls
Imports System.Collections.Generic
Imports System.Windows.Input
Imports System.Collections
Imports System.Windows.Media
Imports DevExpress.Xpf.Core

Namespace How_to_Drag_and_Drop_Between_GridControl_and_Other_Controls
	Partial Public Class MainWindow
		Inherits Window

		Public Class Employee
			Public Property ID() As Integer
			Public Property Name() As String
			Public Property Position() As String
			Public Property Department() As String
			Public Overrides Function ToString() As String
				Return Name
			End Function
		End Class

		Public NotInheritable Class Stuff

			Private Sub New()
			End Sub

			Public Shared Function GetStuff() As ObservableCollection(Of Employee)
'INSTANT VB NOTE: The variable stuff was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
				Dim stuff_Conflict As New ObservableCollection(Of Employee)()
				stuff_Conflict.Add(New Employee() With {
					.ID = 1,
					.Name = "Gregory S. Price",
					.Department = "",
					.Position = "President"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 2,
					.Name = "Irma R. Marshall",
					.Department = "Marketing",
					.Position = "Vice President"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 3,
					.Name = "John C. Powell",
					.Department = "Operations",
					.Position = "Vice President"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 4,
					.Name = "Christian P. Laclair",
					.Department = "Production",
					.Position = "Vice President"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 5,
					.Name = "Karen J. Kelly",
					.Department = "Finance",
					.Position = "Vice President"
				})

				stuff_Conflict.Add(New Employee() With {
					.ID = 6,
					.Name = "Brian C. Cowling",
					.Department = "Marketing",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 7,
					.Name = "Thomas C. Dawson",
					.Department = "Marketing",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 8,
					.Name = "Angel M. Wilson",
					.Department = "Marketing",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 9,
					.Name = "Bryan R. Henderson",
					.Department = "Marketing",
					.Position = "Manager"
				})

				stuff_Conflict.Add(New Employee() With {
					.ID = 10,
					.Name = "Harold S. Brandes",
					.Department = "Operations",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 11,
					.Name = "Michael S. Blevins",
					.Department = "Operations",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 12,
					.Name = "Jan K. Sisk",
					.Department = "Operations",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 13,
					.Name = "Sidney L. Holder",
					.Department = "Operations",
					.Position = "Manager"
				})

				stuff_Conflict.Add(New Employee() With {
					.ID = 14,
					.Name = "James L. Kelsey",
					.Department = "Production",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 15,
					.Name = "Howard M. Carpenter",
					.Department = "Production",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 16,
					.Name = "Jennifer T. Tapia",
					.Department = "Production",
					.Position = "Manager"
				})

				stuff_Conflict.Add(New Employee() With {
					.ID = 17,
					.Name = "Judith P. Underhill",
					.Department = "Finance",
					.Position = "Manager"
				})
				stuff_Conflict.Add(New Employee() With {
					.ID = 18,
					.Name = "Russell E. Belton",
					.Department = "Finance",
					.Position = "Manager"
				})
				Return stuff_Conflict
			End Function
		End Class

		Public Sub New()
			InitializeComponent()
			gridControl.ItemsSource = Stuff.GetStuff()
		End Sub

		Private Overloads Sub OnDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
			If e.Data.GetDataPresent(GetType(RecordDragDropData)) Then
				Dim textBlock As TextBlock = DirectCast(sender, TextBlock)
				textBlock.Background = Brushes.SkyBlue
				e.Handled = True
			End If
		End Sub

		Private Overloads Sub OnDragLeave(ByVal sender As Object, ByVal e As DragEventArgs)
			Dim textBlock As TextBlock = DirectCast(sender, TextBlock)
			textBlock.Background = Nothing
			textBlock.DataContext = Nothing
			e.Handled = True
		End Sub

		Private Overloads Sub OnDrop(ByVal sender As Object, ByVal e As DragEventArgs)
			If e.Data.GetDataPresent(GetType(RecordDragDropData)) Then
				Dim textBlock As TextBlock = DirectCast(sender, TextBlock)
				Dim data As Object = e.Data.GetData(GetType(RecordDragDropData))
				Dim employees() As Object = DirectCast(data, RecordDragDropData).Records
				textBlock.DataContext = CType(DirectCast(employees, IList)(0), Employee)
			End If
		End Sub

		Private Overloads Sub OnPreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			Dim textBlock As TextBlock = DirectCast(sender, TextBlock)
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
				Dim employee As Employee = DirectCast(data, Employee)
				e.Data.SetData(New RecordDragDropData(New List(Of Employee)() From {employee}.ToArray()))
			End If
		End Sub
	End Class
End Namespace