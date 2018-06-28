using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Configuration;

namespace OnlieFileTransfer.common
{
   public class CompressFile
    {
        String SrcPath;
        public CompressFile(string  src_Path)
        {
            SrcPath = src_Path;
        }
        public String Compress()
        {
            string lsDatetime = DateTime.Now.ToString("yyyy_MM_dd__HH_mm_ss_ffffff");
            FileAttributes attr = File.GetAttributes(SrcPath);
            bool isfile = true;
            string lsSplits = string.Empty;
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isfile = false;
                lsSplits = SrcPath.Split('\\').Last();
            }
            else
            {
                isfile = true;
                lsSplits = SrcPath.Split('\\').Last().Split('.')[0];
            }


            string loZipPath = ConfigurationManager.AppSettings["ZipFileDesc"].ToString() + lsDatetime + "_" + lsSplits + ".zip";
                


            FileStream fsOut = File.Create(loZipPath);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);


            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression


            string lsBaseFolder = SrcPath.Substring(0, SrcPath.LastIndexOf("\\"));


            int folderOffset = lsBaseFolder.Length + (lsBaseFolder.EndsWith("\\") ? 0 : 1);
            

            //detect whether its a directory or file
            if (!isfile)
                CompressFolder(SrcPath, zipStream, folderOffset);
            else
                CompressFiles(SrcPath, zipStream, folderOffset);

           

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();

            return loZipPath;
        }

        private void CompressFiles(string fsFullFilePath, ZipOutputStream zipStream, int folderOffset)
        {
            try
            {

                FileInfo fi = new FileInfo(fsFullFilePath);

                string entryName = fsFullFilePath.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(fsFullFilePath))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();                
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        private void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }

        }

    }
