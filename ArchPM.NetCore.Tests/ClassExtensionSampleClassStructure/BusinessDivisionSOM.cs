using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class BusinessDivisionSOM
    {
        public static BusinessDivisionSOM ECS = new BusinessDivisionSOM(1, nameof(ECS));
        public static BusinessDivisionSOM BRG = new BusinessDivisionSOM(2, nameof(BRG));

        public BusinessDivisionSOM()
        { }

        public BusinessDivisionSOM(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<BusinessDivisionSOM> List()
        {
            return new[] { ECS, BRG };
        }

        public static BusinessDivisionSOM FromName(string name)
        {
            switch (name.ToUpper())
            {
                case "1":
                case "EKS":
                case "ECS":
                    name = "ECS";
                    break;
                case "2":
                case "GL":
                case "BRG":
                    name = "BRG";
                    break;
                default:
                    throw new ArgumentException("Unknown business division:" + name);
            }

            var state = List()
                .SingleOrDefault(
                    s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase)
                );

            if (state == null)
            {
                throw new Exception("UnknownBusinessDivisionException"); //UnknownBusinessDivisionException();
            }

            return state;
        }

        public static BusinessDivisionSOM From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception("UnknownBusinessDivisionException"); //UnknownBusinessDivisionException();
            }

            return state;
        }
    }
}