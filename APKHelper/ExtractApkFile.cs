using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;

namespace APKHelper
{
    public class ExtractApkFile
    {

        public static string  ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave, int index)
        {
            string path = null;
            //读取apk,通过解压的方式读取
            using (var zip = ZipFile.Read(APKFilePath))
            {
                using (Stream zipstream = zip[fileResourceLocation].OpenReader())
                {

                    string fileLocation = Path.Combine(FilePathToSave, string.Format("{0}-{1}", index, fileResourceLocation.Split(Convert.ToChar(@"/")).Last()));
                    using (FileStream output = File.Create(fileLocation))
                    {
                        try
                        {
                            zipstream.CopyTo(output);
                            path = fileLocation;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                }
            }
            return path;
        }

        /*
        public static void ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave, int index)
        {
            using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(APKFilePath)))
            {
                using (var filestream = new FileStream(APKFilePath, FileMode.Open, FileAccess.Read))
                {
                    ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == fileResourceLocation)
                        {
                            string fileLocation = Path.Combine(FilePathToSave, string.Format("{0}-{1}", index, fileResourceLocation.Split(Convert.ToChar(@"/")).Last()));
                            using (Stream strm = zipfile.GetInputStream(item))
                            using (FileStream output = File.Create(fileLocation))
                            {
                                try
                                {
                                    strm.CopyTo(output);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }

                        }
                    }
                }
            }
        }
         * */
    }
}
