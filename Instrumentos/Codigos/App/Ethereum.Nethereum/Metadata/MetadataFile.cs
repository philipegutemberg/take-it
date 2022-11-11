using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Models;

namespace Ethereum.Nethereum.Metadata
{
    internal class MetadataFile
    {
        public MetadataFile(Event @event, EventTicketType eventType)
        {
            EventName = @event.Title;
            EventDescription = @event.Description;
            ReferenceUrl = string.Empty;
            ImageUrl = @event.ImageUrl;
            Attributes = new List<object>();

            Attributes.Add(new MetadataFile.BasicAttribute("Location", @event.Location));

            string dateText;

            if (eventType.SingleDay)
                dateText = eventType.StartDate.ToShortDateString();
            else
                dateText = $"{eventType.StartDate.ToShortDateString()} a {eventType.EndDate.ToShortDateString()}";

            Attributes.Add(new MetadataFile.BasicAttribute("Date", dateText));

            Attributes.Add(new MetadataFile.BasicAttribute("Qualification", eventType.Qualification.ToString()));
            Attributes.Add(new MetadataFile.BasicAttribute("Access", eventType.TicketName));
        }

        [JsonPropertyName("name")]
        public string EventName { get; set; }

        [JsonPropertyName("description")]
        public string EventDescription { get; set; }

        [JsonPropertyName("external_url")]
        public string ReferenceUrl { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("attributes")]
        public List<object> Attributes { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this);

        public interface IBaseAttribute { }

        public class BasicAttribute : IBaseAttribute
        {
            public BasicAttribute(string name, string value)
            {
                Name = name;
                Value = value;
            }

            [JsonPropertyName("trait_type")]
            public string Name { get; }

            [JsonPropertyName("value")]
            public string Value { get; }
        }

        public class DateAttribute : IBaseAttribute
        {
            public DateAttribute(string name, DateTime date)
            {
                Name = name;
                Value = ((DateTimeOffset)date).ToUnixTimeSeconds();
            }

            [JsonPropertyName("display_type")]
            public string DisplayType => "date";

            [JsonPropertyName("trait_type")]
            public string Name { get; }

            [JsonPropertyName("value")]
            public long Value { get; }
        }
    }
}