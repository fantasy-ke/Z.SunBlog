namespace Z.Ddd.Common.Minio;

public class UploadObjectInput
{
    public string BucketName { get; set; }

    public string ObjectName { get; set; }

    public string ContentType { get; set; } 

    public Stream Stream { get; set; }

    public UploadObjectInput() { }

    public UploadObjectInput(string bucketName,string objectName,string contentType,Stream stream)
    {
        BucketName= bucketName;
        ObjectName= objectName;
        ContentType= contentType;
        Stream = stream;
    }
}
