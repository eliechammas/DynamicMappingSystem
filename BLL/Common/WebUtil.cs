using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common
{
    public static class WebUtil
    {
        public static string ToBase64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
