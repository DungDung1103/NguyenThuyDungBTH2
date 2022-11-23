using System.Text.RegularExpressions;
namespace NguyenThuyDungBTH2.Models.Process

{
    public class IdAuto 
    {
       public string AutoGenerator(string strInput)
       {
        string strResult="", numPart="", strPart="";
        //tách phần số từ strInput
        numPart = Regex.Match(strInput,@"\d+").Value;
        //tách phần chữ từ strInput
        strPart = Regex.Match(strInput,@"\D+").Value;
        //tăng phần số lên 1 đơn vị
        int intPart = (Convert.ToInt32(numPart)+1);
        //bổ sung các kí tự 0 còn thiếu
        for(int i=0;i<numPart.Length - intPart.ToString().Length;i++)
        {
            strPart +="0";
        }
        strResult = strPart+intPart;
        return strResult;
       }

        
    }
}