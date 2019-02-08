using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sonic_CD_SaveED
{
    public partial class Mainform : Form
    {

        public RSDKv2.SaveFiles SaveData;

        string Filepath;

        public Mainform()
        {
            InitializeComponent();
        }

        void writeLineToConsole(string line)
        {
            Console.WriteLine(line);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.CharacterID = CharLB.SelectedIndex;
            }        
        }

        private void Mainform_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Bag 'O Dicks!");
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.ZoneID = StageList.SelectedIndex;
            }          
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic CD Save File|Sdata.bin";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
               SaveData.Write(dlg.FileName);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Lives = (int)LivesNUD.Value;
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Filepath != null)
            {
                SaveData.Write(Filepath);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Sonic CD Save File|Sdata.bin";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Filepath = dlg.FileName;
                SaveData = new RSDKv2.SaveFiles(dlg.FileName);
                RefreshUI();
            }
        }

        void RefreshUI()
        {
            if (SaveData != null)
            {
                SoundtrackLB.SelectedIndex = SaveData.OSTStyle;
                SpindashLB.SelectedIndex = SaveData.SpindashStyle;
                FilterLB.SelectedIndex = SaveData.Filter;
                MusNUD.Value = SaveData.MusVolume;
                SFXNUD.Value = SaveData.SFXVolume;
                TailsUnlockBox.Checked = SaveData.TailsUnlocked;
                NewSaveCB.Checked = SaveData.NewSave != 0;

                MSPNUD.Value = SaveData.FuturesSaved;
                LivesNUD.Value = SaveData.Lives;
                StageList.SelectedIndex = SaveData.ZoneID;
                CharLB.SelectedIndex = SaveData.CharacterID;
                Score1NUD.Value = SaveData.Score;
                UnknownValueNUD.Value = SaveData.unknown3;
                //GF1NUD.Value = SaveData.GoodFutures;

                Timestone1.Checked = IsBitSet(SaveData.TimeStones, 0);
                Timestone2.Checked = IsBitSet(SaveData.TimeStones, 1);
                Timestone3.Checked = IsBitSet(SaveData.TimeStones, 2);
                Timestone4.Checked = IsBitSet(SaveData.TimeStones, 3);
                Timestone5.Checked = IsBitSet(SaveData.TimeStones, 4);
                Timestone6.Checked = IsBitSet(SaveData.TimeStones, 5);
                Timestone7.Checked = IsBitSet(SaveData.TimeStones, 6);
            }

        }

        private void SoundtrackLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.OSTStyle = SoundtrackLB.SelectedIndex;
            }
        }

        private void SpindashLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SpindashStyle = SpindashLB.SelectedIndex;
            }
        }

        private void FilterLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Filter = FilterLB.SelectedIndex;
            }
        }

        private void MusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.MusVolume = (int)MusNUD.Value;
            }
        }

        private void SFXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SFXVolume = (int)SFXNUD.Value;
            }
        }

        private void TailsUnlockBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.TailsUnlocked = TailsUnlockBox.Checked = !SaveData.TailsUnlocked;
            }
        }

        private void MSPNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.FuturesSaved = (ushort)MSPNUD.Value;
            }
        }

        private void selectSave1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SaveFile = 0;
                RefreshUI();
            }
        }

        private void selectSave2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SaveFile = 1;
                RefreshUI();
            }
        }

        private void selectSave3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SaveFile = 2;
                RefreshUI();
            }
        }

        private void selectSave4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SaveFile = 3;
                RefreshUI();
            }
        }

        private void SaveUnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Score = (int)Score1NUD.Value;
            }
        }

        private void GlobalUnkownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                int i = 0;
                if (NewSaveCB.Checked) i = 1;
                SaveData.NewSave = i;
            }
        }

        private void GF1NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.GoodFutures = (byte)GF1NUD.Value;
            }
        }

        public bool IsBitSet(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private void Timestone1_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(0, Timestone1.Checked);
                RefreshUI();
            }
        }

        private void Timestone2_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(1, Timestone2.Checked);
                RefreshUI();
            }
        }

        private void Timestone3_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(2, Timestone3.Checked);
                RefreshUI();
            }
        }

        private void Timestone4_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(3, Timestone4.Checked);
                RefreshUI();
            }
        }

        private void Timestone5_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(4, Timestone5.Checked);
                RefreshUI();
            }
        }

        private void Timestone6_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(5, Timestone6.Checked);
                RefreshUI();
            }
        }

        private void Timestone7_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(6, Timestone7.Checked);
                RefreshUI();
            }
        }

        private void AllTimestones_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.SetTimeStone(0, AllTimestones.Checked);
                SaveData.SetTimeStone(1, AllTimestones.Checked);
                SaveData.SetTimeStone(2, AllTimestones.Checked);
                SaveData.SetTimeStone(3, AllTimestones.Checked);
                SaveData.SetTimeStone(4, AllTimestones.Checked);
                SaveData.SetTimeStone(5, AllTimestones.Checked);
                SaveData.SetTimeStone(6, AllTimestones.Checked);
                RefreshUI();
            }
        }

        private void RGNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                //SaveData.RoboMachines = (ushort)RGNUD.Value;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        private void openFromSteamAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HedgeModManager.Steam.SteamLocation == null)
                HedgeModManager.Steam.Init();
            var form = new HedgeModManager.SLWSaveForm();
            form.ShowDialog();
            if (!string.IsNullOrWhiteSpace(form.SID))
            {
                //200940
                string path = Path.Combine(HedgeModManager.Steam.SteamLocation, "userdata", form.SID, "200940", "local");
                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Could not Find Sonic CD Data in user Profile!", "Data Not Forund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                path = Path.Combine(path, "Sdata.bin");
                Filepath = path;
                SaveData = new RSDKv2.SaveFiles(path);
                RefreshUI();
            }
        }

        private void NewSaveCB_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (NewSaveCB.Checked) i = 1;
            SaveData.NewSave = i;
        }

        private void NextSSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NextSSBox.SelectedIndex < 0) NextSSBox.SelectedIndex = 0;
            SaveData.SpecialZoneID = NextSSBox.SelectedIndex;
        }

        private void UnknownValueNUD_ValueChanged(object sender, EventArgs e)
        {
            SaveData.unknown3 = (int)UnknownValueNUD.Value;
        }
    }
}
