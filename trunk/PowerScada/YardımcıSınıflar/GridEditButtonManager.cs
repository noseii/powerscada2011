using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;

namespace PowerScada
{
    public delegate bool BeforeExecuteEventHandler(object sender, EventArgs e);

  

    

    public class GridEditButtonManager
    {
        
        protected DevExpress.XtraGrid.GridControl ManagedGrid;
        public IGridButtonAction ButtonAction;
        public string[] FieldName;
        public string[] FieldNames { get { return FieldName; } }
        public DevExpress.XtraGrid.GridControl ManagerGrid { get { return ManagedGrid; } }
        public string[] SourceFieldName;

       
        protected bool isButtonPressed = false;
        protected string CurrentColKey;
        protected string CurrentColSubKey;

        protected string[] LinkFieldSource;
        protected string[] LinkFieldDestination;

        private bool restoreOldValue = true;
        public bool RestoreOldValue
        {
            get
            {
                return restoreOldValue;
            }
            set
            {
                restoreOldValue = value;
            }
        }
        private object OldCellValue;

        public event EventHandler AfterExecute;
        public void OnAfterExecute()
        {
            if (AfterExecute != null)
                AfterExecute(this, EventArgs.Empty);
        }


        public event BeforeExecuteEventHandler BeforeExecute;
        public bool OnBeforeExecute()
        {
            if (BeforeExecute != null)
                return BeforeExecute(this, EventArgs.Empty);
            return true;
        }

        public GridEditButtonManager() { }

        public GridEditButtonManager(DevExpress.XtraGrid.GridControl grid, IGridButtonAction buttonAction, string[] fieldName, string[] sourceFieldName)
        {
            initialize(grid, buttonAction, fieldName, sourceFieldName, false);
        }
        public GridEditButtonManager(DevExpress.XtraGrid.GridControl grid, IGridButtonAction buttonAction, string[] fieldName, string[] sourceFieldName, bool isValidationEnabled)
        {
            initialize(grid, buttonAction, fieldName, sourceFieldName, isValidationEnabled);
        }
        public void Click()
        {


            //GetGridview().ce
            ClickedCell(GetGridview().GetFocusedValue());
        }
        public GridView GetGridview()
        {
            GridView gridview = (GridView)ManagedGrid.Views[0];
            return gridview;
        }


        protected void initialize(DevExpress.XtraGrid.GridControl grid, IGridButtonAction buttonAction, string[] fieldName, string[] sourceFieldName, bool isValidationEnabled)
        {
            this.ManagedGrid = grid;
            this.ButtonAction = buttonAction;
            this.FieldName = fieldName;
            this.SourceFieldName = sourceFieldName;
            //
            this.ButtonAction.ParentManager = this;

            
            //
            //if (FieldName.Length != SourceFieldName.Length)
            //{
            //    throw new Exception("Dizi boyutları farklı!");
            //}
            if (FieldName.Length > SourceFieldName.Length)
            {
                throw new Exception("İstenen bilgi boyutu, kaynak boyutundan büyük olamaz!");
            }

            foreach (string name in FieldName)
            {
                if (GetGridview().Columns.ColumnByFieldName(name) != null && GetGridview().Columns.ColumnByFieldName(name).ColumnEdit!=null)
                { 
                    object nesne=this.GetGridview().Columns.ColumnByFieldName(name).ColumnEdit;
                    DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit button = (nesne as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit);
                    if(button!=null)
                        button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(ManagedGrid_ClickCellButton);
                   
                }
            }


            //GetGridview().RowCellClick += new RowCellClickEventHandler(ManagedGrid_ClickCellButton);

            //this.ManagedGrid.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ManagedGrid_ClickCellButton);
            //this.ManagedGrid.BeforeCellUpdate += new Infragistics.Win.UltraWinGrid.BeforeCellUpdateEventHandler(ManagedGrid_BeforeCellUpdate);
            if (isValidationEnabled)
            {
                //this.ManagedGrid.BeforeCellDeactivate += new System.ComponentModel.CancelEventHandler(ManagedGrid_BeforeCellDeactivate);
                //GetGridview().AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ManagedGrid_AfterCellUpdate);
            }
            this.ManagedGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(ManagedGrid_KeyUp);
            //ManagedGrid.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;

            //GetGridview().BeforeCellUpdate
            //this.ManagedGrid.BeforeCellUpdate += new Infragistics.Win.UltraWinGrid.BeforeCellUpdateEventHandler(ManagedGrid_BeforeCellUpdate);
            //if (isValidationEnabled)
            //{
            //    this.ManagedGrid.BeforeCellDeactivate += new System.ComponentModel.CancelEventHandler(ManagedGrid_BeforeCellDeactivate);
            //    this.ManagedGrid.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ManagedGrid_AfterCellUpdate);
            //}
            //this.ManagedGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(ManagedGrid_KeyUp);
            //ManagedGrid.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;

        }

       

       

       

