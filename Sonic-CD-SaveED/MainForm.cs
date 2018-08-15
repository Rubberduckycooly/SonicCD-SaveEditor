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

        public RSDKv3.SaveFiles SaveData;

        int CurSave = 0;

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
                SaveData.Saves[CurSave].CharacterID = CharLB.SelectedIndex;
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
                SaveData.Saves[CurSave].LevelID = StageList.SelectedIndex;
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
                SaveData.Saves[CurSave].Lives = (int)LivesNUD.Value;
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
                SaveData = new RSDKv3.SaveFiles(dlg.FileName);
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
                GlobalUnkownNUD.Value = SaveData.unknown1;
                GlobalUnkown2NUD.Value = SaveData.unknown2;

                MSPNUD.Value = SaveData.Saves[CurSave].MSHolograms;
                LivesNUD.Value = SaveData.Saves[CurSave].Lives;
                StageList.SelectedIndex = SaveData.Saves[CurSave].LevelID;
                CharLB.SelectedIndex = SaveData.Saves[CurSave].CharacterID;
                Score1NUD.Value = SaveData.Saves[CurSave].Score[3];
                Score2NUD.Value = SaveData.Saves[CurSave].Score[2];
                Score3NUD.Value = SaveData.Saves[CurSave].Score[1];
                Score4NUD.Value = SaveData.Saves[CurSave].Score[0];
                SaveUnknown2NUD.Value = SaveData.Saves[CurSave].unknown2;
                GF1NUD.Value = SaveData.Saves[CurSave].GoodFutures[0];
                GF2NUD.Value = SaveData.Saves[CurSave].GoodFutures[1];
                GF3NUD.Value = SaveData.Saves[CurSave].GoodFutures[2];
                GF4NUD.Value = SaveData.Saves[CurSave].GoodFutures[3];
                RGNUD.Value = SaveData.Saves[CurSave].RoboMachines;

                Timestone1.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 0);
                Timestone2.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 1);
                Timestone3.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 2);
                Timestone4.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 3);
                Timestone5.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 4);
                Timestone6.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 5);
                Timestone7.Checked = IsBitSet(SaveData.Saves[CurSave].TimeStones, 6);
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
                SaveData.Saves[CurSave].MSHolograms = (ushort)MSPNUD.Value;
            }
        }

        private void selectSave1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                CurSave = 0;
                RefreshUI();
            }
        }

        private void selectSave2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                CurSave = 1;
                RefreshUI();
            }
        }

        private void selectSave3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                CurSave = 2;
                RefreshUI();
            }
        }

        private void selectSave4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                CurSave = 3;
                RefreshUI();
            }
        }

        private void SaveUnknown2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].unknown2 = (byte)SaveUnknown2NUD.Value;
            }
        }

        private void SaveUnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].Score[3] = (byte)Score1NUD.Value;
            }
        }

        private void Score2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].Score[2] = (byte)Score2NUD.Value;
            }
        }

        private void Score3NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].Score[1] = (byte)Score3NUD.Value;
            }
        }

        private void Score4NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].Score[0] = (byte)Score4NUD.Value;
            }
        }

        private void GlobalUnkownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.unknown1 = (byte)GlobalUnkownNUD.Value;
            }
        }

        private void GlobalUnkown2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.unknown2 = (byte)GlobalUnkown2NUD.Value;
            }
        }

        private void GF1NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].GoodFutures[0] = (byte)GF1NUD.Value;
            }
        }

        private void GF2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].GoodFutures[1] = (byte)GF2NUD.Value;
            }
        }

        private void GF3NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].GoodFutures[2] = (byte)GF3NUD.Value;
            }
        }

        private void GF4NUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].GoodFutures[3] = (byte)GF4NUD.Value;
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
                SaveData.Saves[CurSave].SetTimeStone(0, Timestone1.Checked);
                RefreshUI();
            }
        }

        private void Timestone2_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(1, Timestone2.Checked);
                RefreshUI();
            }
        }

        private void Timestone3_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(2, Timestone3.Checked);
                RefreshUI();
            }
        }

        private void Timestone4_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(3, Timestone4.Checked);
                RefreshUI();
            }
        }

        private void Timestone5_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(4, Timestone5.Checked);
                RefreshUI();
            }
        }

        private void Timestone6_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(5, Timestone6.Checked);
                RefreshUI();
            }
        }

        private void Timestone7_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(6, Timestone7.Checked);
                RefreshUI();
            }
        }

        private void AllTimestones_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].SetTimeStone(0, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(1, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(2, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(3, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(4, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(5, AllTimestones.Checked);
                SaveData.Saves[CurSave].SetTimeStone(6, AllTimestones.Checked);
                RefreshUI();
            }
        }

        private void RGNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SaveData != null)
            {
                SaveData.Saves[CurSave].RoboMachines = (ushort)RGNUD.Value;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }
    }
}
