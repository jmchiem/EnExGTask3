using System;
using System.IO;
using Newtonsoft.Json;

public class Msg
{
    public string message;
    public string time;

    public Msg(string m)
    {
        message = m;
        time = DateTime.Now.ToString("HH:mm"); // gets time of message
    }
}

public static class EnExGTask3
{

    public static string getJson(string m)
    {
        Msg tmp;
        string json = string.Empty;
        string path = Directory.GetCurrentDirectory() + "\\" + "saveMsg.json";

        // if string is null, throw exception
        if (string.IsNullOrEmpty(m))
        {
            throw new System.ArgumentException("Input cannot be null.");
        }

        // throw exception given prohibited word 
        if (m.Contains("pinapple"))
        {
            throw new System.ArgumentException("Pinapple is not allowed.");
        }

        // get JSON 
        tmp = new Msg(m);

        if (tmp == null)
        {
            Console.Error.WriteLine("Message could not be processed.");
        }
        else
        {
            json = JsonConvert.SerializeObject(tmp);
        }

        // overwrite last saved message
        try
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(json);
            sw.Close();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Message not stored: {0}", e);
        }

        return json;
    }

    public static string getLastMessage()
    {
        string json = string.Empty;
        string path = Directory.GetCurrentDirectory() + "\\" + "saveMsg.json";

        // if file exists, return last message, else return empty string
        if (File.Exists(path))
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                json = sr.ReadLine();
                sr.Close();

            }
            catch (Exception e)
            {
                Console.Error.WriteLine("{0}", e);
            }
        }

        return json;
    }

    public static void Main()
    {

        string test = "hello world";
        string result = EnExGTask3.getJson(test);
        Console.WriteLine("Test message: " + result);

        result = EnExGTask3.getLastMessage();
        Console.WriteLine("Retreive last message: " + result); 
    }

}