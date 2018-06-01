using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Models
{
    public class WolframealphaProxy
    {
        public async static Task<Rootobject> GetResult(String formula)
        {
            var http = new HttpClient();
            String uri = "https://api.wolframalpha.com/v2/query?appid=XK337J-85Q4GE8QEU&podstate=Result__Step-by-step+solution&format=plaintext&output=json&input=solve+" + formula;
            var response = await http.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Rootobject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Rootobject)serializer.ReadObject(ms);

            return data;
        }
    }
}

[DataContract]
public class Rootobject
{
    [DataMember]
    public Queryresult queryresult { get; set; }
}


[DataContract]
public class Queryresult
{
    [DataMember]
    public bool success { get; set; }
    [DataMember]
    public bool error { get; set; }
    [DataMember]
    public int numpods { get; set; }
    [DataMember]
    public string datatypes { get; set; }
    [DataMember]
    public string timedout { get; set; }
    [DataMember]
    public string timedoutpods { get; set; }
    [DataMember]
    public float timing { get; set; }
    [DataMember]
    public float parsetiming { get; set; }
    [DataMember]
    public bool parsetimedout { get; set; }
    [DataMember]
    public string recalculate { get; set; }
    [DataMember]
    public string id { get; set; }
    [DataMember]
    public string host { get; set; }
    [DataMember]
    public string server { get; set; }
    [DataMember]
    public string related { get; set; }
    [DataMember]
    public string version { get; set; }
    [DataMember]
    public Pod[] pods { get; set; }
}

[DataContract]
public class Pod
{
    [DataMember]
    public string title { get; set; }
    [DataMember]
    public string scanner { get; set; }
    [DataMember]
    public string id { get; set; }
    [DataMember]
    public int position { get; set; }
    [DataMember]
    public bool error { get; set; }
    [DataMember]
    public int numsubpods { get; set; }
    [DataMember]
    public Subpod[] subpods { get; set; }
    [DataMember]
    public bool primary { get; set; }
    [DataMember]
    public State[] states { get; set; }
}

[DataContract]
public class Subpod
{
    [DataMember]
    public string title { get; set; }
    [DataMember]
    public string plaintext { get; set; }
}

[DataContract]
public class State
{
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public string input { get; set; }
}
