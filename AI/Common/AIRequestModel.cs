using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.AI.Common
{
    public class AIRequestModel
    {
        public AIRequestModel()
        {
            var allTypes = Assembly.GetExecutingAssembly().DefinedTypes;

            var declarationTypes = allTypes
                .Where(t => typeof(Functiondeclaration).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();
            var declarationClass = declarationTypes
                .Select(type => (Functiondeclaration)Activator.CreateInstance(type))
                .ToList();

            this.tools = new List<Tool> { new Tool { functionDeclarations = declarationClass } };
        }

        public List<Content> contents { get; set; } = new List<Content>();
        public List<Tool> tools { get; set; }

        public class Content
        {
            public string role { get; set; }
            public List<Part> parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }

        public class Tool
        {
            public List<Functiondeclaration> functionDeclarations { get; set; }
        }

        public abstract class Functiondeclaration
        {
            public abstract string name { get; }
            public abstract string description { get; }
            public abstract Parameters parameters { get; }
        }

        public abstract class Parameters
        {
            public abstract string type { get; }
            public abstract object properties { get; }
            public abstract List<string> required { get; }
        }


        //public class Brightness
        //{
        //    public string type { get; set; }
        //    public string description { get; set; }
        //}

        //public class Color_Temp
        //{
        //    public string type { get; set; }
        //    public List<string> @enum { get; set; }
        //    public string description { get; set; }
        //}

        //public class Properties
        //{
        //    public Brightness brightness { get; set; }
        //    public Color_Temp color_temp { get; set; }

        //    public Attendees attendees { get; set; }
        //    public Date date { get; set; }
        //    public Time time { get; set; }
        //    public Topic topic { get; set; }
        //}

        public class Items
        {
            public string type { get; set; }
        }

    }
}
