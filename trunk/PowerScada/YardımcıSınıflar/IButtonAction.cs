using System;
using DevExpress.XtraGrid;
using System.Data;

namespace PowerScada
{
    public interface IEditButtonAction
    {
        string PrimaryKeyName { get; }
        object ParentManager { get; set; }
        bool MultiSelect { get; set; }
        System.Data.DataTable ExecuteDataTable(string searchField, object searchValue, bool returnOnMatch);
    }

    public interface IGridButtonAction : IEditButtonAction
    {
        DataRowView ActiveRow { set; }
        
    }

}
