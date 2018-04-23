using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeList_UnboundMode_ViaBeforeExpandEvent {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            InitTreeList();
            InitData();
        }

        private void InitTreeList() {
            TreeListColumn treeListColumn1 = new TreeListColumn();
            TreeListColumn treeListColumn2 = new TreeListColumn();
            TreeListColumn treeListColumn3 = new TreeListColumn();
            TreeListColumn treeListColumn4 = new TreeListColumn();

            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "FullName";
            treeListColumn1.FieldName = "FullName";
            // 
            // treeListColumn2
            // 
            treeListColumn2.Caption = "Name";
            treeListColumn2.FieldName = "Name";
            treeListColumn2.MinWidth = 27;
            treeListColumn2.VisibleIndex = 0;
            treeListColumn2.Width = 274;
            // 
            // treeListColumn3
            // 
            treeListColumn3.Caption = "Type";
            treeListColumn3.FieldName = "Type";
            treeListColumn3.VisibleIndex = 1;
            treeListColumn3.Width = 112;
            // 
            // treeListColumn4
            // 
            treeListColumn4.AppearanceCell.Options.UseTextOptions = true;
            treeListColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            treeListColumn4.Caption = "Size(Bytes)";
            treeListColumn4.FieldName = "Size";
            treeListColumn4.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            treeListColumn4.Format.FormatString = "n0";
            treeListColumn4.VisibleIndex = 2;
            treeListColumn4.Width = 123;


            treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            treeListColumn1,
            treeListColumn2,
            treeListColumn3,
            treeListColumn4});

            treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            treeList1.OptionsBehavior.AutoChangeParent = false;
            treeList1.OptionsBehavior.AutoNodeHeight = false;
            treeList1.OptionsBehavior.CloseEditorOnLostFocus = false;
            treeList1.OptionsBehavior.Editable = false;
            treeList1.OptionsSelection.KeepSelectedOnClick = false;
            treeList1.OptionsBehavior.ShowToolTips = false;
            treeList1.OptionsBehavior.SmartMouseHover = false;

            treeList1.StateImageList = this.imageList1;
            
            treeList1.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCollapse);
            treeList1.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterExpand);
            treeList1.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.treeList1_BeforeExpand);
        }

        private void InitData() {
            InitFolders(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), null);
        }

        private void InitFolders(string path, TreeListNode pNode) {
            treeList1.BeginUnboundLoad();
            TreeListNode node;
            DirectoryInfo di;
            try {
                string[] root = Directory.GetDirectories(path);
                foreach (string s in root) {
                    try {
                        di = new DirectoryInfo(s);
                        node = treeList1.AppendNode(new object[] { s, di.Name, "Folder", null }, pNode);
                        node.StateImageIndex = 0;
                        node.HasChildren = HasFiles(s);
                        if (node.HasChildren)
                            node.Tag = true;
                    }
                    catch { }
                }
            }
            catch { }
            InitFiles(path, pNode);
            treeList1.EndUnboundLoad();
        }

        private void InitFiles(string path, TreeListNode pNode) {
            TreeListNode node;
            FileInfo fi;
            try {
                string[] root = Directory.GetFiles(path);
                foreach (string s in root) {
                    fi = new FileInfo(s);
                    node = treeList1.AppendNode(new object[] { s, fi.Name, "File", fi.Length }, pNode);
                    node.StateImageIndex = 1;
                    node.HasChildren = false;
                }
            }
            catch { }
        }

        private bool HasFiles(string path) {
            string[] root = Directory.GetFiles(path);
            if (root.Length > 0) return true;
            root = Directory.GetDirectories(path);
            if (root.Length > 0) return true;
            return false;
        }

        private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e) {
            if (e.Node.Tag != null) {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                InitFolders(e.Node.GetDisplayText("FullName"), e.Node);
                e.Node.Tag = null;
                Cursor.Current = currentCursor;
            }
        }

        private void treeList1_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            if (e.Node.StateImageIndex != 1) e.Node.StateImageIndex = 2;
        }

        private void treeList1_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            if (e.Node.StateImageIndex != 1) e.Node.StateImageIndex = 0;
        }
    }

}
