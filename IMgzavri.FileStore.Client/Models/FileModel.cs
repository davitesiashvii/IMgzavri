using System;

namespace IMgzavri.FileStore.Client.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public byte[] File { get; set; }

        public FileModel(IMgzavri.FileStore.Domain.File file, byte[] bytes)
        {
            Id = file.Id;
            Name = file.Name;
            Extension = file.Extension;
            ContentType = file.ContentType;
            Size = file.Size;
            File = bytes;
        }
    }
}