        void ManagedGrid_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F11)
                ClickedCell(GetGridview().GetFocusedValue());
        }

        public void AddLink(string source, string destination)
        {
            int newLength = 0;
            if (LinkFieldSource != null) newLength = LinkFieldSource.Length;
            newLength++; // yeni eklenecek eleman için yer açıyoruz.

            string[] tempStr;
            //-------------------------------------------------------------
            tempStr = new string[newLength];
            if (LinkFieldSource != null) LinkFieldSource.CopyTo(tempStr, 0);
            LinkFieldSource = tempStr;
            //
            tempStr = new string[newLength];
            if (LinkFieldDestination != null) LinkFieldDestination.CopyTo(tempStr, 0);
            LinkFieldDestination = tempStr;
            //-------------------------------------------------------------

            LinkFieldSource[newLength - 1] = source;
            LinkFieldDestination[newLength - 1] = destination;
        }

        private void UpdateLinkData( DataTable dt)
        {
            if (LinkFieldSource != null)
            {
                if (LinkFieldSource.Length > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < LinkFieldSource.Length; i++)
                        {
                            object o = dt.Rows[0][LinkFieldSource[i]];
                            GetGridview().SetRowCellValue(GetGridview().FocusedRowHandle, LinkFieldDestination[i], o); 
                        }
                    }
                    else
                    {
                        // Hata!? 
                    }
                }
            }
        }
        private void UpdateData(DataTable dt)
        {
            
            //ManagedGrid.EventManager.AllEventsEnabled = false;
            if (dt == null)
            {
                if (FieldName.Length > 0)
                {
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        try
                        {   
                          
                            //GetGridview().SetRowCellValue(GetGridview().FocusedRowHandle, FieldName[i], System.DBNull.Value);
                            GetGridview().GetFocusedDataRow()[FieldName[i]] = System.DBNull.Value;
                        }
                        catch (Exception)
                        { }

                    }
                }
                //return;
            }
            else if (FieldName.Length > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        object o = dt.Rows[0][SourceFieldName[i]];
                        if (o != DBNull.Value)
                            //GetGridview().SetRowCellValue(GetGridview().FocusedRowHandle, FieldName[i], o);
                            GetGridview().GetFocusedDataRow()[FieldName[i]] = o;
                       
                    }
                }
                else
                {
                    // Hata!? 
                }
            }
            //ManagedGrid.EventManager.AllEventsEnabled = true;
        }
        protected bool isManaged(string key)
        {
            bool res = false;
            foreach (string s in FieldName)
            {
                if (key == s)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        protected void ExtractColumnKeys(string key)
        {//, ref string colKey, ref string colSubKey){
            CurrentColKey = key;
            CurrentColSubKey = "";

            for (int i = 0; i < FieldNames.Length; i++)
            {
                if (FieldNames[i] == key)
                {
                    CurrentColSubKey = SourceFieldName[i];
                    break;
                }
            }

         
        }


        public void cmdRestoreOldValue()
        {
            if (RestoreOldValue)
                GetGridview().SetFocusedValue(OldCellValue);

            
        }

        private void ClickedCell(object cell)
        {
            //if (Cell.Column.CellActivation == Infragistics.Win.UltraWinGrid.Activation.Disabled || Cell.Column.CellActivation == Infragistics.Win.UltraWinGrid.Activation.NoEdit || Cell.Column.CellActivation == Infragistics.Win.UltraWinGrid.Activation.ActivateOnly) return;
            //if (Cell.Activation == Infragistics.Win.UltraWinGrid.Activation.Disabled || Cell.Activation == Infragistics.Win.UltraWinGrid.Activation.NoEdit || Cell.Activation == Infragistics.Win.UltraWinGrid.Activation.ActivateOnly) return;
            //RowCellClickEventArgs Cell = (RowCellClickEventArgs)cell;

            if (GetGridview().IsNewItemRow(GetGridview().FocusedRowHandle))
            {
                GetGridview().AddNewRow();
                GetGridview().UpdateCurrentRow();
            }

            object o = GetGridview().GetFocusedValue();
            int i = GetGridview().FocusedRowHandle;
            DevExpress.XtraGrid.Columns.GridColumn column = GetGridview().FocusedColumn;

            

            if (!isManaged(column.Name)) return; //***
            //
            isButtonPressed = true; //***
            
            //Cell.Row.Update(); //[07 Ocak 2005] YD

            //***
            bool oldB = restoreOldValue;
            restoreOldValue = false;
            try
            {
                 
                ManageButtonAction(false, true);
            }
            finally
            {
                restoreOldValue = oldB;
            }
            //***

          
            isButtonPressed = false; //***
        }

        //private RowCellClickEventArgs GetCell(GridViewCellEventArgs e)
        //{
        //    return (RowCellClickEventArgs)GetGridview().GetFocusedValue();
        //}

        private void ManagedGrid_ClickCellButton(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {  
            ClickedCell(e);
        }

        //private string GetKey(DataGridViewCellEventArgs e)
        //{
        //    return ManagedGrid.Columns[GetCell(e).ColumnIndex].Name;
        //}

        //private void ManagedGrid_AfterCellUpdate(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (!isManaged(GetKey(e))) return; //***	
        //    if (isButtonPressed) return; //***

        //    //------------------------------------
        //    isButtonPressed = true; //***
        //    //
        //    ManageButtonAction(GetCell(e), true, false);
        //    //
        //    isButtonPressed = false; //***
        //    //-------------------------------------
        //}

        private void ManagedGrid_BeforeCellDeactivate(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }



        private bool ManageButtonAction( bool returnIfFound, bool executeAction)
        {

            object o = GetGridview().GetFocusedValue();
            int i = GetGridview().FocusedRowHandle;
            DevExpress.XtraGrid.Columns.GridColumn column = GetGridview().FocusedColumn;
            
            bool res = false;
            string searchField, searchValue;
            //if (cell.Row.IsFilterRow) return true;

            if (OnBeforeExecute() == false)
                return false;

            ExtractColumnKeys(column.Name);

            searchField = CurrentColSubKey;
            searchValue = GetGridview().GetFocusedValue() == null ? string.Empty : GetGridview().GetFocusedValue().ToString();

            if (searchValue == "")
            {
                UpdateData(null);
                //!? UpdateLinkData(cell, null);

                //!? ManagedGrid.ActiveCell.Tag = ManagedGrid.ActiveCell.Value;
                ManagedGrid.Update();
                if (executeAction == false) return true;
            }


               DataRowView rowView = (DataRowView)GetGridview().GetFocusedRow();
               ButtonAction.ActiveRow = rowView;
            DataTable dt = ButtonAction.ExecuteDataTable(searchField, searchValue, returnIfFound);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    res = true;
                    UpdateData(dt);
                    UpdateLinkData(dt);
                    GetGridview().UpdateCurrentRow();
                    //ManagedGrid.CurrentCell.Tag = ManagedGrid.CurrentCell.Value;
                    //ManagedGrid.UpdateCellValue(cell.ColumnIndex, cell.RowIndex);
                   
                }
                else
                {
                    //					ManagedGrid.ActiveCell.Activate();
                    cmdRestoreOldValue();
                }
            }
            else
            {
                //				ManagedGrid.ActiveCell.Activate();
                cmdRestoreOldValue();
            }

            if (res) OnAfterExecute(); //***

            return res;
        }

        //private void ManagedGrid_CellValidating(object sender, CancelEventArgs e)
        //{
             
        //    OldCellValue = GetCell(e).Value;
        //}
    }
}
