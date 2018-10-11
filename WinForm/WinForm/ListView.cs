using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class ListView : Form
    {
        public ListView()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvMahasiswa.View = System.Windows.Forms.View.Details;
            lvMahasiswa.FullRowSelect = true;
            lvMahasiswa.GridLines = true;

            lvMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvMahasiswa.Columns.Add("Npm.", 70, HorizontalAlignment.Left);
            lvMahasiswa.Columns.Add("Nama", 180, HorizontalAlignment.Left);
            lvMahasiswa.Columns.Add("Jenis Kelamin", 80, HorizontalAlignment.Left);
            lvMahasiswa.Columns.Add("Tempat Lahir", 75, HorizontalAlignment.Left);
            lvMahasiswa.Columns.Add("Tanggal Lahir", 75, HorizontalAlignment.Left);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!mskNpm.MaskFull)
            {
                MessageBox.Show("NPM Harus Diisi!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mskNpm.Focus();
                return;
            }
            if (!(txtNama.Text.Length > 0))
            {
                MessageBox.Show("Nama Harus Diisi!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNama.Focus();
                return;
            }
            var jenisKelamin = rdoLakiLaki.Checked ? "Laki Laki" : "Perempuan";

            var result = MessageBox.Show("Apakah Data Ingin Disimpan ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var noUrut = lvMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mskNpm.Text);
                item.SubItems.Add(txtNama.Text);
                item.SubItems.Add(jenisKelamin);
                item.SubItems.Add(txtTempat.Text);
                item.SubItems.Add(dtpTanggal.Value.ToString("dd/MM/yyyy"));

                lvMahasiswa.Items.Add(item);

                ResetForm();
            }
        }

        private void ResetForm()
        {
            mskNpm.Clear();
            txtNama.Clear();
            rdoLakiLaki.Checked = true;
            txtTempat.Clear();
            dtpTanggal.Value = DateTime.Today;

            mskNpm.Focus();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvMahasiswa.SelectedItems.Count > 0)
            {
                var index = lvMahasiswa.SelectedIndices[0];
                var nama = lvMahasiswa.Items[index].SubItems[2].Text;

                var msg = String.Format("Apakah Data Mahasiswa '{0}' Ingin Dihapus :", nama);
                var result = MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    lvMahasiswa.Items[index].Remove();
                }
            }
            else
            {
                MessageBox.Show("Data Belum Dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            var msg = "Apakah Anda Yakin ?";

            var result = MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void lvwMahasiswa_DoubleClick(object sender, EventArgs e)
        {
            // fungsi double klik untuk me load data.
            // mengambil dari item kemudian di masukan ke textbox inputan sesuai dengan variabelnya.
            var index = lvMahasiswa.SelectedIndices[0];
            var npm = lvMahasiswa.Items[index].SubItems[1].Text;
            var name = lvMahasiswa.Items[index].SubItems[2].Text;
            var jk = lvMahasiswa.Items[index].SubItems[3].Text;
            var lahir = lvMahasiswa.Items[index].SubItems[4].Text;
            var tanggal = lvMahasiswa.Items[index].SubItems[5].Text;
            mskNpm.Text = npm;
            txtNama.Text = name;
            // di perlukan filter apakah kelamin l/p jika 1 maka rdol ter check, jika p maka rdop ter check.
            if (jk == "Laki-Laki")
            {
                rdoLakiLaki.Checked = true;
            }
            else if (jk == "Perempuan")
            {
                rdoPerempuan.Checked = true;
            }
            txtTempat.Text = lahir;
            dtpTanggal.Text = tanggal;
        }
    }
}
