using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerLock
{
    public partial class Locker : Form
    {

        Thread cerrar;
        Point posIni =new Point(5,5);
        bool corre = true;
        public Locker()
        {
            InitializeComponent();
            pnIconos.BackColor=Color.FromArgb(80,Color.DarkGray);
            pnIconos.Refresh();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private string obtenerEnlace(string file)
        {
            try
            {
                FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                using (BinaryReader fileReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0x14, SeekOrigin.Begin);
                    uint flags = fileReader.ReadUInt32();
                    if ((flags & 1) == 1)
                    {
                        fileStream.Seek(0x4c, SeekOrigin.Begin);
                        uint offset = fileReader.ReadUInt16();
                        fileStream.Seek(offset, SeekOrigin.Current);
                    }

                    long fileInfoStartsAt = fileStream.Position;
                    uint totalStructLength = fileReader.ReadUInt32();
                    fileStream.Seek(0xc, SeekOrigin.Current);
                    uint fileOffset = fileReader.ReadUInt32();
                    fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin);
                    long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position;
                    char[] linkTarget = fileReader.ReadChars((int)pathLength);
                    var link = new string(linkTarget);

                    string[] path = link.Split('.');
                    string ext = path[path.Length-1];
                    int p = ext.IndexOf($"\0");
                    ext =ext.Substring(0, ext.IndexOf($"\0"));
                    path[path.Length - 1] = ext;
                    link = string.Join(".",path);
                    int begin = link.IndexOf("\0\0");
                    if (begin > -1)
                    {
                        int end = link.IndexOf("\\\\", begin + 2) + 2;
                        end = link.IndexOf('\0', end) + 1;

                        string firstPart = link.Substring(0, begin);
                        string secondPart = link.Substring(end);

                        return firstPart + secondPart;
                    }
                    else
                    {
                        return link;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        private void killProcess(string process)
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c taskkill /IM " + process;
            p.StartInfo = startInfo;
            while (corre)
            {
                p.Start();
            }
            cerrar.Abort();
        }

        private Bitmap ObtieneIcono(string r)
        {
            Bitmap icono = new Bitmap(50, 50);
            icono = Icon.ExtractAssociatedIcon(r).ToBitmap();
            return icono;
        }

        private Point obtienePos()
        {
            int cant = pnIconos.Controls.Count;
            if (cant > 0)
            {
                if ((posIni.X + 55) > pnIconos.Width)
                {
                    posIni.X = 5;
                    posIni.Y = posIni.Y + 55;
                }
                else
                {
                    posIni.X = (posIni.X) + 55;                
                }
            }
            return posIni;
        }
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string r;
            foreach (string file in files)
            {
                string ext= file.Substring(file.Length - 3, 3).ToLower();
                if ((new string[] {"exe","lnk","jar" }).ToList().IndexOf(ext)>-1)
                {
                    r = file;
                    if (ext=="lnk")
                    {
                        r= obtenerEnlace(file);
                    }
                    PictureBox pb=new PictureBox();
                    pb.Image = ObtieneIcono(r);
                    pb.Size = new Size(50, 50);
                    pb.Tag = r;
                    pb.SizeMode= PictureBoxSizeMode.Zoom;
                    pb.Location = obtienePos();
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.Cursor= Cursors.Hand;
                    pb.Click += Pb_Click;
                    pb.Parent= pnIconos;
                    pnIconos.Controls.Add(pb);
                    string[] f = r.Split('\\');
                    /*cerrar = new Thread(()=>killProcess(f[f.Length - 1]));
                    cerrar.Start();*/
                    //textBox1.Text += (f[f.Length-1].Trim()) + Environment.NewLine;
                }
            }
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Process.Start(p.Tag.ToString());
        }

        private void Locker_Load(object sender, EventArgs e)
        {
            
        }

        private void Locker_FormClosing(object sender, FormClosingEventArgs e)
        {
            corre = false;
        }
    }
}
