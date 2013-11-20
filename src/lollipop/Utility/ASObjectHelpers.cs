using System;
using System.Linq;
using System.Text;
using FluorineFx;

namespace Lollipop.Utility
{
    public static class ASObjectHelpers
    {
        public static string AsString(this ASObject obj)
        {
            var builder = new StringBuilder();
            if (obj.IsTypedObject)
                builder.Append(obj.TypeName);

            builder.AppendLine("{");

            var values = obj.Select(kvp => string.Format("\t\"{0}\" : \"{1}\"", kvp.Key, kvp.Value));
            builder.Append(string.Join("," + Environment.NewLine, values));
            builder.AppendLine();
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}