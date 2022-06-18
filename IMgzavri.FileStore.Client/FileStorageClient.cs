using IMgzavri.FileStore.Client.Models;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.FileStore.Client
{
    public class FileStorageClient : HttpClientService, IFileStorageClient
    {
        public FileStorageClient(IHttpClientFactory httpClientFactory, string clientName) : base(httpClientFactory, clientName)
        {
        }

        public async Task<Result> UploadFilesBytesAsync(List<FileSavingModel> files)
        {
            var dataAsJson = JsonConvert.SerializeObject(files);

            var httpContent = new StringContent(dataAsJson, Encoding.UTF8, @"application/json");

            return await PostAsync<Result>("/File/upload-files-bytes", httpContent);
        }

        public async Task<Result> UploadFileBytesAsync(FileSavingModel file)
        {
            var dataAsJson = JsonConvert.SerializeObject(file);

            var httpContent = new StringContent(dataAsJson, Encoding.UTF8, @"application/json");

            return await PostAsync<Result>("/File/upload-file-bytes", httpContent);
        }

        public async Task<Result> LoadFilePhysicalPathAsync(Guid id)
        {
            return await GetAsync<Result>($"/File/load-file-physical-path/{id}");
        }

        public async Task<Result> LoadFilesPhysicalPathsAsync(List<Guid> ids)
        {
            var idsAsJson = JsonConvert.SerializeObject(new GetFilesPhysicalPathsModel { FileIds = ids });

            var httpContent = new StringContent(idsAsJson, Encoding.UTF8, @"application/json");

            return await PostAsync<Result>("/File/load-files-physical-paths", httpContent);
        }

        public async Task<Result> LoadFileBytesAsync(Guid id)
        {
            return await GetAsync<Result>($"/File/load-file-bytes/{id}");
        }

        public async Task<Result> DeleteFileAsync(Guid id)
        {
            return await DeleteAsync<Result>($"/File/delete/{id}");
        }

        public async Task<Result> ExistsAsync(Guid id)
        {
            return await GetAsync<Result>($"/File/exists/{id}");
        }

        //public Task<Result> UploadFilesBytesAsync(List<FileSavingModel> files)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Result> UploadFileBytesAsync(FileSavingModel file)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Result> IFileStorageClient.LoadFilePhysicalPathAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Result> IFileStorageClient.LoadFilesPhysicalPathsAsync(List<Guid> ids)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Result> IFileStorageClient.LoadFileBytesAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Result> IFileStorageClient.DeleteFileAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Result> IFileStorageClient.ExistsAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
