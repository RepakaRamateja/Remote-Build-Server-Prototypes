﻿///////////////////////////////////////////////////////////////////////
// TestXmlSerialization.cs - Demonstrate XML Serializer              //
//                                                                   //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2016   //
// Application: CSE681 Project 3-Remote Build Server Prototypes      //
// Environment: C# console                                           //
///////////////////////////////////////////////////////////////////////
/*
* Package Operations:
* ===================
* Demonstrate serializing an instance of some type to an XML string
* and deserialize back to an instance of that type.
* 
* Public Interface
* ----------------
* Widget widget = new Widget("Jim");//Widget object for serialize
* newWidget.getName()//get the name
* string xml=newWidget.ToXml();//to convert to xml;
* Widget widget=xml.FromXml<Widget>;// to convert to widget object from string
*  
* Required Files:
* ---------------
*  Serialization.cs 
* 
* Build Process
* -------------
* Required files: Serialization.cs 
* 
* Compiler command: csc  Serialization.cs 
* 
* Maintenance History:
* --------------------
* ver 1.0 : 05 Oct 2017
* - first release
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Utilities
{
  public static class ToAndFromXml
  {
    //----< serialize object to XML >--------------------------------

    static public string ToXml(this object obj)
    {
      // suppress namespace attribute in opening tag

      XmlSerializerNamespaces nmsp = new XmlSerializerNamespaces();
      nmsp.Add("", "");

      var sb = new StringBuilder();
      try
      {
        var serializer = new XmlSerializer(obj.GetType());
        using (StringWriter writer = new StringWriter(sb))
        {
          serializer.Serialize(writer, obj, nmsp);
        }
      }
      catch (Exception ex)
      {
        Console.Write("\n  exception thrown:");
        Console.Write("\n  {0}", ex.Message);
      }
      return sb.ToString();
    }
    //----< deserialize XML to object >------------------------------

    static public T FromXml<T>(this string xml)
    {
      try
      {
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(new StringReader(xml));
      }
      catch(Exception ex)
      {
        Console.Write("\n  deserialization failed\n  {0}", ex.Message);
        return default(T);
      }
    }
  }
  public static class Utilities
  {
    public static void title(this string aString, char underline = '-')
    {
      Console.Write("\n  {0}", aString);
      Console.Write("\n {0}", new string(underline, aString.Length + 2));
    }
  }
  ///////////////////////////////////////////////////////////////////
  // Widget class - used to test serialization and deserialization
  //
  public class Widget
  {
    /*
     * comment out the parameterless constructor to make non-serializable
     */
    public Widget() { }
    public Widget(string nm)
    {
      name = nm;
    }
    public string getName()
    {
      return name;
    }
    public string name { get; set; }
  }

    #if (TEST_Xml)
  class TestXmlSerialization
  {
        //<----------------------Driver logic----------------------------------->
    static void Main(string[] args)
    {
      "Demonstrating XML Serialization and Deserialization".title('=');
      Console.WriteLine();
      Console.Write("\n  attempting to serialize Widget object:");
      Widget widget = new Widget("Jim");
      string xml = widget.ToXml();
      if(xml.Count() > 0)
        Console.Write("\n  widget:\n{0}\n", xml);

      Console.Write("\n  attempting to deserialize Widget object:");
      Widget newWidget = xml.FromXml<Widget>();
      if(newWidget == null)
      {
        Console.Write("\n  deserialized object is null");
      }
      else
      {
        string type = newWidget.GetType().Name;
        Console.Write("\n  retrieved object of type: {0}", type);
        string test = newWidget.getName();
        Console.Write("\n  reconstructed widget's name = \"{0}\"", test);
      }
      Console.Write("\n\n");
    }
  }
#endif
}
