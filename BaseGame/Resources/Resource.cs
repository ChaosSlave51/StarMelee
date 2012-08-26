using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGame.Resources
{
    public class Resource:IComparable<Resource>
    {
        public Resource(string path, Type type)
        {
            Path = path;
            Type = type;
        }
        public string Path { get; set; }
        public Type Type { get; set; }

        public static IEnumerable<Resource> Combine(IEnumerable<INeedsResources> list)
        {
            var ret = new List<Resource>();
            foreach (var item in list)
            {
                ret.AddRange(item.ResourcePaths());
            }

            return ret.Distinct();
        }
        public override bool Equals(object obj)
        {
            Resource other = obj as Resource;
            return other != null && string.Equals(other.Path,this.Path);
        } 
        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public int CompareTo(Resource other)
        {
            return string.Compare(this.Path, other.Path);
        }
    }

}
