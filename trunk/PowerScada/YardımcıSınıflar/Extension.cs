using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace PowerScada
{
    static class Extension
    {
        public static string GetGridStyle(this DataGridView grid)
        {
            string result = "";
            result += " <Style>" + Environment.NewLine;
            foreach (DataGridViewColumn column in grid.Columns)
            {
                result += string.Format(@"    <Column Name='{0}' HeaderText='{1}' Width='{2}' DisplayIndex='{3}' />" + Environment.NewLine,
                    column.Name, column.HeaderText, column.Width, column.DisplayIndex);
            }
            result += " </Style>";

            return result;
        }

        public static void SetGridStyle(this DataGridView grid, string style)
        {
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(style);

            XmlNodeList columns = doc.DocumentElement.GetElementsByTagName("Column");
            foreach (XmlNode columnNode in columns)
            {
                string name = columnNode.Attributes["Name"].InnerText;

                string type = columnNode.Attributes["Type"] != null ? columnNode.Attributes["Type"].InnerText : "";
                string text = columnNode.Attributes["Text"] != null ? columnNode.Attributes["Text"].InnerText : "";
                if (type == "CheckBox")
                    grid.Columns.Add(new DataGridViewCheckBoxColumn() { Name = name, HeaderText = name });
                else if (type == "ComboBox")
                    grid.Columns.Add(new DataGridViewComboBoxColumn() { Name = name, HeaderText = name });
                else if (type == "Button")
                    grid.Columns.Add(new DataGridViewButtonColumn() { Name = name, HeaderText = name, Text = text, UseColumnTextForButtonValue = true });
                else
                    grid.Columns.Add(name, name);
            }
            foreach (XmlNode columnNode in columns)
            {
                string name = columnNode.Attributes["Name"].InnerText;
                DataGridViewColumn column = grid.Columns[name];
                column.DataPropertyName = name;
                column.HeaderText = columnNode.Attributes["HeaderText"] != null ? columnNode.Attributes["HeaderText"].InnerText : "";
                column.Width = int.Parse(columnNode.Attributes["Width"].InnerText);
                column.DisplayIndex = int.Parse(columnNode.Attributes["DisplayIndex"].InnerText);
                column.Visible = columnNode.Attributes["Visible"] != null ? bool.Parse(columnNode.Attributes["Visible"].InnerText) : false;
            }
        }

        public static void SetGridStyle(this DevExpress.XtraGrid.GridControl grid, string style)
        {
            //grid.AutoGenerateColumns = false;
            GridView view = (GridView)grid.DefaultView;
            view.Columns.Clear();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(style);

            XmlNodeList columns = doc.DocumentElement.GetElementsByTagName("Column");
            foreach (XmlNode columnNode in columns)
            {
                string name = columnNode.Attributes["Name"].InnerText;

                string type = columnNode.Attributes["Type"] != null ? columnNode.Attributes["Type"].InnerText : "";
                string text = columnNode.Attributes["Text"] != null ? columnNode.Attributes["Text"].InnerText : "";
                if (type == "CheckBox")
                {
                    int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
                    view.Columns[i].ColumnEdit = new RepositoryItemCheckEdit();
                }
                else if (type == "ComboBox")
                {
                    int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
                    view.Columns[i].ColumnEdit = new RepositoryItemComboBox();
                }
                else if (type == "Button")
                {
                    int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
                    view.Columns[i].ColumnEdit = new RepositoryItemButtonEdit() { TextEditStyle = TextEditStyles.HideTextEditor };
                    view.Columns[i].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                }
                else
                {
                    view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
                }
            }
            foreach (XmlNode columnNode in columns)
            {
                string name = columnNode.Attributes["Name"].InnerText;
                GridColumn column = view.Columns[name];
                column.Caption = columnNode.Attributes["HeaderText"] != null ? columnNode.Attributes["HeaderText"].InnerText : "";
                column.Width = int.Parse(columnNode.Attributes["Width"].InnerText);
                column.VisibleIndex = int.Parse(columnNode.Attributes["DisplayIndex"].InnerText);
            }
        }

    }
}
