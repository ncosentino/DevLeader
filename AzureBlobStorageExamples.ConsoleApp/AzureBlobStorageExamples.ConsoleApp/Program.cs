using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

BlobServiceClient blobServiceClient = new
(
#region Top Secret Stuff
    "DefaultEndpointsProtocol=https;" +
    "AccountName=devleadertest;" +
    "AccountKey=AAABBBCCC;" +
    "EndpointSuffix=core.windows.net"
#endregion
);

 //BlobContainerClient containerClient = blobServiceClient.CreateBlobContainer("thecontainer");
 //await containerClient.CreateAsync();
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("thecontainer");

//BlobClient blobClient = containerClient.GetBlobClient("folder_name/test.txt");
//using Stream blobStream = await blobClient.OpenWriteAsync(overwrite: true);
//using StreamWriter streamWriter = new(blobStream);
//await streamWriter.WriteLineAsync("Hello World");

//BlobClient blobClient = containerClient.GetBlobClient("folder_name/test.txt");
//using Stream blobStream2 = await blobClient.OpenReadAsync();
//using StreamReader streamReader = new(blobStream2);
//string content = await streamReader.ReadToEndAsync();
//Console.WriteLine(content);

//BlobClient blobClient = containerClient.GetBlobClient("folder_name/test.txt");
//await blobClient.UploadAsync("some/file/path");
//await blobClient.UploadAsync(Stream.Null);

//BlobClient blobClient = containerClient.GetBlobClient("folder_name/test.txt");
//await blobClient.SetMetadataAsync(new Dictionary<string, string>
//{
//    { "key1", "value1" },
//    { "key2", "value2" }
//});

//BlobClient blobClient = containerClient.GetBlobClient("folder_name/test.txt");
//await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

var blobId = $"folder_name/{Guid.NewGuid().ToString("N")}.txt";

Console.WriteLine($"Uploading blob to {blobId}...");
BlobClient blobClient = containerClient.GetBlobClient(blobId);
var uploadUri = blobClient.GenerateSasUri(
    BlobSasPermissions.Write,
    DateTimeOffset.UtcNow.AddHours(1));

var httpClient = new HttpClient();
var content = new StringContent("hello world");

content.Headers.Add("x-ms-blob-type", "BlockBlob");

var response = await httpClient.PutAsync(
    uploadUri, 
    content);
Console.WriteLine($"Status code from creating: {response.StatusCode}");

var readUri = blobClient.GenerateSasUri(
    BlobSasPermissions.Read, 
    DateTimeOffset.UtcNow.AddSeconds(5));
Console.WriteLine("Sleeping...");
await Task.Delay(6000);

response = await httpClient.GetAsync(readUri);
Console.WriteLine($"Status code when waiting too long: {response.StatusCode}");

readUri = blobClient.GenerateSasUri(
    BlobSasPermissions.Read,
    DateTimeOffset.UtcNow.AddHours(1));
response = await httpClient.GetAsync(readUri);
Console.WriteLine(
    $"Status code when waiting long enough: {response.StatusCode}");

using var resultStream = response.Content.ReadAsStream();
using StreamReader reader = new(resultStream);
var result = await reader.ReadToEndAsync();
Console.WriteLine(result);
