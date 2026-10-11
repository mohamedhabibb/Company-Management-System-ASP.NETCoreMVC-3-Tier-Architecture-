using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC_With_PL_Arch.Helpers
{
    public static class DecumentSettings
    {
        public static string UploadFile(IFormFile file , string FolderName)
        {
            //1.Get Located Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            //2.Get File Name and Make it Unique
            string FileName=$"{Guid.NewGuid()}{file.FileName}";
            //3.Get File Path[Folder Path + FileName]
            string FilePath=Path.Combine(FolderPath, FileName);
            //4.Save File As Streams
            using var  Fs= new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);
            //5.Return File Name (to store in database)
            return FileName;
        }
        public static void DeleteFile(string FileName , string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName , FileName);

            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }


        }

    }
}
