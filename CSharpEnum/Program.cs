using System;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// C#中枚举操作
/// </summary>
namespace CSharpEnum
{
    public class Program
    {
        static void Main(string[] args)
        {
            Sex sex1 = Sex.Man;
            Sex sex2 = Sex.WoMan;
            //获取枚举的value
            Console.WriteLine((int)sex1); //0
            Console.WriteLine((int)sex2); //1

            //获取枚举的name (下面三种方式都可以)
            Console.WriteLine(sex1.ToString()); //Man
            Console.WriteLine(Enum.GetName(typeof(Sex),sex1)); //Man
            Console.WriteLine(Enum.GetName(sex2.GetType(),sex2)); //WoMan 

            //枚举中一般使用Description特性值用于页面显
            Console.WriteLine(sex1.GetDescription());
            Console.ReadLine();
        }
    }


    /// <summary>
    /// 枚举里面的项就相当于类里面的字段
    /// 枚举的本质其实是类
    /// 性别
    /// </summary>
    public enum Sex
    {
        [Description("男")]
        Man = 0,
        [Description("女")]
        WoMan = 1
    }



}

public static class EnumExtension
{
    public static string GetDescription(this Enum obj)
    {
        if (obj == null)
        {
            return "";
        }
        Type type = obj.GetType();
        //获取枚举的Name
        string enumName = Enum.GetName(obj.GetType(), obj);
        //获取枚举的Field，其实枚举的一个Name就是一个Field
        FieldInfo field = type.GetField(enumName);
        //获取自定义特性。一般这种特性都需要自己封装，但是Description特性
        Attribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        if (attribute != null && attribute is DescriptionAttribute)
        {
            return (attribute as DescriptionAttribute).Description;
        }
        return "";
        
    }
}
