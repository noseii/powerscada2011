using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using System.Reflection;

namespace PowerScada
{
    

    public partial class frmRolIsHakki : Form
    {
        mymodel.myenum.GorevTuru AktifRol = myenum.GorevTuru.Ebe;
         DataTable GorevTurleriTable = new DataTable();
           
        public frmRolIsHakki()
        {
            InitializeComponent();
            GorevTurleriTable.Columns.AddRange(
                new DataColumn[] {
									 new DataColumn("Rol",typeof(string)),
									 new DataColumn("Id",typeof(int)),
									
								 });
            BindRol();
            
            IsHaklarigrid.DataSource = PrepareIsHaklari(AktifRol, 0);
            
        }

        private void BindRol()
        {
            Rolgrid.DataSource = GetRol("");
        }


        private DataTable GetRol(string rol)
        {
             System.Array gorevler = Enum.GetValues(typeof(mymodel.myenum.GorevTuru));
            foreach (mymodel.myenum.GorevTuru gturu in gorevler)
            {
                if (gturu != myenum.GorevTuru.Admin)
                {
                    DataRow row = GorevTurleriTable.NewRow();
                    string name = Enum.GetName(typeof(mymodel.myenum.GorevTuru), gturu);
                    row["Id"] = (int)gturu;
                    row["Rol"] = name;
                    GorevTurleriTable.Rows.Add(row);
                }
            }
            return GorevTurleriTable;

        }

       
       
        private DataTable GetRolEkranHakki(mymodel.myenum.GorevTuru aktifrol)
        {
            DataTable dtrolekran = new DataTable();
            if (aktifrol != 0)
            {
                Condition[] con = new Condition[1];
                con[0].Field = "Rol";
                con[0].Value = aktifrol;
                con[0].Operator = System.Operator.Equal;
                dtrolekran = Persistence.ReadListTable(typeof(RolHakki), new string[] { "*" }, con, null, 100);
            }
            else
            {
                dtrolekran = Persistence.ReadListTable(typeof(RolHakki), new string[] { "*" }, null, null, 100);
            }
            return dtrolekran;
        }

       

        private void txtbxRolAdi_TextChanged(object sender, EventArgs e)
        {
            //Rolgrid.DataSource = GetRol(txtbxRolAdi.Text);

            DataTable gridTable = (DataTable)Rolgrid.DataSource;

            // Set the RowFilter to display a company names that 
            // begin with A through I..
            if (string.IsNullOrEmpty(txtbxRolAdi.Text))
            {
                gridTable.DefaultView.RowFilter ="";
            }
            else
                gridTable.DefaultView.RowFilter = "Rol like '" + txtbxRolAdi.Text+"%'";

        }

