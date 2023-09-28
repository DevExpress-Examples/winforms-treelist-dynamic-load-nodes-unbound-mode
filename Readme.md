<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128637760/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T500394)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms TreeList - Dynamic node loading in unbound mode

This example displays your local file-folder structure in the [WinForms TreeList](https://www.devexpress.com/products/net/controls/winforms/tree_list/) control. The example implements dynamic node loading in [unbound mode](https://docs.devexpress.com/WindowsForms/5557/controls-and-libraries/tree-list/feature-center/data-binding/unbound-mode):

```csharp
private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e) {
    if (e.Node.Tag != null) {
        Cursor currentCursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;
        InitFolders(e.Node.GetDisplayText("FullName"), e.Node);
        e.Node.Tag = null;
        Cursor.Current = currentCursor;
    }
}
```


## Files to Review

* [Form1.cs](./CS/TreeList-UnboundMode-ViaBeforeExpandEvent/Form1.cs) (VB: [Form1.vb](./VB/TreeList-UnboundMode-ViaBeforeExpandEvent/Form1.vb))


## Documentation

* [Unbound Mode](https://docs.devexpress.com/WindowsForms/5557/controls-and-libraries/tree-list/feature-center/data-binding/unbound-mode)
* [Virtual Mode - Binding to a Hierarchical Business Object (Data Source Level)](https://docs.devexpress.com/WindowsForms/2486/controls-and-libraries/tree-list/feature-center/data-binding/virtual-mode-binding-to-a-hierarchical-business-object-data-source-level)
* [Virtual Mode (Dynamic Data Loading) Using Events (Tree List Level)](https://docs.devexpress.com/WindowsForms/5560/controls-and-libraries/tree-list/feature-center/data-binding/virtual-mode-dynamic-data-loading-using-events-tree-list-level)
