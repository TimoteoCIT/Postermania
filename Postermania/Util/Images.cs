using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Postermania.Util
{
    public static class Images
    {
        public static byte[] ReadImage(HttpPostedFileBase image)
        {
            byte[] bytes = null;

            //if (image == null || image.ContentLength == 0) 
            //    throw new ArgumentException("Image can not be null or empty");
            if (image == null)
            {
                return null;
            }

            var reader = new BinaryReader(image.InputStream);
            bytes = reader.ReadBytes((int)image.ContentLength);
            return bytes;
        }
    }
}