using System;
using PowerScada;
using System.Collections.Generic;
using mymodel;
using System.Data;

namespace PowerScada
{
    public abstract class ActionBase : IEditButtonAction, IGridButtonAction
    {
        private ISelectionForm selectionForm;

        public abstract ISelectionForm SelectionForm
        {
            get;
        }

        protected bool editable;
        protected bool multiselect;
        public bool MultiSelect
        {
            get { return multiselect; }
            set { multiselect = value; }
        }
        protected bool returnOnMatch = false;
        protected string searchField;
        protected object searchValue; //Veysel
        protected SelectionFormReturnType returnType;

        public ActionBase()
        {
            editable = true;
            multiselect = false;
            //returnOnMatch = true; VA
            returnType = SelectionFormReturnType.AsIs;
        }

        public ActionBase(bool editable, bool multiselect)
        {
            this.editable = editable;
            this.multiselect = multiselect;
            returnType = SelectionFormReturnType.AsIs;
        }

        //		public ActionBase(bool editable, bool multiselect, bool returnOnMatch){
        //			this.editable = editable;
        //			this.multiselect = multiselect;
        //			this.returnOnMatch = returnOnMatch;
        //		}

        private object parentManager;
        public object ParentManager
        {
            get
            {
                return parentManager;
            }
            set
            {
                parentManager = value;
            }
        }
        protected DataRowView ActiveRowValue;
        public DataRowView ActiveRow
        {
            set
            {
                ActiveRowValue = value;
            }
        }
        public abstract string PrimaryKeyName
        {
            get;
        }
        public abstract string EntityName
        {
            get;
        }

        protected System.Data.DataTable selectedData;

        //[EB] : 5 Ocak 2006
        public System.Data.DataTable SelectedData
        {
            get
            {
                System.Diagnostics.Debug.Assert((selectedData != null), "ActionBase: selectedData null olamaz.");

                return selectedData;
            }
        }
        
        protected mymodel.Entity[] selectedEntity;

        public mymodel.Entity[] SelectedEntity
        {
            get
            {
                return selectedEntity;
            }
            // BK: SelectedEntity'yi set etmek de gerekiyor
            set
            {
                selectedEntity = value;
            }
        }
        public mymodel.Entity[] ExecuteEntity()
        {
            return ExecuteEntity("", "", false);
        }

        /// <summary>
        /// entity bulamaza boş array döndürür.Null dönmez.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ExecuteEntity<T>() where T : class
        {
            mymodel.Entity[] aEntities = ExecuteEntity("", "", false);
            if (aEntities == null) return new T[0];
            T[] tEntities = new T[aEntities.Length];
            for (int i = 0; i < aEntities.Length; i++)
                tEntities[i] = aEntities[i] as T;

            return tEntities;
        }

        /// <summary>
        /// entity bulamaza boş array döndürür.Null dönmez.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ExecuteEntity<T>(string searchField, object searchValue, bool returnOnMatch) where T : class
        {
            mymodel.Entity[] aEntities = ExecuteEntity(searchField, searchValue, returnOnMatch);
            if (aEntities == null) return new T[0];
            T[] tEntities = new T[aEntities.Length];
            for (int i = 0; i < aEntities.Length; i++)
                tEntities[i] = aEntities[i] as T;

            return tEntities;
        }

        public mymodel.Entity[] ExecuteEntity(string searchField, object searchValue, bool returnOnMatch)
        {
            this.searchField = searchField;
            this.searchValue = searchValue;
            this.returnOnMatch = returnOnMatch;
            selectionForm = SelectionForm;
            if (selectionForm == null)
            {
                selectedData = null;
                return null;
            }
            BeforeExecute();//(ref searchField);

            selectedEntity = selectionForm.ShowSelectionList(this.searchField, searchValue, this.returnOnMatch, this.editable, this.multiselect, this.returnType);

            selectedData = selectionForm.SelectedData;// EntityToDataTable(selectionForm.SelectedEntity);
            selectedEntity = selectionForm.SelectedEntity; // DataTableToEntity(selectedData);

            AfterExecute();


            return selectedEntity;
        }

        public System.Data.DataTable ExecuteDataTable()
        {
            return ExecuteDataTable("", "", false);
        }

        public System.Data.DataTable ExecuteDataTable(string searchField, object searchValue, bool returnOnMatch)
        {
            ExecuteEntity(searchField, searchValue, returnOnMatch);
            return selectedData;
        }

        public virtual void AfterExecute()
        {
        }

        public virtual void BeforeExecute()
        {//ref string searchField){
        }
    }


    public enum SelectionFormReturnType
    {
        /// <summary>
        /// RowObjIdEntityName() bilgisine göre işlem yapılır.
        /// </summary>
        OriginalEntity,
        /// <summary>
        /// EntityName() bilgisine göre işlem yapılır.
        /// </summary>
        ComplexViewEntity,
        /// <summary>
        /// Önce RowObjIdEntityName() bilgisine göre işlem yapılmaya çalışılır.Hata çıkarsa EntityName() bilgisine göre işlem yapılır.
        /// </summary>
        AsIs //Olduğu gibi Ne çıkarsa bahtıma
    }
}
