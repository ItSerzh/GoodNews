using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NewsAnalyzer.Core.DataTransferObjects
{
    [DataContract]
    public class IsprasDto
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "annotations")]
        public Annotations Annotations { get; set; }
    }

    [DataContract]
    public class Annotations
    {
        [DataMember(Name = "lemma")]
        public List<Lemma> Lemma { get; set; }
    }

    [DataContract]
    public class Lemma
    {
        [DataMember(Name = "start")]
        public int Start { get; set; }
        [DataMember(Name = "end")]
        public int End { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
