using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Net.Sockets;

namespace OnlieFileTransfer
{
    public delegate void CmpQPusher(string fsDelegate, ThreadPoolParams cbobj);
    public partial class ViewCurrentImages : Form
    {
        public static int MAXPROCCAP = 5;
        public static int MAXTHREAD = 3;
        public string TCPIP = ConfigurationManager.AppSettings["TCPClientIP"].ToString();
        public Int32 TCPPORT = Convert.ToInt32(ConfigurationManager.AppSettings["TCPClientPort"].ToString());


        private StringBuilder moStringBuilder;
        private bool m_bDirty;
        private System.IO.FileSystemWatcher m_Watcher;
        private bool m_bIsWatching;

        #region Internals
        Thread AllocationThread;
        Thread TCPSenderThread;
        ThreadPoolParams[] JobPool;
        public TcpClient TcpClientSock;


        readonly Queue<String> TriggeredFilePaths = new Queue<string>(MAXPROCCAP);
      //  int TFCIndex = -1;
        readonly Queue<String> CompressedFilePaths = new Queue<string>(MAXPROCCAP);

        #endregion

        public void PushToCompressedFilePathsQueue(string fsPath, ThreadPoolParams CBObj)
        {
            lock (CompressedFilePaths)
            {
                Console.WriteLine(" PushToCompressedFilePathsQueue " + fsPath);
                CompressedFilePaths.Enqueue(fsPath);
                System.Threading.Monitor.Pulse(CompressedFilePaths);
                CBObj.IsAvailable = true;
            }
        }

