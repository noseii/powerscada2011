using System;

namespace PowerScada
{
    /// <summary>
    /// Summary description for ISelectionForm.
    /// </summary>

    public interface ISelectionForm
    {
        mymodel.Entity[] ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect);
        mymodel.Entity[] ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect, SelectionFormReturnType returntype);
        System.Data.DataTable SelectedData
        {
            get;
        }
        mymodel.Entity[] SelectedEntity
        {
            get;
        }
    }

    public abstract class SelectionFormBase
    {
        protected SelectionFormReturnType returntype;
        protected bool multiselect;

        public SelectionFormBase()
        {
            returntype = SelectionFormReturnType.AsIs;
            multiselect = false;
        }

        protected mymodel.Entity[] ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect)
        {
            return this.ShowSelectionList(searchField, searchValue, returnOnMatch, editable, multiselect, SelectionFormReturnType.AsIs);
        }
        protected abstract mymodel.Entity[] ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect, SelectionFormReturnType returntype);

        protected abstract System.Data.DataTable SelectedData
        {
            get;
        }
        protected abstract mymodel.Entity[] SelectedEntity
        {
            get;
        }
    }
}
