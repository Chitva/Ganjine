using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure.Utility
{
    public class GetFileExtension
    {
        
        public string  GetExtension(string FileName)
{
    int a = FileName.LastIndexOf('.');
    int length1 = FileName.Length - (a + 1);
    string Ext =FileName.Substring(a + 1, length1);
    return Ext.ToLower();
}
    }

}