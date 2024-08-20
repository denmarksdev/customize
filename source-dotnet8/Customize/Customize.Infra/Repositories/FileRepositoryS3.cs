using Amazon.S3;
using Amazon.S3.Model;
using Customize.Domain.DataObject;
using Customize.Domain.DataObject.File;
using Customize.Domain.Repositories;

namespace Customize.Infra.Repositories
{
    public class FileRepositoryS3 : IFileRepository
    {
        private readonly IAmazonS3 _amazonS3;

        public FileRepositoryS3(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public async Task<Result> PutAsync(PutFile input) 
        {
            var result = new Result();

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = input.BucketName,
                    Key = input.Key,
                    ContentBody = input.Message
                };

                var response = await _amazonS3.PutObjectAsync(putRequest);
                
                result.Success = response.HttpStatusCode == System.Net.HttpStatusCode.OK;
                if (!result.Success) 
                {
                    result.Errors.Add("Reposta S3", response.HttpStatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Errors.Add("NaoGerenciado", ex.Message);
            }

            return result;
        } 
    }
}
