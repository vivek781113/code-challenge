using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace refactor_this.Models
{

    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DescriptionAttribute(description: "ignore")]
        [JsonIgnore]
        public bool IsNew { get; }
    }
}