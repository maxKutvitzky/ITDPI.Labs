﻿using System.Xml;
using System.Xml.Schema;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.Utils;

public class XmlValidator : IDishShopValidator
{
    public List<string> ValidationErrors { get; set; } = new();

    public bool IsValid(string path, XmlSchema schema)
    {
        ValidationErrors.Clear();
        try
        {
            var settings = GetReaderSettings(schema);

            var reader = XmlReader.Create(path, settings);

            while (reader.Read()) ;
        }
        catch (Exception e)
        {
            ValidationErrors.Add(e.Message);
            return false;
        }

        return !ValidationErrors.Any();
    }

    public void ValidateCallBack(object sender, ValidationEventArgs args)
    {
        ValidationErrors.Add(args.Message);
    }

    private XmlReaderSettings GetReaderSettings(XmlSchema schema)
    {
        var settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
        settings.Schemas.Add(schema);
        settings.Schemas.XmlResolver = new XmlUrlResolver();
        settings.ValidationEventHandler += ValidateCallBack;
        return settings;
    }
}