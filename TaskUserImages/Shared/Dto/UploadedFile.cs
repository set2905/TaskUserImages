namespace Contracts.Dto
{
    public  class UploadedFile
    {
        public UploadedFile(byte[] fileContent)
        {
            FileContent=fileContent;
        }

        public byte[] FileContent { get; set; }
    }
}
