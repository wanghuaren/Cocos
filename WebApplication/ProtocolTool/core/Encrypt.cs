using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ProtocolTool.core
{
    class Encrypt
    {
        private static Encrypt _instance;
        public static Encrypt instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Encrypt();
                return _instance;
            }
        }
        public Byte VERSION = 66;
        private string imagResPacket = "/Res.MPQ";
        private BinaryWriter imgResBW;
        public string rootPath;
        public void encryptRes(string _path)
        {
            if (_path == rootPath)
            {
                copyDirectory(rootPath, rootPath + "/../Resource_copy");
                imgResBW = new BinaryWriter(File.Create(rootPath + "/res" + imagResPacket));
            }
            DirectoryInfo directory = new DirectoryInfo(_path);
            if (directory.Exists)
            {
                DirectoryInfo[] _directory = directory.GetDirectories();
                for (int i = 0; i < _directory.Length; i++)
                {

                    encryptRes(_directory[i].FullName);
                }
                //if (_path.IndexOf("Resources\\res")>=0&&_path.IndexOf("Resources\\res")>=_path.Length-15)
                //{

                //}
                FileInfo[] _file = directory.GetFiles();
                for (int i = 0; i < _file.Length; i++)
                {

                    if (_file[i].Extension == ".lua")
                    {
                        encryptLua(_file[i]);
                    }
                    else if (_file[i].Extension == ".png" || _file[i].Extension == ".jpg")
                    {
                        encryptImg(_file[i]);
                    }
                    else if (_file[i].Extension == ".ttf" || _file[i].Extension == ".ttc")
                    {
                        encryptLua(_file[i]);
                    }
                }
            }
            if (_path == rootPath)
                imgResBW.Close();
        }

        private void encryptLua(FileInfo _file)
        {
            string _path = _file.FullName;
            //_path = rootPath + "/../encrypt/" + _path.Replace(rootPath, "");

            FileInfo _fileWrite = new FileInfo(_file.FullName.Replace(_file.Extension, ".temp"));
            FileStream _fileStrmWrite = new FileStream(_fileWrite.FullName, FileMode.Create);
            //if (_fileWrite.Exists)
            //{
            //    _fileStrmWrite = new FileStream(_fileWrite.FullName, FileMode.Create);
            //}
            //else
            //{
            //    Directory.CreateDirectory(_fileWrite.FullName.Replace(_fileWrite.Name, ""));
            //    _fileStrmWrite = File.Create(_path);
            //}
            if (_file.Name == "main.lua" || _file.Extension == ".ttf" || _file.Extension == ".ttc")
            {
                _fileStrmWrite.Close();
                //  _file.CopyTo(_path, true);
                File.Delete(_fileWrite.FullName);
                return;
            }

            FileStream _fileStrm = new FileStream(_file.FullName, FileMode.Open);
            BinaryReader _br = new BinaryReader(_fileStrm);
            BinaryWriter _bw = new BinaryWriter(_fileStrmWrite);

            Byte _code = 128;
            Byte _byte = 1;
            while (_br.BaseStream.Position < _br.BaseStream.Length)
            {
                _byte = _br.ReadByte();
                _byte ^= _code;
                _bw.Write(_byte);
            }
            _br.Close();
            _bw.Close();
            _fileStrm.Close();
            _fileStrmWrite.Close();
            _fileWrite.CopyTo(_file.FullName, true);
            File.Delete(_fileWrite.FullName);
        }
        private void encryptImg(FileInfo _file)
        {


            FileStream _fileRead = File.OpenRead(_file.FullName);
            BinaryReader _br = new BinaryReader(_fileRead);
            string _path = _file.FullName.Replace(rootPath + "\\", "").Replace("\\", "/").Replace("LuaScript/", "");
            imgResBW.Write(VERSION);
            byte[] _pathByte = Encoding.UTF8.GetBytes(_path);
            imgResBW.Write(_pathByte.Length);
            imgResBW.Write(_pathByte);
            imgResBW.Write((Byte)0);
            imgResBW.Write((int)_br.BaseStream.Length);
            imgResBW.Write(_br.ReadBytes((int)_br.BaseStream.Length));
            _fileRead.Close();
            //_path = rootPath + "\\..\\encrypt";
            //_path += _file.DirectoryName.Replace(rootPath, "");
            //if (!Directory.Exists(_path))
            //{
            //    Directory.CreateDirectory(_path);
            //}
            //_path = rootPath + "\\..\\encrypt" + _file.FullName.Replace(rootPath, "");
            File.Create(_file.FullName);
        }

        public void copyDirectory(string sourceDirectory, string destDirectory)
        {
            //判断源目录和目标目录是否存在，如果不存在，则创建一个目录
            if (!Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(sourceDirectory);
            }

            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }

            //拷贝文件
            copyFile(sourceDirectory, destDirectory);

            //拷贝子目录       
            //获取所有子目录名称
            string[] directionName = Directory.GetDirectories(sourceDirectory);
            foreach (string directionPath in directionName)
            {
                //根据每个子目录名称生成对应的目标子目录名称
                string directionPathTemp = destDirectory + "\\" + directionPath.Substring(sourceDirectory.Length + 1);
                //递归下去
                copyDirectory(directionPath, directionPathTemp);
            }
        }

        public void copyFile(string sourceDirectory, string destDirectory)
        {
            //获取所有文件名称
            string[] fileName = Directory.GetFiles(sourceDirectory);
            foreach (string filePath in fileName)
            {
                //根据每个文件名称生成对应的目标文件名称
                string filePathTemp = destDirectory + "\\" + filePath.Substring(sourceDirectory.Length + 1);
                //若不存在，直接复制文件；若存在，覆盖复制
                if (File.Exists(filePathTemp))
                {
                    File.Copy(filePath, filePathTemp, true);
                }
                else
                {
                    File.Copy(filePath, filePathTemp);

                }
            }
        }
    }
}