        public void TCPSender()
        {

            Console.WriteLine(" TCPSender Thread started : " + Thread.CurrentThread.ManagedThreadId);
            TcpClientSock = new TcpClient();
            int PART = 1500;
            try
            {
                TcpClientSock.Connect(TCPIP, TCPPORT);
                Console.WriteLine("Connect to server");
                Stream stm = TcpClientSock.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                while (true)
                {
                    //block operation will decide in future
                    lock (CompressedFilePaths)
                    {
                        System.Threading.Monitor.Wait(CompressedFilePaths);
                        if (CompressedFilePaths.Count > 0)
                        {
                            String data = CompressedFilePaths.Dequeue();
                            Console.WriteLine("Got file for send - " + data);
                            byte[] SendByte = new byte[31457280];
                            //We have to prepare the file acording into the protocol
                            int liPos = 0;

                            UInt32 lsFileNameSize = (UInt32)data.Length;

                            BitConverter.GetBytes(lsFileNameSize).CopyTo(SendByte, liPos);
                            liPos += sizeof(uint);
                            byte[] Temp = asen.GetBytes(data);
                            Temp.CopyTo(SendByte, liPos);
                            liPos += (int)lsFileNameSize;

                            byte[] Fcontent = File.ReadAllBytes(data);
                            UInt32 FLen = (UInt32)Fcontent.Length;

                            BitConverter.GetBytes(FLen).CopyTo(SendByte, liPos);
                            liPos += sizeof(uint); 
                            Fcontent.CopyTo(SendByte, liPos);
                            liPos += (int)FLen;

                            int  j = 0, p;
                            while (j < liPos)
                            {
                                p = Math.Min(PART, liPos - j);
                                stm.Write(SendByte, j, p);
                                j += p;
                                Console.WriteLine("message sent: " + p + ", TOTAL: (" + j + "/" + liPos + ")");
                                Thread.Sleep(1);
                            }
                            Console.WriteLine("Complete send to server");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCPSender - " + ex.Message);
            }
        }
        public void Allocator()
        {
            Console.WriteLine("Allocator Thread started : " + Thread.CurrentThread.ManagedThreadId);
            int TrdIndx = 0;
            while (true)
            {
                //block operation
                lock (TriggeredFilePaths)
                {
                    System.Threading.Monitor.Wait(TriggeredFilePaths);
                    if (TriggeredFilePaths.Count > 0)
                    {
                        try
                        {
                            String data = TriggeredFilePaths.Dequeue();
                            Console.WriteLine(data + " got File ");
                            Compressor cm = new Compressor(data);
                            while (JobPool[TrdIndx].IsAvailable != true)
                                TrdIndx = (TrdIndx + 1) % MAXTHREAD;
                            Console.WriteLine(" Found jobPool for file " + data + " TrdIndx -" + TrdIndx);
                            JobPool[TrdIndx].CompObj = cm;
                            JobPool[TrdIndx].IsAvailable = false;
                            Console.WriteLine(" Calling Notify " + JobPool[TrdIndx].CompObj);
                            JobPool[TrdIndx].Notify();
                            Console.WriteLine(" after Notify " );

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(" Allocator Exception - " + ex.Message);
                        }
                    }
                }

            }
        }

        public void InitializeInternals()
        {
           
            JobPool = new ThreadPoolParams[MAXTHREAD];
            for(int i =0;i<MAXTHREAD;i++)
            {
                JobPool[i] = new ThreadPoolParams();
                JobPool[i].Init(this.PushToCompressedFilePathsQueue);

            }

            //Start the TCPSenderThread thread 
            TCPSenderThread = new Thread(this.TCPSender);
            TCPSenderThread.Start();
            //Start AllocationThread

            AllocationThread = new Thread(this.Allocator);
            AllocationThread.Start();

        }
        public ViewCurrentImages()
        {
            InitializeComponent();
            moStringBuilder = new StringBuilder();
            m_bDirty = false;
            m_bIsWatching = false;
        }
        private void SetHeaderAndWidth()
        {
            dgvFilesList.Columns[0].HeaderText = "File Name";
            dgvFilesList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFilesList.Columns[1].HeaderText = "File Path";
            dgvFilesList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ViewCurrentImages_Load(object sender, EventArgs e)
        {
            InitializeInternals();
            BindFileGridview();       
            StartErrorFileWatcher();
            StartFileWatcher();
            AddLinkButtonToDGV();
        }

        private void StartErrorFileWatcher()
        {
            if (Directory.Exists(Path.Combine(ConfigurationManager.AppSettings["ErrorDirectoryFullPath"].ToString(), DateTime.Now.ToString("ddMMyyyy"))))
            {
                m_Watcher = new System.IO.FileSystemWatcher();
                m_Watcher.Filter = "*.*";
                m_Watcher.Path = Path.Combine(ConfigurationManager.AppSettings["ErrorDirectoryFullPath"].ToString(), DateTime.Now.ToString("ddMMyyyy"));
                m_Watcher.IncludeSubdirectories = true;

                m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
                m_Watcher.Created += new FileSystemEventHandler(OnChanged);
                m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
                m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
                m_Watcher.EnableRaisingEvents = true;
            }
        }
        private void BindFileGridview()
        {
            dgvFilesList.DataSource = null;
            dgvFilesList.Rows.Clear();
            BindAwaitingFile();
            StartErrorFileWatcher();
            BindCompleteFile();
            BindImage();
        }


        private void BindCompleteFile()
        {
            if (Directory.Exists(Path.Combine(ConfigurationManager.AppSettings["CompleteDirectoryFullPath"].ToString(), DateTime.Now.ToString("ddMMyyyy"))))
            {
                string[] loCompleteFilePaths = Directory.GetFiles(Path.Combine(ConfigurationManager.AppSettings["CompleteDirectoryFullPath"].ToString(), DateTime.Now.ToString("ddMMyyyy")));
                string[] loCompleteFolderPaths = Directory.GetDirectories(Path.Combine(ConfigurationManager.AppSettings["CompleteDirectoryFullPath"].ToString(), DateTime.Now.ToString("ddMMyyyy")));
                foreach (string lsFilePath in loCompleteFilePaths)
                {
                    dgvFilesList.Rows.Add("File", System.IO.Path.GetFileName(lsFilePath), lsFilePath, "Success");
                }
                foreach (string lsFolderPath in loCompleteFolderPaths)
                {
                    dgvFilesList.Rows.Add("Folder", System.IO.Path.GetFileName(lsFolderPath), lsFolderPath, "Success");
                }
            }
        }
        private void BindAwaitingFile()
        {
            string[] loAwaitingFilePaths = Directory.GetFiles(ConfigurationManager.AppSettings["AwaitingDirectoryFullPath"].ToString());
            string[] loAwaitingFolderPaths = Directory.GetDirectories(ConfigurationManager.AppSettings["AwaitingDirectoryFullPath"].ToString());
            foreach (string lsFilePath in loAwaitingFilePaths)
            {
                dgvFilesList.Rows.Add("File", System.IO.Path.GetFileName(lsFilePath), lsFilePath, "Awaiting");
            }
            foreach (string lsFolderPath in loAwaitingFolderPaths)
            {
                dgvFilesList.Rows.Add("Folder", System.IO.Path.GetFileName(lsFolderPath), lsFolderPath, "Awaiting");
            }
        }
        private void BindImage()
        {

            Bitmap bmp;
            string lsFilePath = Application.StartupPath + @"\..\..\Images\file.png";
            string lsFolderPath = Application.StartupPath + @"\..\..\Images\folder.png";
            for (int x = 0; x <= dgvFilesList.Rows.Count - 1; x++)
            {
                DataGridViewImageCell cell = (DataGridViewImageCell)dgvFilesList.Rows[x].Cells["PathType"];
                if (dgvFilesList.Rows[x].Cells["PathType"].Value.ToString() == "File")
                {
                    bmp = new Bitmap(lsFilePath);
                }
                else
                {
                    bmp = new Bitmap(lsFolderPath);
                }
                cell.Value = bmp;
            }
        }
        private void StartFileWatcher()
        {

            m_bIsWatching = true;
            m_Watcher = new System.IO.FileSystemWatcher();
            m_Watcher.Filter = "*.*";
            m_Watcher.Path = ConfigurationManager.AppSettings["AwaitingDirectoryFullPath"].ToString();
            m_Watcher.IncludeSubdirectories = true;

            m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
            m_Watcher.Created += new FileSystemEventHandler(OnChanged);
            m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
            m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
            m_Watcher.EnableRaisingEvents = true;
            Console.WriteLine("File Watcher Thread started : " + Thread.CurrentThread.ManagedThreadId);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!m_bDirty)
            {
                moStringBuilder.Remove(0, moStringBuilder.Length);
                moStringBuilder.Append(e.FullPath);
                moStringBuilder.Append(" ");
                moStringBuilder.Append(e.ChangeType.ToString());
                moStringBuilder.Append("    ");
                moStringBuilder.Append(DateTime.Now.ToString());
                m_bDirty = true;
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!m_bDirty)
            {
                moStringBuilder.Remove(0, moStringBuilder.Length);
                moStringBuilder.Append(e.OldFullPath);
                moStringBuilder.Append(" ");
                moStringBuilder.Append(e.ChangeType.ToString());
                moStringBuilder.Append(" ");
                moStringBuilder.Append("to ");
                moStringBuilder.Append(e.Name);
                moStringBuilder.Append("    ");
                moStringBuilder.Append(DateTime.Now.ToString());
                m_bDirty = true;
            }
        }

        private void tmrEditNotify_Tick(object sender, EventArgs e)
        {
            if (m_bDirty)
            {
                lstNotification.BeginUpdate();
                lstNotification.Items.Add(moStringBuilder.ToString());
                lstNotification.EndUpdate();
                BindFileGridview();
                AddLinkButtonToDGV();
                m_bDirty = false;
            }
        }

        private void AddLinkButtonToDGV()
        {
            for (int x = 0; x <= dgvFilesList.Rows.Count - 1; x++)
            {
                DataGridViewLinkCell loLinkCell = (DataGridViewLinkCell)dgvFilesList.Rows[x].Cells["FileAction"];
                if (dgvFilesList.Rows[x].Cells["FileStatus"].Value.ToString() == "Awaiting")
                {
                    loLinkCell.LinkColor = Color.Blue;
                    loLinkCell.TrackVisitedState = true;
                    loLinkCell.UseColumnTextForLinkValue = false;
                    loLinkCell.Value = "Send To Server";
                }
                else if (dgvFilesList.Rows[x].Cells["FileStatus"].Value.ToString() == "Error")
                {
                    loLinkCell.LinkColor = Color.Red;
                    loLinkCell.TrackVisitedState = true;
                    loLinkCell.UseColumnTextForLinkValue = false;
                    loLinkCell.Value = "Retry To Server";
                }
                else
                {
                    loLinkCell.UseColumnTextForLinkValue = false;
                    loLinkCell.Value = "";
                }
            }
        }

        private void dgvFilesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string lsPath = dgvFilesList.Rows[e.RowIndex].Cells["FilePath"].Value.ToString();
                // Pushing file path into TriggeredFilePaths queue
                Console.WriteLine("Pushing to trigger file path - " + lsPath);
                PushToTriggeredFilePaths(lsPath);

                Boolean lbResult;
                if (CopyFileToServer(lsPath))
                {
                    lbResult = true; //MoveFileOnCondition(lsPath, true);
                }
                else
                {
                    lbResult = true;//MoveFileOnCondition(lsPath, false);
                }
                //Write here your code...
                MessageBox.Show(lbResult ? "Successfully Moved" : "Error Occured in Moving");
            }
        }

        private void PushToTriggeredFilePaths(string lsPath)
        {
            lock (TriggeredFilePaths)
            {
                Console.WriteLine(lsPath  + "  inserted into TriggeredFilePaths");
                TriggeredFilePaths.Enqueue(lsPath);
                System.Threading.Monitor.Pulse(TriggeredFilePaths);
            }
        }

        private Boolean CopyFileToServer(string fsFilePath)
        {
            try
            {
                FileInfo loFileInfo = new FileInfo(fsFilePath);
                if (loFileInfo.Exists)
                {
                    loFileInfo.CopyTo(ConfigurationManager.AppSettings["ServerDirectoryFullPath"] + Path.GetFileName(fsFilePath));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CopyFileToServer exception " + ex.Message);
                return false;
            }
        }

        private Boolean MoveFileOnCondition(string fsFilePath, Boolean fbIsSuccess)
        {
            try
            {
                FileInfo loFileInfo = new FileInfo(fsFilePath);
                if (loFileInfo.Exists)
                {
                    if (fbIsSuccess)
                    {
                        CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["CompleteDirectoryFullPath"], DateTime.Now.ToString("ddMMyyyy")));
                        loFileInfo.MoveTo(ConfigurationManager.AppSettings["CompleteDirectoryFullPath"] + Path.Combine(DateTime.Now.ToString("ddMMyyyy"), Path.GetFileName(fsFilePath)));
                    }
                    else
                    {
                        CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["ErrorDirectoryFullPath"], DateTime.Now.ToString("ddMMyyyy")));
                        loFileInfo.MoveTo(ConfigurationManager.AppSettings["ErrorDirectoryFullPath"] + Path.Combine(DateTime.Now.ToString("ddMMyyyy"), Path.GetFileName(fsFilePath)));
                    }
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private Boolean CreateDirectory(string fsFolderPath)
        {
            try
            {
                DirectoryInfo loDirectoryInfo = new DirectoryInfo(fsFolderPath);
                if (!loDirectoryInfo.Exists)
                {
                    Directory.CreateDirectory(fsFolderPath);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class Compressor
    {
        public String SrcPath;
        public Compressor(String src_path)
        {
            SrcPath = src_path;
        }
        public String GetCompressorObjPath()
        {
            common.CompressFile loCF = new common.CompressFile(SrcPath);
            return loCF.Compress();
        }
    }
    public class ThreadPoolParams
    {
        public Thread TrdObj;
        public bool IsAvailable = true;
        public Compressor CompObj;
        public CmpQPusher cmpQPusher;
        public Object MutObj;
        public ThreadPoolParams()
        {
            TrdObj = new Thread(this.Execute);
            MutObj = new Object();
        }

        public void Init(CmpQPusher cmpQ)
        {
            cmpQPusher = cmpQ;
            CompObj = new Compressor(string.Empty);
            TrdObj.Start();
        }
        public void Execute()
        {
            Console.WriteLine(" JobPool Execute Thread started : "+ Thread.CurrentThread.ManagedThreadId);
            string lsOutputPath = string.Empty;
            while (true)
            {
                //block operation
                lock (MutObj)
                {
                    try
                    {
                        System.Threading.Monitor.Wait(MutObj);
                        Console.WriteLine(" Got CompObj " + CompObj);
                        if (!IsAvailable)
                        {
                            Console.WriteLine(" calling Compressor ");
                            lsOutputPath = CompObj.GetCompressorObjPath();
                            Console.WriteLine(" File compress complete ");                            
                            cmpQPusher(lsOutputPath, this);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Execute Excaption - " + ex.Message);
                    }
                }
            }
        }
        public void Notify()
        {
            try
            {
                Console.WriteLine("Notify Start");
                lock (MutObj)
                {
                    System.Threading.Monitor.Pulse(MutObj);
                    Console.WriteLine("Notify Stop");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Notify -" + ex.Message);

            }
        }

    }
}

    
    
