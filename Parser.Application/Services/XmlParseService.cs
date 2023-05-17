using Parser.Core.Interfaces;
using System.Text;
using System.Xml;
using Parser.DataContext.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Parser.Domain.Models.Exceptions;

namespace Parser.Application.Services
{
    public class XmlParseService : IParseService
    {
        private readonly IRepository _repository;
        private readonly ILogger<XmlParseService> _logger;

        public XmlParseService(IRepository repository, ILogger<XmlParseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>> Parse(Stream stream)
        {
            Dictionary<string, object> result = new();
            StringBuilder xmlFile = new();
            XmlDocument doc = new XmlDocument();

            using (var reader = new StreamReader(stream))
            {
                xmlFile.AppendLine(reader.ReadToEnd());
            }

            try
            {
                doc.LoadXml(xmlFile.ToString());
            }
            catch (XmlException)
            {
                _logger.LogError("Incorrect XML file");
            }


            var entities = doc.DocumentElement?.SelectNodes("Entity");
            for (int i = 0; i < entities.Count; i++)
            {
                //GENERAL PROPS PARSE
                var action = entities[i].SelectSingleNode("EntityAction")?.InnerText.ToLower();
                var key = entities[i].SelectSingleNode("EntityKey")?.InnerText;
                var name = entities[i].SelectSingleNode("EntityName")?.InnerText;
                var xmlProps = entities[i].SelectSingleNode("EntityProperties")?.SelectNodes("EntityProperty");

                if ((action == "update" | action == "delete") & string.IsNullOrEmpty(key))
                    throw new NoIdException(action);

                var instanceType = Type.GetType($"Parser.DataContext.Entities.{name},Parser.DataContext");
                var instance = Activator.CreateInstance(instanceType);

                if (!string.IsNullOrEmpty(key))
                    instanceType.GetProperty("Id").SetValue(instance, Guid.Parse(key));


                //VALUE PROPS PARSE
                for (int j = 0; j < xmlProps.Count; j++)
                {
                    var property = instanceType.GetProperty(xmlProps[j].SelectSingleNode("PropertyName").InnerText);
                    var value = xmlProps[j].SelectSingleNode("PropertyValue").InnerText;
                    try
                    {
                        property.SetValue(instance, value);
                    }
                    catch (ArgumentException)
                    {
                        object parsedValue = xmlProps[j].SelectSingleNode("PropertyType").InnerText.ToLower() switch
                        {
                            "dateonly" => DateOnly.Parse(value),
                            "number" => int.Parse(value),
                            "guid" => string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value),
                            _ => throw new WrongFieldTypeException(),
                        };
                        property.SetValue(instance, parsedValue);
                    }
                }

                //LINK PROPS PARSE
                xmlProps = entities[i].SelectSingleNode("EntityLinkProperties")?.SelectNodes("EntityLinkProperty");
                if (xmlProps != null)
                {
                    for (int j = 0; j < xmlProps.Count; j++)
                    {
                        var property = instanceType.GetProperty(xmlProps[j].SelectSingleNode("PropertyName").InnerText);
                        var value = xmlProps[j].SelectSingleNode("PropertyValue").InnerText;
                        try
                        {
                            property.SetValue(instance, xmlProps[j].SelectSingleNode("PropertyValue").InnerText);

                        }
                        catch (ArgumentException)
                        {
                            object parsedValue = xmlProps[j].SelectSingleNode("PropertyType").InnerText.ToLower() switch
                            {
                                "dateonly" => DateOnly.Parse(value),
                                "number" => int.Parse(value),
                                "guid" => string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value),
                                _ => throw new WrongFieldTypeException(),
                            };
                            property.SetValue(instance, parsedValue);
                        }
                    }
                }

                //ENTITIES PARSE
                xmlProps = entities[i].SelectSingleNode("Entities")?.SelectNodes("Entity");
                if (xmlProps != null)
                {
                    for (int j = 0; j < xmlProps.Count; j++)
                    {
                        instanceType.GetProperty(xmlProps[j].SelectSingleNode("EntityName").InnerText).SetValue(instance, ReferenceValueCalculate(xmlProps[j], action));
                    }
                }

                result.Add(action, instance);

                _repository.Save();
            }

            return result;
        }

        public object ReferenceValueCalculate(XmlNode node, string action)
        {
            var key = node.SelectSingleNode("EntityKey")?.InnerText;
            var name = node.SelectSingleNode("EntityName")?.InnerText;
            var xmlProps = node.SelectSingleNode("EntityProperties")?.SelectNodes("EntityProperty");

            var instanceType = Type.GetType($"Parser.DataContext.Entities.{name},Parser.DataContext");
            var instance = Activator.CreateInstance(instanceType);

            if ((action == "update" | action == "delete") & string.IsNullOrEmpty(key))
                throw new NoIdException(action);

            if (!string.IsNullOrEmpty(key))
                instanceType.GetProperty("Id").SetValue(instance, Guid.Parse(key));

            //VALUE PROPS PARSE
            for (int j = 0; j < xmlProps.Count; j++)
            {
                var property = instanceType.GetProperty(xmlProps[j].SelectSingleNode("PropertyName").InnerText);
                var value = xmlProps[j].SelectSingleNode("PropertyValue").InnerText;
                try
                {
                    property.SetValue(instance, value);
                }
                catch (ArgumentException)
                {
                    object parsedValue = xmlProps[j].SelectSingleNode("PropertyType").InnerText.ToLower() switch
                    {
                        "dateonly" => DateOnly.Parse(value),
                        "number" => int.Parse(value),
                        "guid" => string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value),
                        _ => throw new WrongFieldTypeException(),
                    };
                    property.SetValue(instance, parsedValue);
                }
            }

            //LINK PROPS PARSE
            xmlProps = node.SelectSingleNode("EntityLinkProperties")?.SelectNodes("EntityLinkProperty");
            if (xmlProps != null)
            {
                for (int j = 0; j < xmlProps?.Count; j++)
                {
                    var property = instanceType.GetProperty(xmlProps[j].SelectSingleNode("PropertyName").InnerText);
                    var value = xmlProps[j].SelectSingleNode("PropertyValue").InnerText;
                    try
                    {
                        property.SetValue(instance, xmlProps[j].SelectSingleNode("PropertyValue").InnerText);
                    }
                    catch (ArgumentException)
                    {
                        object parsedValue = xmlProps[j].SelectSingleNode("PropertyType").InnerText.ToLower() switch
                        {
                            "dateonly" => DateOnly.Parse(value),
                            "number" => int.Parse(value),
                            "guid" => string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value),
                            _ => throw new WrongFieldTypeException(),
                        };
                        property.SetValue(instance, parsedValue);
                    }
                }
            }

            //ENTITIES PARSE
            xmlProps = node.SelectSingleNode("Entities")?.SelectNodes("Entity");
            if (xmlProps != null)
            {
                for (int j = 0; j < xmlProps.Count; j++)
                {
                    instanceType.GetProperty(xmlProps[j].SelectSingleNode("EntityName").InnerText).SetValue(instance, ReferenceValueCalculate(xmlProps[j], action));
                }
            }

            return instance;
        }
    }
}
