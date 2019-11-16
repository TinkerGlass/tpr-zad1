﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Classes
{
    [Serializable]
    [XmlRoot("CatalogRoot")]
    public class Catalog : ICloneable, IOwnSerialization
    {
        private static long nextID = 0;
        [XmlIgnore]
        private long code;
        [XmlElement("code")]
        public long Code
        {
            get { return code; }
            set
            {
                if (value > nextID)
                    nextID = value + 1;
                code = value;
            }
        }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("author")]
        public string Author { get; set; }

        public Catalog(string title, string description, string author)
        {
            Code = getNextID();
            Title = title;
            Description = description;
            Author = author;
        }

        public Catalog(Catalog catalog)
        {
            Code = catalog.Code;
            Title = catalog.Title;
            Description = catalog.Description;
            Author = catalog.Author;
        }

        public Catalog() { }

        public override string ToString()
        {
            return "Book id " + this.Code + " Title: " + this.Title + ", description: " + this.Description + ", author: " + this.Author;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is Catalog)) return false;
            return ((Catalog)obj).Code == Code;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (7 * 17) ^ Code.GetHashCode();
            }
        }

        public static long getNextID()
        {
            return nextID++;
        }

        public object Clone()
        {
            return new Catalog(this);
        }

        public string Serialization(ObjectIDGenerator idGenerator)
        {
            string serializedData = "";
            serializedData += this.GetType().FullName + ";";
            serializedData += idGenerator.GetId(this, out bool firstTime).ToString() + ";";
            serializedData += this.Code.ToString() + ";";
            serializedData += this.Title + ";";
            serializedData += this.Description + ";";
            serializedData += this.Author + ";";
            return serializedData;
        }

        public void Deserialization(string[] data, Dictionary<long, Object>  deserializedObjects)
        {
            this.Code = long.Parse(data[2]);
            this.Title = data[3];
            this.Description = data[4];
            this.Author = data[5];
        }
    }
}