        private void textBoxEkranIsmi_TextChanged(object sender, EventArgs e)
        {
            
                if (textBoxEkranIsmi.Text.Length > 0)
                {
                    DataTable dt = (IsHaklarigrid.DataSource as DataTable);


                    DataRow[] row = dt.Select("Hak like '" + textBoxEkranIsmi.Text + "%'");
                    DataTable dtyeni = dt.Clone();

                    foreach (DataRow rw in row)
                    {
                        DataRow dtrow = dtyeni.NewRow();
                        dtrow = rw;
                        dtyeni.ImportRow(dtrow);
                    }

                    IsHaklarigrid.DataSource = dtyeni;


                }
                else
                    IsHaklarigrid.DataSource = PrepareIsHaklari(AktifRol, 0);

            
           
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {  
            UpdateIsHaklari();
        }

      

        private void Rolgrid_SelectionChanged(object sender, EventArgs e)
        {
              if (Rolgrid.CurrentRow != null && Rolgrid.CurrentRow.Cells["Rol"].Value!=null && Rolgrid.CurrentRow.Cells["Rol"].Value != System.DBNull.Value)
                {

                    AktifRol = (mymodel.myenum.GorevTuru)Enum.Parse(typeof(mymodel.myenum.GorevTuru), Rolgrid.CurrentRow.Cells["Rol"].Value.ToString());
                    IsHaklarigrid.DataSource = PrepareIsHaklari(AktifRol, 0);
                }
                else
                    IsHaklarigrid.DataSource = PrepareIsHaklari(AktifRol, 0);
        }

        private DataTable PrepareIsHaklari(mymodel.myenum.GorevTuru rolu, int isYeriObjId)
        {
            DataTable isHaklariTable = new DataTable();
            isHaklariTable.Columns.AddRange(
                new DataColumn[] {
									
									 new DataColumn("Id",typeof(int)),
									 new DataColumn("Hak",typeof(string)),
									 new DataColumn("Var",typeof(bool)),
                                     //new DataColumn("Engelle",typeof(bool)),
									 new DataColumn("Aktif",typeof(bool))
								 });
            System.Array haklar = Enum.GetValues(typeof(mymodel.myenum.Hak));
            foreach (mymodel.myenum.Hak hak in haklar)
            {
                if (hak != mymodel.myenum.Hak.Undefined)
                { // Undefined hakkını eklemeye gerek yok
                    DataRow row = isHaklariTable.NewRow();
                    string name = Enum.GetName(typeof(mymodel.myenum.Hak), hak);
                    int p = name.IndexOf("_");

                    row["Id"] = (int)hak;
                    row["Hak"] = (p >= 0 ? name.Substring(p + 1) : name);
                    row["Var"] = false;
                    //row["Engelle"] = false;
                    row["Aktif"] = true;
                    
                    isHaklariTable.Rows.Add(row);
                }
            }
            isHaklariTable.AcceptChanges();

            RolHakki[] isHaklari=null;
            if (rolu > 0)
            {
                Condition[] con = new Condition[1];
                con[0].Field = "Rol";
                con[0].Operator = System.Operator.Equal;
                con[0].Value = rolu.ToString();

                isHaklari = Persistence.ReadList<RolHakki>(new string[] { "*" }, con, null, 100);
            }
            else
                isHaklari = Persistence.ReadList<RolHakki>(new string[] { "*" }, null, null, 100);


            foreach (RolHakki hak in isHaklari)
            {
                DataRow[] rows = isHaklariTable.Select("Id=" + ((int)hak.Hak).ToString());
                if (rows.Length > 0)
                {
                    rows[0]["Aktif"] = hak.Aktif;
                    //rows[0]["RolHakki.Id"] = hak.Id;
                    rows[0]["Var"] = hak.Var;
                    //rows[0]["Engelle"] = hak.Engelle;
                    
                }
            }
            isHaklariTable.AcceptChanges();
            return isHaklariTable;
        }


        private void UpdateIsHaklari()
        {
            DataTable isHakkiTable = (DataTable)IsHaklarigrid.DataSource;
            mymodel.myenum.Hak hak;
            bool engelle;
            bool hakVar;
           
            bool aktif;
            foreach (DataRow row in isHakkiTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    //long r = (int)row["Id"];
                    //if (r > 0)
                    //    Persistence.DeleteByKey(typeof(RolHakki), r, true);
                    hakVar = (row.IsNull("Hak") ? false : (bool)row["Var"]);
                    //engelle = (row.IsNull("Engelle") ? false : (bool)row["Engelle"]);
                    aktif = (row.IsNull("Aktif") ? false : (bool)row["Aktif"]);

                    //if (hakVar)// || engelle)
                    //{
                        hak = (row.IsNull("Id") ? mymodel.myenum.Hak.Undefined : (mymodel.myenum.Hak)(row["Id"]));
                        RolHakki rolHakki = new RolHakki();
                        rolHakki.Aktif = true;
                        //rolHakki.Engelle = engelle;
                        rolHakki.Hak = hak;
                        rolHakki.Rol = AktifRol;
                        rolHakki.Var = hakVar;

                        try
                        {
                            Condition[] con = new Condition[2];
                            con[0].Field = "Rol";
                            con[0].Operator = System.Operator.Equal;
                            con[0].Value = rolHakki.Rol.ToString();
                            con[1].Field = "Hak";
                            con[1].Value = rolHakki.Hak.ToString();
                            con[1].Operator = System.Operator.Equal;
                            try
                            {
                                RolHakki eskirolhakki = Persistence.Read<RolHakki>(con);
                                if (eskirolhakki != null)
                                {

                                    eskirolhakki.Hak = rolHakki.Hak;
                                    eskirolhakki.Aktif = rolHakki.Aktif;
                                    eskirolhakki.Var = rolHakki.Var;
                                    eskirolhakki.Update();
                                }
                                else
                                {
                                    rolHakki.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }


                //}
            }
            isHakkiTable.AcceptChanges();
        }

        private void Rolgrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataTable isHakkiTable = (DataTable)IsHaklarigrid.DataSource;
            DataTable degisiklikler=isHakkiTable.GetChanges(DataRowState.Modified);
            if (degisiklikler != null && degisiklikler.Rows.Count > 0)
            {
                if (MessageBox.Show("Yaptığınız değişiklikler kaydedilsin mi ?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    UpdateIsHaklari();
                }
            }
       
        }

       
    }
}

