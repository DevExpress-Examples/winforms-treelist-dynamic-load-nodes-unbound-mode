Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Namespace TreeList_UnboundMode_ViaBeforeExpandEvent

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            InitTreeList()
            InitData()
        End Sub

        Private Sub InitTreeList()
            Dim treeListColumn1 As TreeListColumn = New TreeListColumn()
            Dim treeListColumn2 As TreeListColumn = New TreeListColumn()
            Dim treeListColumn3 As TreeListColumn = New TreeListColumn()
            Dim treeListColumn4 As TreeListColumn = New TreeListColumn()
            ' 
            ' treeListColumn1
            ' 
            treeListColumn1.Caption = "FullName"
            treeListColumn1.FieldName = "FullName"
            ' 
            ' treeListColumn2
            ' 
            treeListColumn2.Caption = "Name"
            treeListColumn2.FieldName = "Name"
            treeListColumn2.MinWidth = 27
            treeListColumn2.VisibleIndex = 0
            treeListColumn2.Width = 274
            ' 
            ' treeListColumn3
            ' 
            treeListColumn3.Caption = "Type"
            treeListColumn3.FieldName = "Type"
            treeListColumn3.VisibleIndex = 1
            treeListColumn3.Width = 112
            ' 
            ' treeListColumn4
            ' 
            treeListColumn4.AppearanceCell.Options.UseTextOptions = True
            treeListColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            treeListColumn4.Caption = "Size(Bytes)"
            treeListColumn4.FieldName = "Size"
            treeListColumn4.Format.FormatType = DevExpress.Utils.FormatType.Numeric
            treeListColumn4.Format.FormatString = "n0"
            treeListColumn4.VisibleIndex = 2
            treeListColumn4.Width = 123
            treeList1.Columns.AddRange(New TreeListColumn() {treeListColumn1, treeListColumn2, treeListColumn3, treeListColumn4})
            treeList1.Dock = DockStyle.Fill
            treeList1.OptionsBehavior.AutoChangeParent = False
            treeList1.OptionsBehavior.AutoNodeHeight = False
            treeList1.OptionsBehavior.CloseEditorOnLostFocus = False
            treeList1.OptionsBehavior.Editable = False
            treeList1.OptionsSelection.KeepSelectedOnClick = False
            treeList1.OptionsBehavior.ShowToolTips = False
            treeList1.OptionsBehavior.SmartMouseHover = False
            treeList1.StateImageList = imageList1
            AddHandler treeList1.AfterCollapse, New DevExpress.XtraTreeList.NodeEventHandler(AddressOf treeList1_AfterCollapse)
            AddHandler treeList1.AfterExpand, New DevExpress.XtraTreeList.NodeEventHandler(AddressOf treeList1_AfterExpand)
            AddHandler treeList1.BeforeExpand, New DevExpress.XtraTreeList.BeforeExpandEventHandler(AddressOf treeList1_BeforeExpand)
        End Sub

        Private Sub InitData()
            InitFolders(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), Nothing)
        End Sub

        Private Sub InitFolders(ByVal path As String, ByVal pNode As TreeListNode)
            treeList1.BeginUnboundLoad()
            Dim node As TreeListNode
            Dim di As DirectoryInfo
            Try
                Dim root As String() = Directory.GetDirectories(path)
                For Each s As String In root
                    Try
                        di = New DirectoryInfo(s)
                        node = treeList1.AppendNode(New Object() {s, di.Name, "Folder", Nothing}, pNode)
                        node.StateImageIndex = 0
                        node.HasChildren = HasFiles(s)
                        If node.HasChildren Then node.Tag = True
                    Catch
                    End Try
                Next
            Catch
            End Try

            InitFiles(path, pNode)
            treeList1.EndUnboundLoad()
        End Sub

        Private Sub InitFiles(ByVal path As String, ByVal pNode As TreeListNode)
            Dim node As TreeListNode
            Dim fi As FileInfo
            Try
                Dim root As String() = Directory.GetFiles(path)
                For Each s As String In root
                    fi = New FileInfo(s)
                    node = treeList1.AppendNode(New Object() {s, fi.Name, "File", fi.Length}, pNode)
                    node.StateImageIndex = 1
                    node.HasChildren = False
                Next
            Catch
            End Try
        End Sub

        Private Function HasFiles(ByVal path As String) As Boolean
            Dim root As String() = Directory.GetFiles(path)
            If root.Length > 0 Then Return True
            root = Directory.GetDirectories(path)
            If root.Length > 0 Then Return True
            Return False
        End Function

        Private Sub treeList1_BeforeExpand(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.BeforeExpandEventArgs)
            If e.Node.Tag IsNot Nothing Then
                Dim currentCursor As Cursor = Cursor.Current
                Cursor.Current = Cursors.WaitCursor
                InitFolders(e.Node.GetDisplayText("FullName"), e.Node)
                e.Node.Tag = Nothing
                Cursor.Current = currentCursor
            End If
        End Sub

        Private Sub treeList1_AfterExpand(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs)
            If e.Node.StateImageIndex <> 1 Then e.Node.StateImageIndex = 2
        End Sub

        Private Sub treeList1_AfterCollapse(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs)
            If e.Node.StateImageIndex <> 1 Then e.Node.StateImageIndex = 0
        End Sub
    End Class
End Namespace
