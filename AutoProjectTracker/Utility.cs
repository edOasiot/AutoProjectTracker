using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoProjectTracker
{
    public static class Utility
    {
        public static Object GetFieldValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                FieldInfo info = type.GetField(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj);
            }
            return obj;
        }

        // so we can use reflection to access the object properties
        public static T GetFieldValue<T>(this Object obj, String name)
        {
            Object retval = GetFieldValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj);
            }
            return obj;
        }

        // so we can use reflection to access the object properties
        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static Type GetPropType(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                return info.PropertyType;
            }
            return null;
        }

        public static void SetPropValue(this Object obj, String name, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(name);
            propertyInfo.SetValue(obj, value, null);
        }

        public static async Task<string> GetStringAsync(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < 5; i++)
                {
                    // Always catch network exceptions for async methods
                    try
                    {
                        return await httpClient.GetStringAsync(new Uri(Url));
                    }
                    catch (Exception ex)
                    {
                        // Details in ex.Message and ex.HResult.
                    }
                }
            }
            return null;
        }

        //public static void WriteToLog(string LogFileName, string msg, bool notimeStamp = false, bool deleteFile = false)
        //{
        //    if (String.IsNullOrEmpty(LogFileName))
        //        return;

        //    LogFile.WriteToLog(LogFileName, msg, 0, notimeStamp, deleteFile);
        //}

        //public static void WriteToLog(string LogFileName, string MethodName, Exception Ex)
        //{
        //    if (String.IsNullOrEmpty(LogFileName))
        //        return;

        //    string msg = MethodName + ": " + Ex.Message + "\n" + Ex.StackTrace;

        //    if (Ex.InnerException != null)
        //        msg += "\n" + Ex.InnerException.Message;

        //    LogFile.WriteToLog(LogFileName, msg, 0);
        //}

        //public static void WaitForNextMarketInterval(Int32 additionSleepSeconds = 0)
        //{
        //    Settings settings = ReadSettings();
        //    DateTime dt = DateTime.Now;
        //    Thread.Sleep((60 - dt.Second) * 1000);
        //    Thread.Sleep(additionSleepSeconds * 1000);
        //    int minutePastHour = dt.Minute + 1;

        //    int mod = (minutePastHour + settings.MarketIntervalMinutePastHour) % settings.MarketIntervalMinutes;

        //    if (!mod.Equals(0))
        //        Thread.Sleep((settings.MarketIntervalMinutes - mod) * 60000);
        //}

        public static Settings ReadSettings()
        {
            Settings settings;

            string path = LocateSettingFile();

            using (StreamReader sr = File.OpenText(path))
            {
                settings = JsonConvert.DeserializeObject<Settings>(sr.ReadToEnd());
            }

            return settings;
        }

        private static string LocateSettingFile()
        {
            string exeLocation = Assembly.GetEntryAssembly().Location;
            string baseDir = exeLocation.Substring(0, exeLocation.LastIndexOf(@"\"));
            string path = baseDir + @"\" + Environment.MachineName + ".json";

            if (!File.Exists(path))
                path = baseDir + @"\AutoProjectTracker.json";

            if (!File.Exists(path))
                path = baseDir + @"\..\..\" + Environment.MachineName + ".json";

            if (!File.Exists(path))
                path = baseDir + @"\..\..\AutoProjectTracker.json";

            return path;
        }

        public static Settings UpdateSetting(string key, object value)
        {
            Settings settings = ReadSettings();

            string path = LocateSettingFile();

            string FileData = File.ReadAllText(path);

            Type settingType = GetPropType(settings, key);
            object settingValue = GetPropValue(settings, key);
            if (settingValue == null)
                settingValue = "";

            FileData = FileData.Replace("\"" + key + "\": \"" + settingValue.ToString() + "\"", "\"" + key + "\": \"" + value.ToString() + "\"");

            File.WriteAllText(path, FileData);

            return ReadSettings();
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dict)
        {
            return dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static T ConvertJsonToClass<T>(this string json)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        public static DateTime ParseDateTime(String dt)
        {
            DateTime dateTime;

            string[] validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                "MM/dd/yyyy hh:mm tt", "M/d/yyyy h:m:s tt", "yyyy-MM-dd HH:mm:ss,fff" };

            if (DateTime.TryParseExact(dt, validformats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else
                return DateTime.MinValue;
        }

        static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

        public static string Encrypt(string TextToEncrypt)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(TextToEncrypt);
                byte[] encryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Pass the data to ENCRYPT, the public key information 
                    //(using RSACryptoServiceProvider.ExportParameters(false),
                    //and a boolean flag specifying no OAEP padding.
                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                    //Display the decrypted plaintext to the console. 
                    return ByteConverter.GetString(encryptedData);
                }
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }

            return "";
        }

        public static string Decrypt(string TextToDecrypt)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] encryptedData = ByteConverter.GetBytes(TextToDecrypt);
                byte[] decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Pass the data to DECRYPT, the private key information 
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding.
                    decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                    //Display the decrypted plaintext to the console. 
                    return ByteConverter.GetString(decryptedData);
                }
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }

            return "";
        }

        public static void SendMailUsingYahoo(string key)
        {
            try
            {
                Settings settings = ReadSettings();

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("stockminder2020@yahoo.com");

                //receiver email adress
                mailMessage.To.Add(settings.Email);

                //subject of the email
                mailMessage.Subject = "StockMinder Key";

                mailMessage.Body = "Copy this key into your json settings file:" + Environment.NewLine + Environment.NewLine + key;
                mailMessage.IsBodyHtml = true;

                //SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com");
                //port number for Yahoo
                smtpClient.Port = 587;
                //credentials to login in to yahoo account
                smtpClient.Credentials = new NetworkCredential("stockminder2020@yahoo.com", "imxzdwjlheowwgru");
                //enabled SSL
                smtpClient.EnableSsl = true;
                //Send an email
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //WriteToLog(@"C:\StockMinderService.txt", "SendMail", ex);
            }
        }
        public static List<string> GetTableFields(Type type, ref string sql)
        {
            List<string> fields = new List<string>();

            foreach (PropertyInfo field in type.GetProperties())
            {
                fields.Add(field.Name);
                sql += field.Name + " ";
                sql += GetDBFieldName(field.PropertyType.Name) + ",";
            }
            sql = sql.Substring(0, sql.Length - 1) + ")";

            return fields;
        }

        public static string GetDBFieldName(string DBFieldType)
        {
            return GetTypeToDBField()[DBFieldType];
        }

        public static string GetDBFieldType(string DBFieldName)
        {
            return GetTypeToDBField().First(x => x.Value.Equals(DBFieldName)).Key;
        }
        public static IDictionary<string, string> DBtypeProvider;
        public static IDictionary<string, string> GetTypeToDBField()
        {
            if (DBtypeProvider == null)
            {
                DBtypeProvider = new Dictionary<string, string>()
                    {
                        {"Decimal", "real"},
                        {"String", "text"},
                        {"DateTime", "text"},
                        {"Int64", "integer" },
                        {"Int32", "integer" },
                        {"int", "integer" },
                        {"bool", "integer" },
                        {"Boolean", "integer" },
                        {"status", "status" }
                    };
            }

            return DBtypeProvider;
        }
        public static string SetDateTime(object value)
        {
            return "'" + DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
        }

        public static double WeightedAverage<T>(this IEnumerable<T> records, Func<T, double> value, Func<T, double> weight)
        {
            double weightedValueSum = records.Sum(x => value(x) * weight(x));
            double weightSum = records.Sum(x => weight(x));

            if (weightSum != 0)
                return weightedValueSum / weightSum;
            else
                throw new DivideByZeroException("Your message here");
        }

        //public static void CalculateRSI(ref List<Bar> bars, Bar PrevBar, int Length=15)
        //{
        //    decimal NetChgAvg = 0;
        //    decimal TotChgAvg = 0;
        //    decimal Change = 0;
        //    decimal SF = 1 / Convert.ToDecimal(Length);
        //    decimal ChgRatio = 0;
        //    int iStart = 0;

        //    //if (File.Exists(@"C:\Temp\NinjaTrader\RSI.csv"))
        //    //File.Delete(@"C:\Temp\NinjaTrader\RSI.csv");

        //    if (PrevBar == null)
        //        iStart = Length;

        //    for (int i = iStart; i < bars.Count; i++)
        //    {
        //        if (i == Length && PrevBar == null)
        //        {
        //            bars[i].NetChgAvg = (bars[i].Close - bars[0].Close) / Convert.ToDecimal(Length);
        //            for (int j = Length; j > 0; j--)
        //                TotChgAvg += Math.Abs(bars[j].Close - bars[j - 1].Close);

        //            bars[i].TotChgAvg = TotChgAvg / Convert.ToDecimal(Length);
        //        }
        //        else if (i == 0)
        //        {
        //            bars[i] = CalculateRSIValues(bars[i], PrevBar, Length);
        //        }
        //        else
        //        {
        //            //Change = bars[i].Close - bars[i - 1].Close;
        //            //NetChgAvg = bars[i - 1].NetChgAvg + SF * (Change - bars[i - 1].NetChgAvg);
        //            //TotChgAvg = bars[i - 1].TotChgAvg + SF * (Math.Abs(Change) - bars[i - 1].TotChgAvg);
        //            bars[i] = CalculateRSIValues(bars[i], bars[i-1], Length);
        //        }

        //        if (TotChgAvg != 0)
        //            ChgRatio = NetChgAvg / TotChgAvg;
        //        else
        //            ChgRatio = 0;

        //        //Utility.WriteToLog(@"C:\Temp\NinjaTrader\RSI.csv", bars[i].Time + "," + bars[i].Close + "," + Math.Round(NetChgAvgPrev, 2).ToString() + "," + Math.Round(TotChgAvgPrev, 2).ToString() + "," + Math.Round(Change, 2).ToString() + "," + Math.Round(NetChgAvg, 2).ToString() + "," + Math.Round(TotChgAvg, 2).ToString() + "," + Math.Round(bars[i].RSI,2), true);                 
        //    }
        //}

        public static decimal CalculateRSI(decimal NetChgAvg, decimal TotChgAvg)
        {
            if (TotChgAvg.Equals(0))
                return 0;

            return Convert.ToDecimal(50 * ((NetChgAvg / TotChgAvg) + 1));
        }

        //public static Bar CalculateRSIValues(Bar bar, Bar PrevBar, int Length = 15)
        //{
        //    decimal SF = 1 / Convert.ToDecimal(Length);
        //    decimal Change = bar.Close - PrevBar.Close;

        //    Bar newBar = new Bar(bar);

        //    newBar.NetChgAvg = PrevBar.NetChgAvg + SF * (Change - PrevBar.NetChgAvg);
        //    newBar.TotChgAvg = PrevBar.TotChgAvg + SF * (Math.Abs(Change) - PrevBar.TotChgAvg);

        //    return newBar;
        //}
    }
}
