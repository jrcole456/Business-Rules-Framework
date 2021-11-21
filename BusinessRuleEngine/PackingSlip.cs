using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine
{
    /// <summary>
    /// Holds the packing slip details.
    /// 
    /// </summary>
    public class PackingSlip : IEquatable<PackingSlip>
    {
        /// <summary>
        /// Holds a unique ID for the packing slip.
        /// </summary>
        private int UniqueID { get; set; }

        /// <summary>
        /// the name of the packing slip.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The department name of the packing slip.
        /// </summary>
        public string Department { get; private set; }

        /// <summary>
        /// The final destination of the packing slip.
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        /// The source of the packing slip.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// the agent name.
        /// </summary>
        public Agent CommissionAgent { get; set; }

        /// <summary>
        /// All the additional products added to the payment.
        /// </summary>
        public List<Product> AddedProducts { get; private set; }

        /// <summary>
        /// A simple generate unique ID to identify the packing slip.
        /// </summary>
        /// <returns></returns>
        private static int GetUniqueID()
        {
            return new Random().Next(0, Int16.MaxValue);
        }

        /// <summary>
        /// Returns true if this packing slip is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !String.IsNullOrEmpty(Name) || !String.IsNullOrEmpty(Department);
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PackingSlip()
        {
            Name = String.Empty;
            Destination = String.Empty;
            Source = String.Empty;
            Department = String.Empty;
            UniqueID = GetUniqueID();
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        public PackingSlip(string name, string source, string destination,
            string department, Agent agent)
        {
            Name = name;
            Source = source;
            Destination = destination;
            Department = department;
            AddedProducts = new List<Product>();
            CommissionAgent = new Agent(agent); ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        /// <param name="product"></param>
        public PackingSlip(string name, string source, string destination,
            string department, Product product)
        {
            Name = name;
            Source = source;
            Destination = destination;
            Department = department;
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>() { product };
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="other"></param>
        public PackingSlip(PackingSlip other)
        {
            Name = other.Name;
            Destination = other.Name;
            Source = other.Source;
            Department = other.Department;
            UniqueID = other.UniqueID;
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>(other.AddedProducts);
        }

        /// <summary>
        /// GetHashCode method used for merging lists of configuration.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            if (Object.ReferenceEquals(this, null)) return 0;
            return Name.GetHashCode() +
                Source.GetHashCode() +
                Destination.GetHashCode() +
                Department.GetHashCode() +        
                CommissionAgent.Name.GetHashCode() +
                AddedProducts.Select(s => s.Name).OrderBy(s => s).Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
        }

        /// <summary>
        /// The Equals method.
        /// </summary>
        public bool Equals(PackingSlip other)
        {
            return (this == other);
        }

        /// <summary>
        /// Override the Equals method.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PackingSlip);
        }

        /// <summary>
        /// Equals operator.
        /// </summary>
        public static bool operator ==(PackingSlip ts1, PackingSlip ts2)
        {
            if (object.ReferenceEquals(ts1, ts2))
            {
                return true;
            }
            // If the test system are both null
            if ((object.ReferenceEquals(ts1, null)) || (object.ReferenceEquals(ts2, null)))
            {
                return false;
            }

            // Make the equality test system.
            return (ts1.Name == ts2.Name) &&
                (ts1.Source == ts2.Source) &&
                (ts1.Destination == ts2.Destination) &&
                (ts1.Department == ts2.Department) &&
                (ts1.CommissionAgent.Name == ts2.CommissionAgent.Name) &&
                (ts1.AddedProducts.Select(a => a.Name).SequenceEqual(ts2.AddedProducts.Select(a => a.Name)));
        }

        /// <summary>
        /// Not equals operator.
        /// </summary>
        public static bool operator !=(PackingSlip ts1, PackingSlip ts2)
        {
            return !(ts1 == ts2);
        }
    }
}
