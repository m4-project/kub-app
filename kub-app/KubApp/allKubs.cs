using System;

public class Class1
{
    public Class1()
    {

    }

    public void kubID()
    {
        if (message != null)
        {
            string json = message;
            var result = JsonConvert.DeserializeObject<RootObject>(json);
        }
    }

    public class newKubs
    {
        public string kubId { get; set; }
        public string kubSerialNumber { get; set; }
    }

    public class RootObject
    {
        public List<newKubs> allKubs { get; set; }
    }
}
