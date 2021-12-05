using System;
using System.Collections.Generic;
using System.Text;

namespace Honoured.FileUploads
{
    public class UploadResult
    {
        public bool Uploaded { get; set; }
        public string FileName { get; set; }
        public string StoredFileName { get; set; }
        public int ErrorCode { get; set; }

        public static UploadResult Error(int errCode)
        {
            return new UploadResult { ErrorCode = errCode };
        }
    }
}
