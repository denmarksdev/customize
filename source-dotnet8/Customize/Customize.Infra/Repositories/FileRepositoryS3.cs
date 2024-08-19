using Amazon.S3;
using Amazon.S3.Model;
using Customize.Domain.DataObject;
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

        public async Task<Result> PutAsync(string key, string bucketName, string message) 
        {
            var result = new Result();

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    ContentBody = message
                };

                var response = await _amazonS3.PutObjectAsync(putRequest);

                result.Sucess = response.HttpStatusCode == System.Net.HttpStatusCode.OK;
                if (!result.Sucess) 
                {
                    result.Errors.Add("Resosta S3", response.HttpStatusCode.ToString());
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
