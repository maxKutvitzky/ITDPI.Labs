using System.Xml;
using DishShop.Lab2.Parsers.Entities.Base;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalMapper<T> where T : Entity
{
    T XmlToObject(XmlTextReader Xml);
    List<T> XmlToObjects(XmlTextReader Xml);
    string ObjectToXml(T obj);
    string ObjectsToXml(List<T> list);
}