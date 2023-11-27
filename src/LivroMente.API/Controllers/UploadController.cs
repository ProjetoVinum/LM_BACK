using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Drive.v3.DriveService;

namespace LivroMente.API.Controllers
{
    [Route("Api/Controller/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        //  static string[] Scopes = { DriveService.ScopeConstants.DriveFile};//DriveService.Scope.DriveFile };
        //  static string ApplicationName = "LivroEMente"; // Substitua pelo nome da sua aplicação
        //  static string ServiceAccountKeyPath = "client_secret.json"; // Substitua pelo caminho para o arquivo JSON de chave de serviço

        // static void Main(string[] args)
        // {
        //     var credential = GetServiceAccountCredential();

        //     // Crie um serviço Drive autenticado
        //     var service = new DriveService(new BaseClientService.Initializer()
        //     {
        //         HttpClientInitializer = credential,
        //         ApplicationName = ApplicationName,
        //     });

        //     // Agora você pode usar 'service' para fazer chamadas à API do Google Drive
        // }

        // static ServiceAccountCredential GetServiceAccountCredential()
        // {
        //     using (var stream = new FileStream(ServiceAccountKeyPath, FileMode.Open, FileAccess.Read))
        //     {
        //         var credential = GoogleCredential.FromStream(stream)
        //                                         .CreateScoped(Scopes)
        //                                         .UnderlyingCredential as ServiceAccountCredential;

        //         if (credential != null)
        //         {
        //             return credential;
        //         }
        //         else
        //         {
        //             throw new Exception("Falha ao criar credencial do serviço.");
        //         }
        //     }
        // }
    
        private static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken =
                "ya29.a0AfB_byAalS_P0KXyFsA9PzG8j8R9PegxH_0QhOcb_6iD48w_SRCJ0kwWqh0Xy2fI87VvnL6q_gKP2AUkOF8cPUBoIYZfpvV1KPVsl7Uhej3z-3h-6syGTCv8PbR9FV8LzDulwpEhr8yrbjIIjUV5yYZ4ynuE7YUPuDfYaCgYKAd4SARASFQHGX2MipTCUdcMD2cqUSP4Dx-aPMQ0171",
                
                RefreshToken = "1//04neWgv32gc_ACgYIARAAGAQSNwF-L9Ir1WY7Z7H4aSqEAezhKTu-teE_6O8HY5um2IAmQBYeEwkxu_o_6cRlehdS9CV858AzqaA",
            };
            
            var applicationName = "LivroEMente" ;// Use the name of the project in Google Cloud
            var username = "grappeincvinum@gmail.com"; // Use your email
            
            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "790068342173-q0bqobbsdblgki94pvm5888e8dp4aqnb.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });
    
            var credential = new UserCredential(apiCodeFlow, username, tokenResponse); 
            
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            
            return service;
        }

    //    [HttpPost]
    //     [AllowAnonymous] 
    //     public string CreateFolder(string parent, string folderName)
    //     {
    //          var service = GetService();
    //         var driveFolder = new Google.Apis.Drive.v3.Data.File();
    //         driveFolder.Name = folderName;
    //         driveFolder.MimeType = "application/vnd.google-apps.folder";
    //         driveFolder.Parents = new string[] { parent };
    //         var command = service.Files.Create(driveFolder);
    //         var file = command.Execute();
    //         return file.Id;
    //     }

        
         [HttpPost]
         [AllowAnonymous] 
        public IActionResult Upload(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                using (var stream = arquivo.OpenReadStream())
                {
                    // Chame o método UploadFile passando o fluxo de dados do arquivo e outras informações necessárias
                    string fileId = UploadFile(stream, arquivo.FileName, arquivo.ContentType, "1m1yulxFuOKXx3Pc6BkAXkAgwcv_8qp6g", "Descrição do arquivo");
                    
                        string link = ($"https://drive.google.com/file/d/{fileId}/view");
                    // Faça algo com o ID do arquivo retornado, se necessário
                    return Ok(link);
                }
            }
            else
            {
                return BadRequest("Nenhum arquivo enviado.");
            }
        }




        private string UploadFile(Stream file, string fileName, string fileMime, string folder, string fileDescription)
        {


            DriveService service = GetService();
        
            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = fileName;
            driveFile.Description = fileDescription;
            driveFile.MimeType = fileMime;
            driveFile.Parents = new string[] { folder };
            
            var request = service.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";
            
            var response = request.Upload();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                throw response.Exception;
            
            return request.ResponseBody.Id;
        }
        
        [HttpDelete]
         [AllowAnonymous] 
        public void DeleteFile(string fileId)
        {
            var service = GetService();
            var command = service.Files.Delete(fileId);
            var result = command.Execute();
        }

        // [HttpGet]
        //  [AllowAnonymous] 
        // public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder)
        // {
        //     var service = GetService();
            
        //     var fileList = service.Files.List();
        //     fileList.Q =$"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
        //     fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";
            
        //     var result = new List<Google.Apis.Drive.v3.Data.File>();
        //     string pageToken = null;
        //     do
        //     {
        //         fileList.PageToken = pageToken;
        //         var filesResult = fileList.Execute();
        //         var files = filesResult.Files;
        //         pageToken = filesResult.NextPageToken;
        //         result.AddRange(files);
        //     } while (pageToken != null);
            
        //     return result;
        // }

    }
}